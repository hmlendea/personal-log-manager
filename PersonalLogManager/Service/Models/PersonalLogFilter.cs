using System.Collections.Generic;

namespace PersonalLogManager.Service.Models
{
    public sealed class PersonalLogFilter
    {
        public string Date { get; set; }

        public string Time { get; set; }

        public string Template { get; set; }

        public string Localisation { get; set; } = "en";

        public Dictionary<string, string> Data { get; set; }

        public int Count { get; set; } = 1;
    }
}
