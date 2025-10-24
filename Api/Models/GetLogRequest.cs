using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;

namespace PersonalLogManager.Api.Models
{
    public class GetLogRequest
    {
        public string Date { get; set; }

        public string Time { get; set; }

        public string Template { get; set; }

        public Dictionary<string, string> Data { get; set; }

        [Range(1, 10000)]
        public int Count { get; set; } = 1;
    }
}
