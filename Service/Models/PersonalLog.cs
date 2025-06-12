using System;

namespace PersonalLogManager.Service.Models
{
    public class PersonalLog(DateTime activityDateTime)
    {
        public string Id { get; set; } = Guid.NewGuid().ToString();

        public DateTime ActivityDateTime { get; set; } = activityDateTime;

        public DateTime AddedDateTime { get; set; } = DateTime.UtcNow;

        public DateTime? UpdatedDateTime { get; set; } = null;
    }
}
