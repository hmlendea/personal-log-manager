using System.Collections.Generic;

using Moq;

using NUnit.Framework;

using NuciText.Obfuscation;

using PersonalLogManager.Service.Models;
using PersonalLogManager.Service.TextBuilding.Localisation;

namespace PersonalLogManager.UnitTests.Service.TextBuilding.Localisation
{
    [TestFixture]
    public class EnglishTextBuilderTests
    {
        Mock<INuciTextObfuscator> obfuscatorMock;
        EnglishTextBuilder textBuilder;

        [SetUp]
        public void SetUp()
        {
            obfuscatorMock = new Mock<INuciTextObfuscator>();
            obfuscatorMock
                .Setup(obfuscator => obfuscator.Deobfuscate(It.IsAny<string>()))
                .Returns((string input) => input);

            textBuilder = new EnglishTextBuilder(obfuscatorMock.Object);
        }

        // ── BuildAccountActivationLogText ──────────────────────

        [Test]
        public void GivenPlatform_WhenBuildAccountActivationLogText_ThenReturnsExpectedText()
        {
            PersonalLog log = BuildLog(new Dictionary<string, string> { { "platform", "Nucilandia" } });

            string result = textBuilder.BuildAccountActivationLogText(log);

            Assert.That(result, Is.EqualTo("I have activated the Nucilandia account"));
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

            Assert.That(result, Is.EqualTo("I have activated the Nucilandia (IlarionPintilie) account"));
        }

        // ── BuildAccountBanningLogText ─────────────────────────

        [Test]
        public void GivenPlatformWithoutBanReason_WhenBuildAccountBanningLogText_ThenReturnsSimpleText()
        {
            PersonalLog log = BuildLog(new Dictionary<string, string> { { "platform", "Nucilandia" } });

            string result = textBuilder.BuildAccountBanningLogText(log);

            Assert.That(result, Is.EqualTo("The Nucilandia account has been banned"));
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
                Is.EqualTo("The Nucilandia account has been banned for the following reason: spam"));
        }

        // ── BuildAccountLoginLogText ───────────────────────────

