using System;
using System.Linq;
using System.Reflection;

using NUnit.Framework;

using PersonalLogManager.Service.Models;
using PersonalLogManager.Service.TextBuilding;

namespace PersonalLogManager.ArchitectureTests
{
    [TestFixture]
    public sealed class TextBuilderContractTests
    {
        [Test]
        public void GivenLocalisedBuilders_WhenInspectingTemplates_ThenEveryDispatchMethodIsPublic()
        {
            Type[] builderTypes = typeof(Program).Assembly.GetExportedTypes()
                .Where(type => !type.IsAbstract && typeof(IPersonalLogTextBuilder).IsAssignableFrom(type))
                .ToArray();
            PersonalLogTemplate[] templates = Enum.GetValues<PersonalLogTemplate>();

            Assert.That(builderTypes, Is.Not.Empty, "At least one production localisation must be selected.");
            Assert.That(templates, Is.Not.Empty, "Template dispatch checks must select existing templates.");

            using (Assert.EnterMultipleScope())
            {
                foreach (Type builderType in builderTypes)
                {
                    foreach (PersonalLogTemplate template in templates)
                    {
                        AssertTemplateMethod(builderType, template);
                    }
                }
            }
        }

        private static void AssertTemplateMethod(Type builderType, PersonalLogTemplate template)
        {
            string methodName = $"Build{template}LogText";
            MethodInfo? method = builderType.GetMethod(
                methodName,
                BindingFlags.Public | BindingFlags.Instance,
                [typeof(PersonalLog)]);

            Assert.That(
                method,
                Is.Not.Null,
                $"{builderType.Name} must expose {methodName}(PersonalLog) for factory dispatch.");
            Assert.That(
                method?.ReturnType,
                Is.EqualTo(typeof(string)),
                $"{builderType.Name}.{methodName} must return rendered text.");
        }
    }
}