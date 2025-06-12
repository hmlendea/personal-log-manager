using AutoMapper;

using PersonalLogManager.DataAccess.DataObjects;
using PersonalLogManager.Service.Models;

namespace PersonalLogManager.DataAccess
{
    public class DataAccessMappingProfile : Profile
    {
        public DataAccessMappingProfile()
        {
            CreateMap<TextLog, TextLogEntity>()
                .ForMember(dest => dest.Date, opt => opt.MapFrom(src => src.Date.ToString("yyyy-MM-dd")))
                .ForMember(dest => dest.Time, opt => opt.MapFrom(src => src.Time.HasValue ? src.Time.Value.ToString("HH:mm:ss") : null))
                .ForMember(dest => dest.CreatedDT, opt => opt.MapFrom(src => src.CreatedDateTime.ToString("o")))
                .ForMember(dest => dest.UpdatedDT, opt => opt.MapFrom(src => src.UpdatedDateTime.HasValue ? src.UpdatedDateTime.Value.ToString("o") : null));
        }
    }
}
