using System.Collections.Generic;

using Moq;

using NUnit.Framework;

using NuciText.Obfuscation;

using PersonalLogManager.Service.Models;
using PersonalLogManager.Service.TextBuilding.Localisation;

namespace PersonalLogManager.UnitTests.Service.TextBuilding.Localisation
{
    [TestFixture]
    public class RomanianTextBuilderTests
    {
        Mock<INuciTextObfuscator> obfuscatorMock;
        RomanianTextBuilder textBuilder;

        [SetUp]
        public void SetUp()
        {
            obfuscatorMock = new Mock<INuciTextObfuscator>();
            obfuscatorMock
                .Setup(obfuscator => obfuscator.Deobfuscate(It.IsAny<string>()))
                .Returns((string input) => input);

            textBuilder = new RomanianTextBuilder(obfuscatorMock.Object);
        }

        // ── BuildAccountActivationLogText ──────────────────────

        [Test]
        public void GivenPlatform_WhenBuildAccountActivationLogText_ThenReturnsExpectedText()
        {
            PersonalLog log = BuildLog(new Dictionary<string, string> { { "platform", "Nucilandia" } });

            string result = textBuilder.BuildAccountActivationLogText(log);

            Assert.That(result, Is.EqualTo("Am activat contul de Nucilandia"));
        }

        [Test]
        public void GivenPlatformWithDiscriminator_WhenBuildAccountActivationLogText_ThenIncludesDiscriminator()
        {
            PersonalLog log = BuildLog(new Dictionary<string, string>
            {
                { "platform", "Nucilandia" },
                { "discriminator", "IlarionPintilie" }
            });

            string result = textBuilder.BuildAccountActivationLogText(log);

            Assert.That(result, Is.EqualTo("Am activat contul de Nucilandia (IlarionPintilie)"));
        }

        // ── BuildAccountBanningLogText ─────────────────────────

        [Test]
        public void GivenPlatformWithoutBanReason_WhenBuildAccountBanningLogText_ThenReturnsSimpleText()
        {
            PersonalLog log = BuildLog(new Dictionary<string, string> { { "platform", "Nucilandia" } });

            string result = textBuilder.BuildAccountBanningLogText(log);

            Assert.That(result, Is.EqualTo("Contul de Nucilandia a fost banat"));
        }

        [Test]
        public void GivenPlatformWithBanReason_WhenBuildAccountBanningLogText_ThenIncludesBanReason()
        {
            PersonalLog log = BuildLog(new Dictionary<string, string>
            {
                { "platform", "Nucilandia" },
                { "ban_reason", "spam" }
            });

            string result = textBuilder.BuildAccountBanningLogText(log);

            Assert.That(
                result,
                Is.EqualTo("Contul de Nucilandia a fost banat pentru următorul motiv: spam"));
        }

        // ── BuildAccountLoginLogText ───────────────────────────

        [Test]
        public void GivenPlatformWithoutIpAddress_WhenBuildAccountLoginLogText_ThenReturnsSimpleText()
        {
            PersonalLog log = BuildLog(new Dictionary<string, string> { { "platform", "Nucilandia" } });

            string result = textBuilder.BuildAccountLoginLogText(log);

            Assert.That(result, Is.EqualTo("M-am logat în contul de Nucilandia"));
        }

        [Test]
        public void GivenPlatformWithIpAddress_WhenBuildAccountLoginLogText_ThenIncludesIpAddress()
        {
            PersonalLog log = BuildLog(new Dictionary<string, string>
            {
                { "platform", "Nucilandia" },
                { "ip_address", "192.168.0.1" }
            });

            string result = textBuilder.BuildAccountLoginLogText(log);

            Assert.That(
                result,
                Is.EqualTo("M-am logat în contul de Nucilandia de la adresa IP 192.168.0.1"));
        }

        // ── BuildAccountEmailAddressChangeLogText ──────────────

        [Test]
        public void GivenOnlyNewEmailAddress_WhenBuildAccountEmailAddressChangeLogText_ThenUsesVerbSetat()
        {
            PersonalLog log = BuildLog(new Dictionary<string, string>
            {
                { "platform", "Nucilandia" },
                { "new_email_address", "ilarion.pintilie@nucilandia.ro" }
            });

            string result = textBuilder.BuildAccountEmailAddressChangeLogText(log);

            Assert.That(
                result,
                Is.EqualTo(
                    "Am setat adresa de e-mail a contului de Nucilandia" +
                    " în ilarion.pintilie@nucilandia.ro"));
        }

