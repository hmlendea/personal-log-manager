using System;

namespace PersonalLogManager.Service.Models
{
    public sealed class TextLog(DateOnly date, TimeOnly? time) : PersonalLog(date, time)
    {
        public string Content { get; set; }

        public TextLog(DateOnly date) : this(date, null) { }
    }
}
