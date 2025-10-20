using System;
using AutoMapper;

using PersonalLogManager.Api.Models;
using PersonalLogManager.DataAccess.DataObjects;
using PersonalLogManager.Service.Models;

namespace PersonalLogManager.Service
{
    public class ServiceMappingProfile : Profile
    {
        public ServiceMappingProfile()
        {
            CreateMap<StoreLogRequest, PersonalLog>()
                .ForCtorParam("date", opt => opt.MapFrom(src => DateOnly.Parse(src.Date)))
                .ForCtorParam("time", opt => opt.MapFrom(src => string.IsNullOrWhiteSpace(src.Time) ? (TimeOnly?)null : TimeOnly.Parse(src.Time)))
                .ForMember("Template", opt => opt.MapFrom(src => Enum.Parse<PersonalLogTemplate>(src.Template)));

            CreateMap<PersonalLogEntity, PersonalLog>()
                .ForCtorParam("date", opt => opt.MapFrom(src => DateOnly.Parse(src.Date)))
                .ForCtorParam("time", opt => opt.MapFrom(src => string.IsNullOrWhiteSpace(src.Time) ? (TimeOnly?)null : TimeOnly.Parse(src.Time)))
                .ForMember("Template", opt => opt.MapFrom(src => Enum.Parse<PersonalLogTemplate>(src.Template)));
        }
    }
}
