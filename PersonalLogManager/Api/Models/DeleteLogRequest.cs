using NuciAPI.Requests;

namespace PersonalLogManager.Api.Models
{
    public class DeleteLogRequest : NuciApiRequest
    {
        public string Identifier { get; set; }
    }
}
