using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Text.Json.Serialization;
using NuciAPI.Requests;
using NuciSecurity.HMAC;

namespace PersonalLogManager.Api.Models
{
    public class UpdateLogRequest : NuciApiRequest
    {
        [Required]
        [HmacOrder(1)]
        [JsonPropertyName("id")]
        public string Identifier { get; set; }

        [HmacOrder(2)]
        public string Date { get; set; }

        [HmacOrder(3)]
        public string Time { get; set; }

        [HmacOrder(4)]
        public string TimeZone { get; set; }

        [HmacOrder(5)]
        public string Template { get; set; }

        [HmacOrder(6)]
        public Dictionary<string, string> Data { get; set; }
    }
}
