using System;
using System.Collections.Generic;
using System.Linq;
using System.Text.RegularExpressions;
using NuciDAL.Repositories;
using NuciExtensions;
using NuciLog.Core;
using PersonalLogManager.Api.Models;
using PersonalLogManager.DataAccess.DataObjects;
using PersonalLogManager.Logging;
using PersonalLogManager.Service.Mapping;
using PersonalLogManager.Service.TextBuilding;

namespace PersonalLogManager.Service
{
    public class PersonalLogService(
        IPersonalLogTextBuilderFactory logTextBuilder,
        IFileRepository<PersonalLogEntity> repository,
        ILogger logger) : IPersonalLogService
    {
        private const int MaxLogIdValue = 1000000000;

        private readonly Random random = new();

        public void StorePersonalLog(StoreLogRequest request)
        {
            IEnumerable<LogInfo> logInfos =
            [
                new(MyLogInfoKey.Template, request.Template),
                new(MyLogInfoKey.Date, request.Date),
                new(MyLogInfoKey.Time, request.Time),
                new(MyLogInfoKey.TimeZone, request.TimeZone)
            ];

            logger.Info(
                MyOperation.StorePersonalLog,
                OperationStatus.Started,
                logInfos);

            string id = GenerateUniqueLogId();

            try
            {
                repository.Add(new()
                {
                    Id = id,
                    Date = request.Date,
                    Time = request.Time,
                    TimeZone = request.TimeZone,
                    Template = request.Template,
                    Data = request.Data,
                    CreatedDT = DateTime.UtcNow.ToString("o")
                });

                repository.SaveChanges();
            }
            catch (Exception ex)
            {
                logger.Error(
                    MyOperation.StorePersonalLog,
                    OperationStatus.Failure,
                    ex,
                    logInfos);
                throw;
            }

            logger.Debug(
                MyOperation.StorePersonalLog,
                OperationStatus.Success,
                logInfos,
                new LogInfo(MyLogInfoKey.Identifier, id));
        }

        private string GenerateUniqueLogId()
        {
            string id;

            do
            {
                id = $"L{random.Next(0, MaxLogIdValue):D9}";
            }
            while (repository.ContainsId(id));

            return id;
        }

        public GetLogResponse GetPersonalLogs(GetLogRequest request)
        {
            IEnumerable<LogInfo> logInfos =
            [
                new(MyLogInfoKey.Template, request.Template),
                new(MyLogInfoKey.Date, request.Date),
                new(MyLogInfoKey.Time, request.Time),
                new(MyLogInfoKey.Localisation, request.Localisation),
                new(MyLogInfoKey.Count, request.Count)
            ];

            logger.Info(
                MyOperation.GetPersonalLogs,
                OperationStatus.Started,
                logInfos);

            try
            {
                IEnumerable<PersonalLogEntity> logs = repository.GetAll();

                if (!string.IsNullOrWhiteSpace(request.Date))
                {
                    logs = logs.Where(log => DoesFieldMatch(log.Date, request.Date));
                }

                if (!string.IsNullOrWhiteSpace(request.Time))
                {
                    logs = logs.Where(log => DoesFieldMatch(log.Time, request.Time));
                }

                if (!string.IsNullOrWhiteSpace(request.Template))
                {
                    logs = logs.Where(log => DoesFieldMatch(log.Template, request.Template));
                }

                logs = FilterByRequestData(logs, request.Data);

                logger.Debug(
                    MyOperation.GetPersonalLogs,
                    OperationStatus.Success,
                    logInfos,
                    new LogInfo(MyLogInfoKey.Count, logs.Count()));

                IEnumerable<PersonalLogEntity> sorted = logs
                    .OrderByDescending(log => log.Date)
                    .ThenByDescending(log => log.Time)
                    .ThenBy(log => log.Template)
                    .ThenBy(log => log.CreatedDT)
                    .Take(request.Count);

                return new GetLogResponse()
                {
                    Logs = BuildLogTexts(sorted, request.Localisation)
                };
            }
            catch (Exception ex)
            {
                logger.Error(
                    MyOperation.GetPersonalLogs,
                    OperationStatus.Failure,
                    ex,
                    logInfos);

                throw;
            }
        }

        private static IEnumerable<PersonalLogEntity> FilterByRequestData(
            IEnumerable<PersonalLogEntity> logs,
            Dictionary<string, string> requestedData)
        {
            if (requestedData is null || requestedData.Count == 0)
            {
                return logs;
            }

            foreach (KeyValuePair<string, string> requestedPair in requestedData)
            {
                logs = logs.Where(log =>
                    log.Data is not null &&
                    log.Data.TryGetValue(requestedPair.Key, out string existingValue) &&
                    existingValue is not null &&
                    DoesFieldMatch(existingValue, requestedPair.Value, RegexOptions.IgnoreCase));
            }

            return logs;
        }

        List<string> BuildLogTexts(IEnumerable<PersonalLogEntity> logs, string localisation)
        {
            List<string> logTexts = [];

            if (EnumerableExt.IsNullOrEmpty(logs))
            {
                return [];
            }

            string lastLogId = null;

            try
            {
                foreach (PersonalLogEntity log in logs)
                {
                    lastLogId = log.Id;
                    logTexts.Add($"{log.Id} {logTextBuilder.BuildLogText(log.ToDomainModel(), localisation)}");
                }
            }
            catch (Exception ex)
            {
                throw new InvalidOperationException($"An error occurred while building log text for {lastLogId}.", ex);
            }

            return logTexts;
        }

        public void UpdatePersonalLog(UpdateLogRequest request)
        {
            IEnumerable<LogInfo> logInfos =
            [
                new LogInfo(MyLogInfoKey.Identifier, request.Identifier),
                new LogInfo(MyLogInfoKey.Date, request.Date),
                new LogInfo(MyLogInfoKey.Time, request.Time),
                new LogInfo(MyLogInfoKey.TimeZone, request.TimeZone),
                new LogInfo(MyLogInfoKey.Template, request.Template)
            ];

            logger.Info(
                MyOperation.UpdatePersonalLog,
                OperationStatus.Started,
                logInfos);

            try
            {
                PersonalLogEntity personalLog = repository.Get(request.Identifier);

                if (request.Date is not null)
                {
                    personalLog.Date = request.Date;
                }

                if (request.Time is not null)
                {
                    personalLog.Time = request.Time;
                }

                if (request.TimeZone is not null)
                {
                    personalLog.TimeZone = request.TimeZone;
                }

                if (request.Template is not null)
                {
                    personalLog.Template = request.Template;
                }

                if (request.Data is not null)
                {
                    personalLog.Data ??= [];

                    foreach (string parameter in request.Data.Keys)
                    {
                        personalLog.Data[parameter] = request.Data[parameter];
                    }
                }

                personalLog.UpdatedDT = DateTime.UtcNow.ToString("o");

                repository.Update(personalLog);
                repository.SaveChanges();

                logger.Debug(
                    MyOperation.UpdatePersonalLog,
                    OperationStatus.Success,
                    logInfos);
            }
            catch (Exception ex)
            {
                logger.Error(
                    MyOperation.UpdatePersonalLog,
                    OperationStatus.Failure,
                    ex,
                    logInfos);

                throw;
            }
        }

        public void DeletePersonalLog(DeleteLogRequest request)
        {
            IEnumerable<LogInfo> logInfos =
            [
                new(MyLogInfoKey.Identifier, request.Identifier)
            ];

            logger.Info(
                MyOperation.DeletePersonalLog,
                OperationStatus.Started,
                logInfos);

            try
            {
                repository.Remove(request.Identifier);
                repository.SaveChanges();
            }
            catch (Exception ex)
            {
                logger.Error(
                    MyOperation.DeletePersonalLog,
                    OperationStatus.Failure,
                    ex,
                    logInfos);

                throw;
            }

            logger.Debug(
                MyOperation.DeletePersonalLog,
                OperationStatus.Success,
                logInfos);
        }

        private static bool DoesFieldMatch(
            string input,
            string pattern,
            RegexOptions options = RegexOptions.None)
        {
            if (input is null || pattern is null)
            {
                return false;
            }

            string anchoredPattern = pattern;

            if (!anchoredPattern.StartsWith("^"))
            {
                anchoredPattern = "^" + pattern;
            }

            if (!anchoredPattern.EndsWith("$"))
            {
                anchoredPattern += "$";
            }

            return Regex.IsMatch(input, anchoredPattern, options);
        }
    }
}