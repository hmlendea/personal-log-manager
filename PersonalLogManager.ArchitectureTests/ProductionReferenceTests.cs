using System;
using System.IO;
using System.Linq;
using System.Reflection;
using System.Runtime.CompilerServices;
using System.Xml.Linq;

using NUnit.Framework;

namespace PersonalLogManager.ArchitectureTests
{
    [TestFixture]
    public sealed class ProductionReferenceTests
    {
        [Test]
        public void GivenProductionProjects_WhenInspectingReferences_ThenTestsAreNotReferenced()
        {
            string[] projectPaths = Directory.GetFiles(
                Path.Combine(TestContext.CurrentContext.TestDirectory, "ProductionProjects"),
                "*.csproj");

            Assert.That(projectPaths, Is.Not.Empty, "Production project reference checks must not be empty.");

            foreach (string projectPath in projectPaths)
            {
                XDocument project = XDocument.Load(projectPath);
                string[] forbiddenReferences = project.Descendants()
                    .Where(element => element.Name.LocalName is "ProjectReference" or "PackageReference" or "Reference")
                    .Select(element => element.Attribute("Include")?.Value ?? string.Empty)
                    .Where(IsTestDependency)
                    .ToArray();

                Assert.That(
                    forbiddenReferences,
                    Is.Empty,
                    $"{Path.GetFileName(projectPath)} must not declare test-project or test-framework dependencies.");
            }
        }

        [Test]
        public void GivenTheProductionAssembly_WhenInspectingReferences_ThenTestsAreNotReferenced()
        {
            Assembly productionAssembly = typeof(Program).Assembly;
            AssemblyName[] references = productionAssembly.GetReferencedAssemblies();

            Assert.That(references, Is.Not.Empty);
            Assert.That(
                references.Where(reference => IsTestDependency(reference.Name ?? string.Empty)),
                Is.Empty,
                "The runtime assembly must not reference tests or architecture tooling.");
        }

        [Test]
        public void GivenTheProductionAssembly_WhenInspectingVisibility_ThenInternalsAreNotExposed()
            => Assert.That(
                typeof(Program).Assembly.GetCustomAttributes<InternalsVisibleToAttribute>(),
                Is.Empty,
                "The production assembly must not expose internal members to friend assemblies.");

        private static bool IsTestDependency(string referenceName) =>
            referenceName.Contains("Tests", StringComparison.OrdinalIgnoreCase) ||
            referenceName.Contains("NUnit", StringComparison.OrdinalIgnoreCase) ||
            referenceName.Contains("TestPlatform", StringComparison.OrdinalIgnoreCase) ||
            referenceName.Contains("Test.Sdk", StringComparison.OrdinalIgnoreCase) ||
            referenceName.Contains("ArchUnit", StringComparison.OrdinalIgnoreCase) ||
            referenceName.Contains("Moq", StringComparison.OrdinalIgnoreCase);
    }
}