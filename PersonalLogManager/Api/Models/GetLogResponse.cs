using System.Collections.Generic;
using System.Linq;

using NuciAPI.Responses;
using NuciSecurity.HMAC;

namespace PersonalLogManager.Api.Models
{
    public class GetLogResponse : NuciApiSuccessResponse
    {
        [HmacOrder(1)]
        public IEnumerable<string> Logs { get; set; } = [];

        [HmacOrder(2)]
        public int Count => Logs.Count();
    }
}
