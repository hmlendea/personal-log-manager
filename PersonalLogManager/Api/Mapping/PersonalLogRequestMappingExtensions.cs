using PersonalLogManager.Api.Models;
using PersonalLogManager.Service.Models;

namespace PersonalLogManager.Api.Mapping
{
    static class PersonalLogRequestMappingExtensions
    {
        internal static PersonalLogCreation ToServiceModel(this StoreLogRequest request) => new()
        {
            Date = request.Date,
            Time = request.Time,
            TimeZone = request.TimeZone,
            Template = request.Template,
            Data = request.Data
        };

        internal static PersonalLogFilter ToServiceModel(this GetLogRequest request) => new()
        {
            Date = request.Date,
            Time = request.Time,
            Template = request.Template,
            Localisation = request.Localisation,
            Data = request.Data,
            Count = request.Count
        };

        internal static PersonalLogUpdate ToServiceModel(this UpdateLogRequest request) => new()
        {
            Identifier = request.Identifier,
            Date = request.Date,
            Time = request.Time,
            TimeZone = request.TimeZone,
            Template = request.Template,
            Data = request.Data
        };
    }
}
