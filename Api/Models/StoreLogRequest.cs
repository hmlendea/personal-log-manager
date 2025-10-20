using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;

namespace PersonalLogManager.Api.Models
{
    public class StoreLogRequest
    {
        [Required]
        public string Date { get; set; }

        public string Time { get; set; }

        public string TimeZone { get; set; }

        public string Template { get; set; }

        public Dictionary<string, string> Data { get; set; }
    }
}
