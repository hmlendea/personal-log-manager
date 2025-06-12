using NuciDAL.DataObjects;

namespace PersonalLogManager.DataAccess.DataObjects
{
    public abstract class PersonalLogEntity : EntityBase
    {
        public string Date { get; set; }

        public string Time { get; set; }

        public string CreatedDT { get; set; }

        public string UpdatedDT { get; set; }
    }
}
