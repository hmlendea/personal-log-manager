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
            CreateMap<TextLogEntity, TextLog>();
            CreateMap<StoreTextLogRequest, TextLog>()
                .ForMember(dest => dest.ActivityDateTime, opt => opt.MapFrom(src => DateTime.Parse(src.ActivityDateTime)));
        }
    }
}
