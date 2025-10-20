using System;
using System.Collections.Generic;
using System.Linq;
using AutoMapper;
using NuciDAL.Repositories;
using PersonalLogManager.Api.Models;
using PersonalLogManager.DataAccess.DataObjects;
using PersonalLogManager.Service.Models;

namespace PersonalLogManager.Service
{
    public class PersonalLogService(
        IPersonalLogTextBuilder logTextBuilder,
        IFileRepository<PersonalLogEntity> logRepository,
        IMapper mapper) : IPersonalLogService
    {
        private readonly Random random = new();

        public GetLogResponse GetLogs(GetLogRequest request)
        {
            IEnumerable<PersonalLogEntity> logs = logRepository.GetAll();

            if (!string.IsNullOrWhiteSpace(request.Date))
            {
                logs = logs.Where(log => log.Date.Equals(request.Date));
            }

            if (!string.IsNullOrWhiteSpace(request.Time))
            {
                logs = logs.Where(log => log.Time.Equals(request.Time));
            }

            return new GetLogResponse()
            {
                Logs = [.. logs
                    .OrderBy(log => log.Date)
                    .Take(request.Count)
                    .Select(log => $"{log.Id} " + logTextBuilder.BuildLogText(mapper.Map<PersonalLog>(log)))]
            };
        }

        public void StorePersonalLog(StoreLogRequest request)
        {
            PersonalLog personalLog = mapper.Map<PersonalLog>(request);
            PersonalLogEntity personalLogEntity = mapper.Map<PersonalLogEntity>(personalLog);
            personalLogEntity.Id = $"LOG{random.Next(0, 1000000):D7}";

            logRepository.Add(personalLogEntity);
            logRepository.ApplyChanges();
        }
    }
}
