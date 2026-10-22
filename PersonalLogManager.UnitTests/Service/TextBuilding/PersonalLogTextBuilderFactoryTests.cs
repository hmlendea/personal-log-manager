using System;
using System.Collections.Generic;

using Moq;

using NUnit.Framework;

using NuciText.Normalisation;
using NuciText.Obfuscation;

using PersonalLogManager.Service.Models;
using PersonalLogManager.Service.TextBuilding;

namespace PersonalLogManager.UnitTests.Service.TextBuilding
{
    [TestFixture]
    public class PersonalLogTextBuilderFactoryTests
    {
        Mock<INuciTextNormaliser> normaliserMock;
        Mock<INuciTextObfuscator> obfuscatorMock;
        PersonalLogTextBuilderFactory factory;

        [SetUp]
        public void SetUp()
        {
            normaliserMock = new Mock<INuciTextNormaliser>();
            normaliserMock
                .Setup(normaliser => normaliser.NormaliseSentence(It.IsAny<string>()))
                .Returns((string input) => input);

            obfuscatorMock = new Mock<INuciTextObfuscator>();
            obfuscatorMock
                .Setup(obfuscator => obfuscator.Deobfuscate(It.IsAny<string>()))
                .Returns((string input) => input);

            factory = new PersonalLogTextBuilderFactory(
                normaliserMock.Object,
                obfuscatorMock.Object);
        }

        // ── BuildLogText prefix ────────────────────────────────

        [Test]
        public void GivenLogWithoutTime_WhenBuildLogText_ThenPrefixContainsDateOnly()
        {
            PersonalLog log = BuildTextLog(
                new DateOnly(2012, 9, 5),
                null,
                "UTC",
                "Hello world");

            string result = factory.BuildLogText(log, "en");

            Assert.That(result, Does.StartWith("2012-09-05: "));
        }

        [Test]
        public void GivenLogWithTime_WhenBuildLogText_ThenPrefixContainsDateAndTime()
        {
            PersonalLog log = BuildTextLog(
                new DateOnly(2012, 9, 5),
                new TimeOnly(9, 30),
                "UTC",
                "Hello world");

            string result = factory.BuildLogText(log, "en");

            Assert.That(result, Does.StartWith("2012-09-05: 09:30 UTC: "));
        }

        [Test]
        public void GivenLogWithCustomTimeZone_WhenBuildLogText_ThenPrefixContainsTimeZone()
        {
            PersonalLog log = BuildTextLog(
                new DateOnly(2012, 9, 5),
                new TimeOnly(14, 0),
                "EET",
                "Hello world");

            string result = factory.BuildLogText(log, "en");

            Assert.That(result, Does.StartWith("2012-09-05: 14:00 EET: "));
        }

        // ── Localisation selection ─────────────────────────────

        [TestCase("ro")]
        [TestCase("ro-RO")]
        [TestCase("ro-MD")]
        public void GivenRomanianLocalisationVariant_WhenBuildLogText_ThenUsesRomanianBuilder(
            string localisation)
        {
            PersonalLog log = BuildActivationLog("Nucilandia");

            string result = factory.BuildLogText(log, localisation);

            Assert.That(result, Does.Contain("Am activat"));
        }

        [TestCase(null)]
        [TestCase("")]
        [TestCase("  ")]
        [TestCase("fr")]
        [TestCase("de")]
        [TestCase("en")]
        public void GivenNonRomanianLocalisation_WhenBuildLogText_ThenUsesEnglishBuilder(
            string localisation)
        {
            PersonalLog log = BuildActivationLog("Nucilandia");

            string result = factory.BuildLogText(log, localisation);

            Assert.That(result, Does.Contain("I have activated"));
        }

        // ── Template dispatch ──────────────────────────────────

        [Test]
        public void GivenTextTemplate_WhenBuildLogText_ThenCallsNormaliserOnLogText()
        {
            PersonalLog log = BuildTextLog(
                new DateOnly(2012, 9, 5),
                null,
                "UTC",
                "Hello world");

            factory.BuildLogText(log, "en");

            normaliserMock.Verify(
                normaliser => normaliser.NormaliseSentence(It.IsAny<string>()),
                Times.Once());
        }

        [Test]
        public void GivenTextTemplate_WhenBuildLogText_ThenResultContainsLogText()
        {
            PersonalLog log = BuildTextLog(
                new DateOnly(2012, 9, 5),
                null,
                "UTC",
                "Hello world");

            string result = factory.BuildLogText(log, "en");

            Assert.That(result, Does.Contain("Hello world"));
        }

        [Test]
        public void GivenUnknownTemplate_WhenBuildLogText_ThenThrowsMissingMethodException()
        {
            PersonalLog log = new(new DateOnly(2012, 9, 5))
            {
                Template = (PersonalLogTemplate)9999,
                Data = []
            };

            Assert.That(
                () => factory.BuildLogText(log, "en"),
                Throws.TypeOf<MissingMethodException>());
        }

        [TestCase("en", "", "My Devicius has disconnected from a Wi-Fi network")]
        [TestCase("en", "Nucilandia", "My Devicius has disconnected from the 'Nucilandia' Wi-Fi network")]
        [TestCase("ro", "", "Devicius s-a deconectat de la o rețea Wi-Fi")]
        [TestCase("ro", "Nucilandia", "Devicius s-a deconectat de la rețeaua Wi-Fi 'Nucilandia'")]
        public void GivenAWiFiDisconnection_WhenBuildingLogText_ThenTheLocalisedTextIsReturned(
            string localisation,
            string networkName,
            string expectedText)
        {
            PersonalLog log = new(new DateOnly(2012, 9, 5))
            {
                Template = PersonalLogTemplate.DeviceWiFiDisconnection,
                Data = new Dictionary<string, string>
                {
                    { "device_name", "Devicius" },
                    { "network_name", networkName }
                }
            };

            string renderedText = factory.BuildLogText(log, localisation);

            Assert.That(renderedText, Is.EqualTo($"2012-09-05: {expectedText}"));
        }

        private static PersonalLog BuildTextLog(
            DateOnly date,
            TimeOnly? time,
            string timeZone,
            string text)
        {
            return new(date, time, timeZone)
            {
                Template = PersonalLogTemplate.Text,
                Data = new Dictionary<string, string> { { "text", text } }
            };
        }

        private static PersonalLog BuildActivationLog(string platform)
        {
            return new(new DateOnly(2012, 9, 5))
            {
                Template = PersonalLogTemplate.AccountActivation,
                Data = new Dictionary<string, string> { { "platform", platform } }
            };
        }
    }
}
