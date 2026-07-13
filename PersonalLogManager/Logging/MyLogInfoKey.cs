using NuciLog.Core;

namespace PersonalLogManager.Logging
{
    public sealed class MyLogInfoKey : LogInfoKey
    {
        MyLogInfoKey(string name) : base(name) { }

        public static LogInfoKey Identifier => new MyLogInfoKey(nameof(Identifier));

        public static LogInfoKey Template => new MyLogInfoKey(nameof(Template));

        public static LogInfoKey Date => new MyLogInfoKey(nameof(Date));

        public static LogInfoKey Time => new MyLogInfoKey(nameof(Time));

        public static LogInfoKey TimeZone => new MyLogInfoKey(nameof(TimeZone));

        public static LogInfoKey Localisation => new MyLogInfoKey(nameof(Localisation));

        public static LogInfoKey Count => new MyLogInfoKey(nameof(Count));
    }
}
