using ArchUnitNET.Domain;
using ArchUnitNET.Loader;

namespace PersonalLogManager.ArchitectureTests
{
    internal static class ProductionArchitecture
    {
        internal static readonly Architecture Model = new ArchLoader()
            .LoadAssemblies(typeof(Program).Assembly)
            .Build();
    }
}