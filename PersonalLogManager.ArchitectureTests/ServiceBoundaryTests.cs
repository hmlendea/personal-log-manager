using ArchUnitNET.Domain;
using ArchUnitNET.NUnit;

using NUnit.Framework;

using static ArchUnitNET.Fluent.ArchRuleDefinition;

namespace PersonalLogManager.ArchitectureTests
{
    [TestFixture]
    public sealed class ServiceBoundaryTests
    {
        [TestCase("PersonalLogManager.Api.Controllers", "PersonalLogManager.Service.PersonalLogService")]
        [TestCase("PersonalLogManager.Service", "PersonalLogManager.Service.TextBuilding.PersonalLogTextBuilderFactory")]
        [TestCase("PersonalLogManager.Service", "PersonalLogManager.Service.TextBuilding.Localisation.EnglishTextBuilder")]
        [TestCase("PersonalLogManager.Service", "PersonalLogManager.Service.TextBuilding.Localisation.RomanianTextBuilder")]
        public void GivenAnOrchestrator_WhenInspectingCollaborators_ThenConcreteImplementationsAreNotUsed(
            string sourceNamespace,
            string forbiddenTypeName)
        {
            IObjectProvider<IType> sourceTypes = Types().That().ResideInNamespace(sourceNamespace);
            IObjectProvider<IType> forbiddenTypes = Types().That().HaveFullName(forbiddenTypeName);

            Assert.That(sourceTypes.GetObjects(ProductionArchitecture.Model), Is.Not.Empty);
            Assert.That(forbiddenTypes.GetObjects(ProductionArchitecture.Model), Is.Not.Empty);

            Types().That().Are(sourceTypes).Should().NotDependOnAny(forbiddenTypes)
                .Because("Controllers and use cases must consume abstractions, not concrete adapters.")
                .Check(ProductionArchitecture.Model);
        }

        [TestCase("NuciDAL.Repositories.JsonRepository`1")]
        [TestCase("NuciLog.NuciLogger")]
        [TestCase("System.IO.File")]
        [TestCase("System.Text.Json.JsonSerializer")]
        [TestCase("Newtonsoft.Json.JsonConvert")]
        public void GivenTheService_WhenInspectingInfrastructureDependencies_ThenAdaptersRemainExternal(
            string forbiddenTypeName)
        {
            IObjectProvider<IType> serviceTypes = Types().That()
                .ResideInNamespace("PersonalLogManager.Service");

            Assert.That(serviceTypes.GetObjects(ProductionArchitecture.Model), Is.Not.Empty);

            Types().That().Are(serviceTypes).Should().NotDependOnAny(
                Types().That().HaveFullName(forbiddenTypeName))
                .Because("Persistence, logging, and JSON mechanics belong behind injected abstractions.")
                .Check(ProductionArchitecture.Model);
        }

        [TestCase("PersonalLogManager.Api.Controllers", "PersonalLogManager.Service.IPersonalLogService")]
        [TestCase("PersonalLogManager.Service", "PersonalLogManager.Service.TextBuilding.IPersonalLogTextBuilderFactory")]
        public void GivenAnOrchestrator_WhenInspectingDependencies_ThenTheDocumentedPortIsUsed(
            string sourceNamespace,
            string contractTypeName)
        {
            IObjectProvider<IType> sourceTypes = Classes().That().ResideInNamespace(sourceNamespace);
            IObjectProvider<IType> contractTypes = Types().That().HaveFullName(contractTypeName);

            Assert.That(sourceTypes.GetObjects(ProductionArchitecture.Model), Is.Not.Empty);
            Assert.That(contractTypes.GetObjects(ProductionArchitecture.Model), Is.Not.Empty);

            Classes().That().Are(sourceTypes).Should().DependOnAny(contractTypes)
                .Because("Use cases and controllers must cross the documented abstraction boundary.")
                .Check(ProductionArchitecture.Model);
        }
    }
}