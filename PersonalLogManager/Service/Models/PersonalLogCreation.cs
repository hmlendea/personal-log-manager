using System.Collections.Generic;

namespace PersonalLogManager.Service.Models
{
    public sealed class PersonalLogCreation
    {
        public string Date { get; set; }

        public string Time { get; set; }

        public string TimeZone { get; set; }

        public string Template { get; set; }

        public Dictionary<string, string> Data { get; set; }
    }
}
