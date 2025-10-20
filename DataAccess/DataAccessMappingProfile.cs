using AutoMapper;

using PersonalLogManager.DataAccess.DataObjects;
using PersonalLogManager.Service.Models;

namespace PersonalLogManager.DataAccess
{
    public class DataAccessMappingProfile : Profile
    {
        public DataAccessMappingProfile()
        {
            CreateMap<PersonalLog, PersonalLogEntity>()
                .ForMember(dest => dest.Date, opt => opt.MapFrom(src => src.Date.ToString("yyyy-MM-dd")))
                .ForMember(dest => dest.Time, opt => opt.MapFrom(src => src.Time.Equals(null) ? src.Time.Value.ToString("HH:mm") : null))
                .ForMember(dest => dest.CreatedDT, opt => opt.MapFrom(src => src.CreatedDateTime.ToString("o")))
                .ForMember(dest => dest.UpdatedDT, opt => opt.MapFrom(src => src.UpdatedDateTime.Equals(null) ? src.UpdatedDateTime.Value.ToString("o") : null));
        }
    }
}
