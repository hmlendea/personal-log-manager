using System;

namespace PersonalLogManager.Service.Models
{
    public class PersonalLog(DateOnly date, TimeOnly? time)
    {
        public string Id { get; set; } = Guid.NewGuid().ToString();

        public DateOnly Date { get; set; } = date;

        public TimeOnly? Time { get; set; } = time;

        public DateTime CreatedDateTime { get; set; } = DateTime.Now;

        public DateTime? UpdatedDateTime { get; set; } = DateTime.Now;

        public PersonalLog(DateOnly date) : this(date, null) { }
    }
}
