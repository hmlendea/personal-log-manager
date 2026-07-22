using System.Collections.Generic;

using NuciAPI.Responses;
using NuciSecurity.HMAC;

namespace PersonalLogManager.Api.Models
{
    public class GetLogByIdResponse : NuciApiSuccessResponse
    {
        [HmacOrder(1)]
        public string Id { get; set; }

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

        [HmacOrder(7)]
        public string CreatedDateTime { get; set; }

        [HmacOrder(8)]
        public string UpdatedDateTime { get; set; }
    }
}
