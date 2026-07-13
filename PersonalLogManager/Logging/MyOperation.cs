using NuciLog.Core;

namespace PersonalLogManager.Logging
{
    public sealed class MyOperation : Operation
    {
        MyOperation(string name) : base(name) { }

        public static Operation StorePersonalLog => new MyOperation(nameof(StorePersonalLog));

        public static Operation GetPersonalLogs => new MyOperation(nameof(GetPersonalLogs));

        public static Operation UpdatePersonalLog => new MyOperation(nameof(UpdatePersonalLog));

        public static Operation DeletePersonalLog => new MyOperation(nameof(DeletePersonalLog));
    }
}