        [Test]
        public void GivenOldAndNewEmailAddresses_WhenBuildAccountEmailAddressChangeLogText_ThenUsesVerbSchimbat()
        {
            PersonalLog log = BuildLog(new Dictionary<string, string>
            {
                { "platform", "Nucilandia" },
                { "old_email_address", "vasile.ciupitu@gmail.com" },
                { "new_email_address", "ilarion.pintilie@nucilandia.ro" }
            });

            string result = textBuilder.BuildAccountEmailAddressChangeLogText(log);

            Assert.That(
                result,
                Is.EqualTo(
                    "Am schimbat adresa de e-mail a contului de Nucilandia" +
                    " din vasile.ciupitu@gmail.com" +
                    " în ilarion.pintilie@nucilandia.ro"));
        }

        // ── BuildAccountDeletionLogText ────────────────────────

        [Test]
        public void GivenPlatform_WhenBuildAccountDeletionLogText_ThenReturnsExpectedText()
        {
            PersonalLog log = BuildLog(new Dictionary<string, string> { { "platform", "Nucilandia" } });

            string result = textBuilder.BuildAccountDeletionLogText(log);

            Assert.That(result, Is.EqualTo("Am șters contul de Nucilandia"));
        }

        // ── BuildAccountPasswordChangeLogText ─────────────────

        [Test]
        public void GivenPlatform_WhenBuildAccountPasswordChangeLogText_ThenReturnsExpectedText()
        {
            PersonalLog log = BuildLog(new Dictionary<string, string> { { "platform", "Nucilandia" } });

            string result = textBuilder.BuildAccountPasswordChangeLogText(log);

            Assert.That(result, Is.EqualTo("Am schimbat parola contului de Nucilandia"));
        }

        // ── BuildAccountDataExportSaveLogText ──────────────────

        [Test]
        public void GivenRequestDateAndRequestId_WhenBuildAccountDataExportSaveLogText_ThenIncludesBoth()
        {
            PersonalLog log = BuildLog(new Dictionary<string, string>
            {
                { "platform", "Nucilandia" },
                { "request_date", "2012-09-05" },
                { "request_id", "REQ-613" }
            });

            string result = textBuilder.BuildAccountDataExportSaveLogText(log);

            Assert.That(result, Does.Contain("REQ-613"));
            Assert.That(result, Does.Contain("2012-09-05"));
        }

        [Test]
        public void GivenPlatformOnly_WhenBuildAccountDataExportSaveLogText_ThenReturnsSimpleText()
        {
            PersonalLog log = BuildLog(new Dictionary<string, string>
            {
                { "platform", "Nucilandia" }
            });

            string result = textBuilder.BuildAccountDataExportSaveLogText(log);

            Assert.That(
                result,
                Is.EqualTo("Am salvat exportul datelor contului de Nucilandia"));
        }

        // ── GetLocalisedValue (Romanian-specific behaviour) ────

        [Test]
        public void GivenValueWithEnglishConjunction_WhenGetLocalisedValue_ThenConvertsToRomanian()
        {
            Dictionary<string, string> data = new() { { "items", "Solara and Cratesia" } };

            string result = textBuilder.GetLocalisedValue(data, "items");

            Assert.That(result, Is.EqualTo("Solara și Cratesia"));
        }

        [Test]
        public void GivenValueWithRomanianConjunction_WhenGetLocalisedValue_ThenKeepsRomanian()
        {
            Dictionary<string, string> data = new() { { "items", "Solara și Cratesia" } };

            string result = textBuilder.GetLocalisedValue(data, "items");

            Assert.That(result, Is.EqualTo("Solara și Cratesia"));
        }

        [Test]
        public void GivenValueWithAmpersand_WhenGetLocalisedValue_ThenReplacesWithRomanian()
        {
            Dictionary<string, string> data = new() { { "items", "Solara & Cratesia" } };

            string result = textBuilder.GetLocalisedValue(data, "items");

            Assert.That(result, Is.EqualTo("Solara și Cratesia"));
        }

        private static PersonalLog BuildLog(Dictionary<string, string> data)
        {
            PersonalLog log = new(new DateOnly(2012, 9, 5))
            {
                Data = data
            };

            return log;
        }
    }
}
