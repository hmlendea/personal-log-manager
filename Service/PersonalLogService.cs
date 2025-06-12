using System;
using AutoMapper;
using NuciDAL.Repositories;
using PersonalLogManager.Api.Models;
using PersonalLogManager.DataAccess.DataObjects;
using PersonalLogManager.Service.Models;

namespace PersonalLogManager.Service
{
    public class PersonalLogService(
        IRepository<TextLogEntity> personalLogRepository,
        IMapper mapper) : IPersonalLogService
    {
        public void StoreTextLog(StoreTextLogRequest request)
            => StorePersonalLog(mapper.Map<TextLog>(request));

        void StorePersonalLog(PersonalLog personalLog)
        {
            ArgumentNullException.ThrowIfNull(personalLog);

            personalLogRepository.Add(mapper.Map<TextLogEntity>(personalLog));
            personalLogRepository.ApplyChanges();
        }
    }
}
