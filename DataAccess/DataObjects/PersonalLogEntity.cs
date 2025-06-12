using NuciDAL.DataObjects;

namespace PersonalLogManager.DataAccess.DataObjects
{
    public abstract class PersonalLogEntity : EntityBase
    {
        public string ActivityDateTime { get; set; }

        public string AddedDateTime { get; set; }

        public string UpdatedDateTime { get; set; }
    }
}
