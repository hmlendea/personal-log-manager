using System.Linq;
using System.Text.RegularExpressions;

using ArchUnitNET.Domain;
using ArchUnitNET.NUnit;

using NUnit.Framework;

using static ArchUnitNET.Fluent.ArchRuleDefinition;

namespace PersonalLogManager.ArchitectureTests
{
    [TestFixture]
    public sealed class LayerDependencyTests
    {
        [TestCase("PersonalLogManager.Api", "PersonalLogManager.DataAccess")]
        [TestCase("PersonalLogManager.Api", "PersonalLogManager.Service.Models")]
        [TestCase("PersonalLogManager.Api", "PersonalLogManager.Service.Mapping")]
        [TestCase("PersonalLogManager.Api", "PersonalLogManager.Service.TextBuilding")]
        [TestCase("PersonalLogManager.Api", "NuciDAL")]
        [TestCase("PersonalLogManager.Api", "Microsoft.EntityFrameworkCore")]
        [TestCase("PersonalLogManager.Api", "Amazon.DynamoDBv2")]
        [TestCase("PersonalLogManager.Service.Models", "PersonalLogManager.Api")]
        [TestCase("PersonalLogManager.Service.Models", "PersonalLogManager.DataAccess")]
        [TestCase("PersonalLogManager.Service.Models", "PersonalLogManager.Service.TextBuilding")]
        [TestCase("PersonalLogManager.Service.Models", "PersonalLogManager.Service.Mapping")]
        [TestCase("PersonalLogManager.Service.Models", "Microsoft.AspNetCore")]
        [TestCase("PersonalLogManager.Service.Models", "NuciDAL")]
        [TestCase("PersonalLogManager.Service.Models", "Microsoft.EntityFrameworkCore")]
        [TestCase("PersonalLogManager.Service.Models", "Amazon.DynamoDBv2")]
        [TestCase("PersonalLogManager.Service.Models", "Dapper")]
        [TestCase("PersonalLogManager.Service.Models", "MediatR")]
        [TestCase("PersonalLogManager.DataAccess", "PersonalLogManager.Api")]
        [TestCase("PersonalLogManager.DataAccess", "PersonalLogManager.Service")]
        [TestCase("PersonalLogManager.Service.Mapping", "PersonalLogManager.Api")]
        [TestCase("PersonalLogManager.Service.Mapping", "PersonalLogManager.Service.TextBuilding")]
        [TestCase("PersonalLogManager.Service.TextBuilding", "PersonalLogManager.Api")]
        [TestCase("PersonalLogManager.Service.TextBuilding", "PersonalLogManager.DataAccess")]
        [TestCase("PersonalLogManager.Service.TextBuilding", "PersonalLogManager.Service.Mapping")]
        [TestCase("PersonalLogManager.Service.TextBuilding", "NuciDAL")]
        [TestCase("PersonalLogManager.Service.TextBuilding", "Microsoft.AspNetCore")]
        [TestCase("PersonalLogManager.Api.Models", "PersonalLogManager.Service")]
        [TestCase("PersonalLogManager.Api.Models", "PersonalLogManager.Configuration")]
        [TestCase("PersonalLogManager.Configuration", "PersonalLogManager.Api")]
        [TestCase("PersonalLogManager.Configuration", "PersonalLogManager.Service")]
        [TestCase("PersonalLogManager.Configuration", "PersonalLogManager.DataAccess")]
        public void GivenALayer_WhenInspectingDependencies_ThenTheBoundaryIsRespected(
            string sourceNamespace,
            string forbiddenNamespace)
        {
            IObjectProvider<IType> sourceTypes = Types().That()
                .ResideInNamespaceMatching($"^{Regex.Escape(sourceNamespace)}(\\.|$)");

            Assert.That(
                sourceTypes.GetObjects(ProductionArchitecture.Model),
                Is.Not.Empty,
                $"The {sourceNamespace} boundary must select production types.");

            Types().That().Are(sourceTypes).Should().NotDependOnAny(
                Types().That().ResideInNamespaceMatching($"^{Regex.Escape(forbiddenNamespace)}(\\.|$)"))
                .Because($"{sourceNamespace} must remain independent of {forbiddenNamespace}.")
                .Check(ProductionArchitecture.Model);
        }

        [TestCase("PersonalLogManager.Api")]
        [TestCase("PersonalLogManager.Service")]
        [TestCase("PersonalLogManager.DataAccess")]
        public void GivenALayer_WhenInspectingHostDependencies_ThenCompositionRemainsExternal(
            string sourceNamespace)
        {
            IObjectProvider<IType> sourceTypes = Types().That()
                .ResideInNamespaceMatching($"^{Regex.Escape(sourceNamespace)}(\\.|$)");
            IType[] selectedTypes = sourceTypes
                .GetObjects(ProductionArchitecture.Model).ToArray();

            Assert.That(selectedTypes, Is.Not.Empty);

            Types().That().Are(sourceTypes)
                .Should().NotDependOnAny(Types().That().ResideInNamespace("PersonalLogManager"))
                .Because("Host composition must depend on layers, not the reverse.")
                .Check(ProductionArchitecture.Model);
        }
    }
}