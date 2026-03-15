using System;
using System.Collections.Generic;
using System.Linq;
using System.Text.RegularExpressions;
using NuciDAL.Repositories;
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

            string id = $"L{random.Next(0, 1000000000):D9}";

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

                if (request.Data is not null && request.Data.Count > 0)
                {
                    foreach (string dataKey in request.Data.Keys)
                    {
                        logs = logs.Where(log =>
                            log.Data is not null &&
                            log.Data.ContainsKey(dataKey) &&
                            log.Data[dataKey] is not null &&
                            DoesFieldMatch(
                                log.Data[dataKey],
                                request.Data[dataKey],
                                RegexOptions.IgnoreCase));
                    }
                }

                logger.Debug(
                    MyOperation.GetPersonalLogs,
                    OperationStatus.Success,
                    logInfos,
                    new LogInfo(MyLogInfoKey.Count, logs.Count()));

                return new GetLogResponse()
                {
                    Logs = [.. logs
                        .OrderByDescending(log => log.Date)
                        .ThenByDescending(log => log.Time)
                        .ThenBy(log => log.Template)
                        .ThenBy(log => log.CreatedDT)
                        .Take(request.Count)
                        .Select(log => $"{log.Id} " + logTextBuilder.BuildLogText(log.ToDomainModel(), request.Localisation))]
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

                if (request.Data is not null)
                {
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
            string anchoredPattern = pattern;
            if (input is null || pattern is null)
            {
                return false;
            }

            if (!pattern.StartsWith("^"))
            {
                anchoredPattern = "^" + pattern;
            }

            if (!pattern.EndsWith("$"))
            {
                anchoredPattern += "$";
            }

            return Regex.IsMatch(input, anchoredPattern, options);
        }
    }
}