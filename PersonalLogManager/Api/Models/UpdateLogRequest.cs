using System.Collections.Generic;
using NuciAPI.Requests;
using NuciSecurity.HMAC;

namespace PersonalLogManager.Api.Models
{
    public class UpdateLogRequest : NuciApiRequest
    {
        public string Identifier { get; set; }

        [HmacOrder(1)]
        public string Date { get; set; }

        [HmacOrder(2)]
        public string Time { get; set; }

        [HmacOrder(3)]
        public string TimeZone { get; set; }

        [HmacOrder(4)]
        public string Template { get; set; }

        [HmacOrder(5)]
        public Dictionary<string, string> Data { get; set; }
    }
}
