using System;
using System.Collections.Generic;
using System.Linq;
using System.Text.RegularExpressions;

using NuciDAL.Repositories;

using NuciExtensions;

using NuciLog.Core;

using PersonalLogManager.DataAccess.DataObjects;
using PersonalLogManager.Logging;
using PersonalLogManager.Service.Mapping;
using PersonalLogManager.Service.Models;
using PersonalLogManager.Service.TextBuilding;

namespace PersonalLogManager.Service
{
    public class PersonalLogService(
        IPersonalLogTextBuilderFactory logTextBuilder,
        IFileRepository<PersonalLogEntity> repository,
        ILogger logger) : IPersonalLogService
    {
        private static int MaxLogIdValue => 1000000000;

        private readonly Random random = new();

        public void StorePersonalLog(PersonalLogCreation creation)
        {
            IEnumerable<LogInfo> logInfos =
            [
                new(MyLogInfoKey.Template, creation.Template),
                new(MyLogInfoKey.Date, creation.Date),
                new(MyLogInfoKey.Time, creation.Time),
                new(MyLogInfoKey.TimeZone, creation.TimeZone)
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
                    Date = creation.Date,
                    Time = creation.Time,
                    TimeZone = creation.TimeZone,
                    Template = creation.Template,
                    Data = creation.Data,
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

        public IEnumerable<string> GetPersonalLogs(PersonalLogFilter filter)
        {
            IEnumerable<LogInfo> logInfos =
            [
                new(MyLogInfoKey.Template, filter.Template),
                new(MyLogInfoKey.Date, filter.Date),
                new(MyLogInfoKey.Time, filter.Time),
                new(MyLogInfoKey.Localisation, filter.Localisation),
                new(MyLogInfoKey.Count, filter.Count)
            ];

            logger.Info(
                MyOperation.GetPersonalLogs,
                OperationStatus.Started,
                logInfos);

            try
            {
                IEnumerable<PersonalLogEntity> logs = repository.GetAll();

                if (!string.IsNullOrWhiteSpace(filter.Date))
                {
                    logs = logs.Where(log => DoesFieldMatch(log.Date, filter.Date));
                }

                if (!string.IsNullOrWhiteSpace(filter.Time))
                {
                    logs = logs.Where(log => DoesFieldMatch(log.Time, filter.Time));
                }

                if (!string.IsNullOrWhiteSpace(filter.Template))
                {
                    logs = logs.Where(log => DoesFieldMatch(log.Template, filter.Template));
                }

                logs = FilterByRequestData(logs, filter.Data);

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
                    .Take(filter.Count);

                return BuildLogTexts(sorted, filter.Localisation);
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

        private IEnumerable<string> BuildLogTexts(IEnumerable<PersonalLogEntity> logs, string localisation)
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

        public void UpdatePersonalLog(PersonalLogUpdate update)
        {
            IEnumerable<LogInfo> logInfos =
            [
                new(MyLogInfoKey.Identifier, update.Identifier),
                new(MyLogInfoKey.Date, update.Date),
                new(MyLogInfoKey.Time, update.Time),
                new(MyLogInfoKey.TimeZone, update.TimeZone),
                new(MyLogInfoKey.Template, update.Template)
            ];

            logger.Info(
                MyOperation.UpdatePersonalLog,
                OperationStatus.Started,
                logInfos);

            try
            {
                PersonalLogEntity personalLog = repository.Get(update.Identifier);

                if (update.Date is not null)
                {
                    personalLog.Date = update.Date;
                }

                if (update.Time is not null)
                {
                    personalLog.Time = update.Time;
                }

                if (update.TimeZone is not null)
                {
                    personalLog.TimeZone = update.TimeZone;
                }

                if (update.Template is not null)
                {
                    personalLog.Template = update.Template;
                }

                if (update.Data is not null)
                {
                    personalLog.Data ??= [];

                    foreach (string parameter in update.Data.Keys)
                    {
                        personalLog.Data[parameter] = update.Data[parameter];
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

        public void DeletePersonalLog(string id)
        {
            IEnumerable<LogInfo> logInfos =
            [
                new(MyLogInfoKey.Identifier, id)
            ];

            logger.Info(
                MyOperation.DeletePersonalLog,
                OperationStatus.Started,
                logInfos);

            try
            {
                repository.Remove(id);
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

        private static bool DoesFieldMatch(string input, string pattern)
            => DoesFieldMatch(input, pattern, RegexOptions.None);

        private static bool DoesFieldMatch(
            string input,
            string pattern,
            RegexOptions options)
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