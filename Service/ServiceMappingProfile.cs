using System;
using AutoMapper;

using PersonalLogManager.Api.Models;
using PersonalLogManager.Service.Models;

namespace PersonalLogManager.Service
{
    public class ServiceMappingProfile : Profile
    {
        public ServiceMappingProfile()
        {
            CreateMap<StoreTextLogRequest, TextLog>()
                .ForCtorParam("date", opt => opt.MapFrom(src => DateOnly.Parse(src.Date)))
                .ForCtorParam("time", opt => opt.MapFrom(src => string.IsNullOrWhiteSpace(src.Time) ? (TimeOnly?)null : TimeOnly.Parse(src.Time)));
        }
    }
}