        [Test]
        public void GivenPlatformWithoutIpAddress_WhenBuildAccountLoginLogText_ThenReturnsSimpleText()
        {
            PersonalLog log = BuildLog(new Dictionary<string, string> { { "platform", "Nucilandia" } });

            string result = textBuilder.BuildAccountLoginLogText(log);

            Assert.That(result, Is.EqualTo("I have logged in to the Nucilandia account"));
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
                Is.EqualTo("I have logged in to the Nucilandia account from the 192.168.0.1 IP address"));
        }

        // ── BuildAccountEmailAddressChangeLogText ──────────────

        [Test]
        public void GivenOnlyNewEmailAddress_WhenBuildAccountEmailAddressChangeLogText_ThenUsesVerbSet()
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
                    "I have set the e-mail address of the Nucilandia account" +
                    " to ilarion.pintilie@nucilandia.ro"));
        }

        [Test]
        public void GivenOldAndNewEmailAddresses_WhenBuildAccountEmailAddressChangeLogText_ThenUsesVerbChanged()
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
                    "I have changed the e-mail address of the Nucilandia account" +
                    " from vasile.ciupitu@gmail.com" +
                    " to ilarion.pintilie@nucilandia.ro"));
        }

        // ── BuildAccountDeletionRequestLogText ────────────────

        [Test]
        public void GivenPlatformOnly_WhenBuildAccountDeletionRequestLogText_ThenReturnsSimpleText()
        {
            PersonalLog log = BuildLog(new Dictionary<string, string> { { "platform", "Nucilandia" } });

            string result = textBuilder.BuildAccountDeletionRequestLogText(log);

            Assert.That(
                result,
                Is.EqualTo("I have sent a request for the deletion of the Nucilandia account"));
        }

        [Test]
        public void GivenPlatformWithRequestId_WhenBuildAccountDeletionRequestLogText_ThenIncludesRequestId()
        {
            PersonalLog log = BuildLog(new Dictionary<string, string>
            {
                { "platform", "Nucilandia" },
                { "request_id", "REQ-613" }
            });

            string result = textBuilder.BuildAccountDeletionRequestLogText(log);

            Assert.That(result, Does.Contain("REQ-613"));
        }

        [Test]
        public void GivenPlatformWithRequestMethod_WhenBuildAccountDeletionRequestLogText_ThenIncludesMappedMethod()
        {
            PersonalLog log = BuildLog(new Dictionary<string, string>
            {
                { "platform", "Nucilandia" },
                { "request_method", "EMail" }
            });

            string result = textBuilder.BuildAccountDeletionRequestLogText(log);

            Assert.That(result, Does.Contain("via e-mail"));
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
        public void GivenRequestDateOnly_WhenBuildAccountDataExportSaveLogText_ThenIncludesRequestDate()
        {
            PersonalLog log = BuildLog(new Dictionary<string, string>
            {
                { "platform", "Nucilandia" },
                { "request_date", "2012-09-05" }
            });

            string result = textBuilder.BuildAccountDataExportSaveLogText(log);

            Assert.That(result, Does.Contain("2012-09-05"));
            Assert.That(result, Does.Not.Contain("identification code"));
        }

        [Test]
        public void GivenRequestIdOnly_WhenBuildAccountDataExportSaveLogText_ThenIncludesRequestId()
        {
            PersonalLog log = BuildLog(new Dictionary<string, string>
            {
                { "platform", "Nucilandia" },
                { "request_id", "REQ-613" }
            });

            string result = textBuilder.BuildAccountDataExportSaveLogText(log);

            Assert.That(result, Does.Contain("REQ-613"));
            Assert.That(result, Does.Not.Contain("2012-09-05"));
        }

        [Test]
        public void GivenPlatformWithoutRequestDetails_WhenBuildAccountDataExportSaveLogText_ThenReturnsSimpleText()
        {
            PersonalLog log = BuildLog(new Dictionary<string, string>
            {
                { "platform", "Nucilandia" }
            });

            string result = textBuilder.BuildAccountDataExportSaveLogText(log);

            Assert.That(
                result,
                Is.EqualTo(
                    "I have saved the export of the data related to the Nucilandia account"));
        }

        // ── BuildAccountDeletionLogText ────────────────────────

        [Test]
        public void GivenPlatform_WhenBuildAccountDeletionLogText_ThenReturnsExpectedText()
        {
            PersonalLog log = BuildLog(new Dictionary<string, string> { { "platform", "Nucilandia" } });

            string result = textBuilder.BuildAccountDeletionLogText(log);

            Assert.That(result, Is.EqualTo("I have deleted the Nucilandia account"));
        }

        // ── BuildAccountPasswordChangeLogText ─────────────────

        [Test]
        public void GivenPlatform_WhenBuildAccountPasswordChangeLogText_ThenReturnsExpectedText()
        {
            PersonalLog log = BuildLog(new Dictionary<string, string> { { "platform", "Nucilandia" } });

            string result = textBuilder.BuildAccountPasswordChangeLogText(log);

            Assert.That(result, Is.EqualTo("I have changed the password of the Nucilandia account"));
        }

        // ── GetLocalisedValue (English-specific behaviour) ─────

        [Test]
        public void GivenValueWithRomanianConjunction_WhenGetLocalisedValue_ThenConvertsToEnglish()
        {
            Dictionary<string, string> data = new() { { "items", "Solara și Cratesia" } };

            string result = textBuilder.GetLocalisedValue(data, "items");

            Assert.That(result, Is.EqualTo("Solara and Cratesia"));
        }

        [Test]
        public void GivenValueWithEnglishConjunction_WhenGetLocalisedValue_ThenKeepsEnglish()
        {
            Dictionary<string, string> data = new() { { "items", "Solara and Cratesia" } };

            string result = textBuilder.GetLocalisedValue(data, "items");

            Assert.That(result, Is.EqualTo("Solara and Cratesia"));
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
