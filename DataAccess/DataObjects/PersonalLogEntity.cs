using System.Collections.Generic;
using NuciDAL.DataObjects;

namespace PersonalLogManager.DataAccess.DataObjects
{
    public class PersonalLogEntity : EntityBase
    {
        public string Date { get; set; }

        public string Time { get; set; }

        public string TimeZone { get; set; }

        public string CreatedDT { get; set; }

        public string UpdatedDT { get; set; }

        public string Template { get; set; }

        public Dictionary<string, string> Data { get; set; }
    }
}
