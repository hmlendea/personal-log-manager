using System;

namespace PersonalLogManager.Service.Models
{
    public sealed class TextLog(DateTime activityDateTime) : PersonalLog(activityDateTime)
    {
        public string Content { get; set; }
    }
}
