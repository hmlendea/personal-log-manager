using NuciAPI.Requests;

namespace PersonalLogManager.Api.Models
{
    public class GetLogByIdRequest : NuciApiRequest
    {
        public string Identifier { get; set; }
    }
}
