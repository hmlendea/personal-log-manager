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
        IRepository<TextLogEntity> textLogRepository,
        IMapper mapper) : IPersonalLogService
    {
        public GetLogResponse GetLogs(GetLogRequest request)
        {
            IEnumerable<TextLogEntity> logs = textLogRepository.GetAll();

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
                Logs = [.. logs.Take(request.Count).Select(mapper.Map<GetLogResponseObject>)]
            };
        }

        public void StoreTextLog(StoreTextLogRequest request)
            => StorePersonalLog(mapper.Map<TextLog>(request));

        void StorePersonalLog(PersonalLog personalLog)
        {
            ArgumentNullException.ThrowIfNull(personalLog);

            textLogRepository.Add(mapper.Map<TextLogEntity>(personalLog));
            textLogRepository.ApplyChanges();
        }
    }
}
