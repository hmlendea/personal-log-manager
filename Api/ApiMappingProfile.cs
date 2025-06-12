using AutoMapper;
using PersonalLogManager.Api.Models;
using PersonalLogManager.DataAccess.DataObjects;

namespace PersonalLogManager.Api
{
    public class ApiMappingProfile : Profile
    {
        public ApiMappingProfile()
        {
            CreateMap<TextLogEntity, GetLogResponseObject>();
        }
    }
}
