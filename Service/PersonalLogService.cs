using System;
using System.Collections.Generic;
using System.Linq;
using AutoMapper;
using NuciDAL.Repositories;
using PersonalLogManager.Api.Models;
using PersonalLogManager.Configuration;
using PersonalLogManager.DataAccess.DataObjects;
using PersonalLogManager.Service.Models;
using PersonalLogManager.Service.TextBuilding;

namespace PersonalLogManager.Service
{
    public class PersonalLogService(
        IPersonalLogTextBuilderFactory logTextBuilder,
        IFileRepository<PersonalLogEntity> repository,
        SecuritySettings securitySettings,
        IMapper mapper) : IPersonalLogService
    {
        private readonly Random random = new();

        public void StorePersonalLog(StoreLogRequest request)
        {
            request.ValidateHMAC(securitySettings.SharedSecretKey);

            repository.Add(new()
            {
                Id = $"L{random.Next(0, 1000000000):D9}",
                Date = request.Date,
                Time = request.Time,
                TimeZone = request.TimeZone,
                Template = request.Template,
                Data = request.Data,
                CreatedDT = DateTime.UtcNow.ToString("o")
            });

            repository.ApplyChanges();
        }

        public GetLogResponse GetPersonalLogs(GetLogRequest request)
        {
            request.ValidateHMAC(securitySettings.SharedSecretKey);

            IEnumerable<PersonalLogEntity> logs = repository.GetAll();

            if (!string.IsNullOrWhiteSpace(request.Date))
            {
                logs = logs.Where(log => log.Date.Equals(request.Date));
            }

            if (!string.IsNullOrWhiteSpace(request.Time))
            {
                logs = logs.Where(log => log.Time.Equals(request.Time));
            }

            if (!string.IsNullOrWhiteSpace(request.Template))
            {
                logs = logs.Where(log => log.Template.Equals(request.Template));
            }

            if (request.Data is not null && request.Data.Count > 0)
            {
                foreach (string dataKey in request.Data.Keys)
                {
                    logs = logs.Where(log => log.Data.ContainsKey(dataKey) && log.Data[dataKey].Equals(request.Data[dataKey], StringComparison.OrdinalIgnoreCase));
                }
            }

            return new GetLogResponse()
            {
                Logs = [.. logs
                    .OrderByDescending(log => log.Date)
                    .ThenByDescending(log => log.Time)
                    .ThenBy(log => log.Template)
                    .ThenBy(log => log.CreatedDT)
                    .Take(request.Count)
                    .Select(log => $"{log.Id} " + logTextBuilder.BuildLogText(mapper.Map<PersonalLog>(log)))]
            };
        }

        public void UpdatePersonalLog(UpdateLogRequest request)
        {
            request.ValidateHMAC(securitySettings.SharedSecretKey);

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
                    if (personalLog.Data.ContainsKey(parameter))
                    {
                        personalLog.Data[parameter] = request.Data[parameter];
                    }
                }
            }

            personalLog.UpdatedDT = DateTime.UtcNow.ToString("o");

            repository.Update(personalLog);
            repository.ApplyChanges();
        }

        public void DeletePersonalLog(DeleteLogRequest request)
        {
            request.ValidateHMAC(securitySettings.SharedSecretKey);
            repository.Remove(request.Identifier);
            repository.ApplyChanges();
        }
    }
}