using System.Text.Json.Serialization;
using NuciAPI.Requests;
using NuciSecurity.HMAC;

namespace PersonalLogManager.Api.Models
{
    public class DeleteLogRequest : NuciApiRequest
    {
        [HmacOrder(1)]
        [JsonPropertyName("id")]
        public string Identifier { get; set; }
    }
}
