using System;
using System.Collections.Generic;

using Moq;

using NUnit.Framework;

using NuciText.Obfuscation;

using PersonalLogManager.Service.Models;
using PersonalLogManager.Service.TextBuilding.Localisation;

namespace PersonalLogManager.UnitTests.Service.TextBuilding
{
    [TestFixture]
    public class PersonalLogTextBuilderBaseTests
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

        // ── BuildTextLogText ───────────────────────────────────

        [Test]
        public void GivenLogWithTextData_WhenBuildTextLogText_ThenReturnsTextValue()
        {
            PersonalLog log = BuildLog(new Dictionary<string, string> { { "text", "Hello, world." } });

            string result = textBuilder.BuildTextLogText(log);

            Assert.That(result, Is.EqualTo("Hello, world."));
        }

        [Test]
        public void GivenLogWithMissingTextKey_WhenBuildTextLogText_ThenReturnsNull()
        {
            PersonalLog log = BuildLog(new Dictionary<string, string>());

            string result = textBuilder.BuildTextLogText(log);

            Assert.That(result, Is.Null);
        }

        // ── GetPlatform ────────────────────────────────────────

        [Test]
        public void GivenNullData_WhenGetPlatform_ThenReturnsNull()
        {
            string result = textBuilder.GetPlatform(null);

            Assert.That(result, Is.Null);
        }

        [Test]
        public void GivenDataWithPlatformOnly_WhenGetPlatform_ThenReturnsPlatform()
        {
            Dictionary<string, string> data = new() { { "platform", "Nucilandia" } };

            string result = textBuilder.GetPlatform(data);

            Assert.That(result, Is.EqualTo("Nucilandia"));
        }

        [Test]
        public void GivenDataWithPlatformAndDiscriminator_WhenGetPlatform_ThenReturnsPlatformWithDiscriminator()
        {
            Dictionary<string, string> data = new()
            {
                { "platform", "Nucilandia" },
                { "discriminator", "IlarionPintilie" }
            };

            string result = textBuilder.GetPlatform(data);

            Assert.That(result, Is.EqualTo("Nucilandia (IlarionPintilie)"));
        }

        [Test]
        public void GivenDataWithBlankPlatform_WhenGetPlatform_ThenReturnsNull()
        {
            Dictionary<string, string> data = new() { { "platform", " " } };

            string result = textBuilder.GetPlatform(data);

            Assert.That(result, Is.Null);
        }

        // ── GetDiscriminator ───────────────────────────────────

        [Test]
        public void GivenNullData_WhenGetDiscriminator_ThenReturnsNull()
        {
            string result = textBuilder.GetDiscriminator(null);

            Assert.That(result, Is.Null);
        }

        [Test]
        public void GivenDataWithDiscriminatorKey_WhenGetDiscriminator_ThenReturnsDiscriminator()
        {
            Dictionary<string, string> data = new() { { "discriminator", "IlarionPintilie" } };

            string result = textBuilder.GetDiscriminator(data);

            Assert.That(result, Is.EqualTo("IlarionPintilie"));
        }

        [Test]
        public void GivenDataWithAccountKey_WhenGetDiscriminator_ThenReturnsAccount()
        {
            Dictionary<string, string> data = new() { { "account", "IlarionPintilie" } };

            string result = textBuilder.GetDiscriminator(data);

            Assert.That(result, Is.EqualTo("IlarionPintilie"));
        }

        [Test]
        public void GivenDataWithAccountIdKey_WhenGetDiscriminator_ThenReturnsAccountId()
        {
            Dictionary<string, string> data = new() { { "account_id", "613" } };

            string result = textBuilder.GetDiscriminator(data);

            Assert.That(result, Is.EqualTo("613"));
        }

        [Test]
        public void GivenDataWithUsernameKey_WhenGetDiscriminator_ThenReturnsUsername()
        {
            Dictionary<string, string> data = new() { { "username", "solaire_of_astora" } };

            string result = textBuilder.GetDiscriminator(data);

            Assert.That(result, Is.EqualTo("solaire_of_astora"));
        }

        [Test]
        public void GivenDataWithPhoneNumberKey_WhenGetDiscriminator_ThenReturnsPhoneNumber()
        {
            Dictionary<string, string> data = new() { { "phone_number", "+40123456789" } };

            string result = textBuilder.GetDiscriminator(data);

            Assert.That(result, Is.EqualTo("+40123456789"));
        }

        [Test]
        public void GivenDataWithEmailAddressKey_WhenGetDiscriminator_ThenReturnsEmailAddress()
        {
            Dictionary<string, string> data = new()
            {
                { "email_address", "ilarion.pintilie@nucilandia.ro" }
            };

            string result = textBuilder.GetDiscriminator(data);

            Assert.That(result, Is.EqualTo("ilarion.pintilie@nucilandia.ro"));
        }

        [Test]
        public void GivenDataWithDiscriminatorAndAccount_WhenGetDiscriminator_ThenPrefersDiscriminator()
        {
            Dictionary<string, string> data = new()
            {
                { "discriminator", "IlarionPintilie" },
                { "account", "solaire_of_astora" }
            };

            string result = textBuilder.GetDiscriminator(data);

            Assert.That(result, Is.EqualTo("IlarionPintilie"));
        }

        // ── GetDataValue ───────────────────────────────────────

        [Test]
        public void GivenNullData_WhenGetDataValue_ThenReturnsNull()
        {
            string result = textBuilder.GetDataValue(null, "key");

            Assert.That(result, Is.Null);
        }

        [Test]
        public void GivenDataWithMissingKey_WhenGetDataValue_ThenReturnsNull()
        {
            Dictionary<string, string> data = new();

            string result = textBuilder.GetDataValue(data, "key");

            Assert.That(result, Is.Null);
        }

        [Test]
        public void GivenDataWithBlankValue_WhenGetDataValue_ThenReturnsNull()
        {
            Dictionary<string, string> data = new() { { "key", "   " } };

            string result = textBuilder.GetDataValue(data, "key");

            Assert.That(result, Is.Null);
        }

        [Test]
        public void GivenDataWithValidValue_WhenGetDataValue_ThenReturnsDeobfuscatedValue()
        {
            Dictionary<string, string> data = new() { { "key", "Nucilandia" } };

            string result = textBuilder.GetDataValue(data, "key");

            Assert.That(result, Is.EqualTo("Nucilandia"));
        }

        [Test]
        public void GivenNullDataWithDefault_WhenGetDataValueWithDefault_ThenReturnsDefault()
        {
            string result = textBuilder.GetDataValue(null, "key", "default-value");

            Assert.That(result, Is.EqualTo("default-value"));
        }

        [Test]
        public void GivenDataWithMissingKeyWithDefault_WhenGetDataValueWithDefault_ThenReturnsDefault()
        {
            Dictionary<string, string> data = new();

            string result = textBuilder.GetDataValue(data, "key", "default-value");

            Assert.That(result, Is.EqualTo("default-value"));
        }

        [Test]
        public void GivenDataWithBlankValueWithDefault_WhenGetDataValueWithDefault_ThenReturnsDefault()
        {
            Dictionary<string, string> data = new() { { "key", " " } };

            string result = textBuilder.GetDataValue(data, "key", "default-value");

            Assert.That(result, Is.EqualTo("default-value"));
        }

        // ── GetDecimalValue ────────────────────────────────────

        [Test]
        public void GivenDataWithWholeNumber_WhenGetDecimalValue_ThenReturnsNumberWithoutDecimals()
        {
            Dictionary<string, string> data = new() { { "amount", "613" } };

            string result = textBuilder.GetDecimalValue(data, "amount");

            Assert.That(result, Is.EqualTo("613"));
        }

        [Test]
        public void GivenDataWithDecimalNumber_WhenGetDecimalValue_ThenReturnsNumberWithDecimals()
        {
            Dictionary<string, string> data = new() { { "amount", "3.14" } };

            string result = textBuilder.GetDecimalValue(data, "amount");

            Assert.That(result, Is.EqualTo("3.14"));
        }

        [Test]
        public void GivenDataWithMissingKey_WhenGetDecimalValue_ThenThrowsArgumentException()
        {
            Dictionary<string, string> data = new();

            Assert.That(
                () => textBuilder.GetDecimalValue(data, "amount"),
                Throws.InstanceOf<ArgumentException>());
        }

        // ── GetLocalisedValue (English) ────────────────────────

        [Test]
        public void GivenEnglishBuilderAndValueWithRomanianConjunction_WhenGetLocalisedValue_ThenReplacesWithEnglish()
        {
            Dictionary<string, string> data = new() { { "items", "Solara și Cratesia" } };

            string result = textBuilder.GetLocalisedValue(data, "items");

            Assert.That(result, Is.EqualTo("Solara and Cratesia"));
        }

        [Test]
        public void GivenEnglishBuilderAndValueWithAmpersand_WhenGetLocalisedValue_ThenReplacesWithAnd()
        {
            Dictionary<string, string> data = new() { { "items", "Solara & Cratesia" } };

            string result = textBuilder.GetLocalisedValue(data, "items");

            Assert.That(result, Is.EqualTo("Solara and Cratesia"));
        }

        [Test]
        public void GivenNullData_WhenGetLocalisedValue_ThenReturnsNull()
        {
            string result = textBuilder.GetLocalisedValue(null, "items");

            Assert.That(result, Is.Null);
        }

        [Test]
        public void GivenMissingKey_WhenGetLocalisedValueWithDefault_ThenReturnsDefault()
        {
            Dictionary<string, string> data = new();

            string result = textBuilder.GetLocalisedValue(data, "items", "default-value");

            Assert.That(result, Is.EqualTo("default-value"));
        }

        // ── IsDataValuePlural ──────────────────────────────────

        [Test]
        public void GivenSingleValue_WhenIsDataValuePlural_ThenReturnsFalse()
        {
            Dictionary<string, string> data = new() { { "item", "Solara" } };

            bool result = textBuilder.IsDataValuePlural(data, "item");

            Assert.That(result, Is.False);
        }

        [Test]
        public void GivenValueWithAndConjunction_WhenIsDataValuePlural_ThenReturnsTrue()
        {
            Dictionary<string, string> data = new() { { "item", "Solara and Cratesia" } };

            bool result = textBuilder.IsDataValuePlural(data, "item");

            Assert.That(result, Is.True);
        }

        [Test]
        public void GivenValueWithComma_WhenIsDataValuePlural_ThenReturnsTrue()
        {
            Dictionary<string, string> data = new() { { "item", "Solara, Cratesia" } };

            bool result = textBuilder.IsDataValuePlural(data, "item");

            Assert.That(result, Is.True);
        }

        [Test]
        public void GivenValueWithAmpersand_WhenIsDataValuePlural_ThenReturnsTrue()
        {
            Dictionary<string, string> data = new() { { "item", "Solara & Cratesia" } };

            bool result = textBuilder.IsDataValuePlural(data, "item");

            Assert.That(result, Is.True);
        }

        [Test]
        public void GivenValueWithRomanianConjunction_WhenIsDataValuePlural_ThenReturnsTrue()
        {
            Dictionary<string, string> data = new() { { "item", "Solara și Cratesia" } };

            bool result = textBuilder.IsDataValuePlural(data, "item");

            Assert.That(result, Is.True);
        }

        [Test]
        public void GivenNullData_WhenIsDataValuePlural_ThenReturnsFalse()
        {
            bool result = textBuilder.IsDataValuePlural(null, "item");

            Assert.That(result, Is.False);
        }

        [Test]
        public void GivenMissingKey_WhenIsDataValuePlural_ThenReturnsFalse()
        {
            Dictionary<string, string> data = new();

            bool result = textBuilder.IsDataValuePlural(data, "item");

            Assert.That(result, Is.False);
        }

        // ── IsDataValuePresent ─────────────────────────────────

        [Test]
        public void GivenNullData_WhenIsDataValuePresent_ThenReturnsFalse()
        {
            bool result = textBuilder.IsDataValuePresent(null, "key");

            Assert.That(result, Is.False);
        }

        [Test]
        public void GivenDataWithoutKey_WhenIsDataValuePresent_ThenReturnsFalse()
        {
            Dictionary<string, string> data = new();

            bool result = textBuilder.IsDataValuePresent(data, "key");

            Assert.That(result, Is.False);
        }

        [Test]
        public void GivenDataWithKey_WhenIsDataValuePresent_ThenReturnsTrue()
        {
            Dictionary<string, string> data = new() { { "key", "value" } };

            bool result = textBuilder.IsDataValuePresent(data, "key");

            Assert.That(result, Is.True);
        }

        // ── GetMappedDataValue ─────────────────────────────────

        [Test]
        public void GivenDataWithExactMatchingKey_WhenGetMappedDataValue_ThenReturnsMappedValue()
        {
            Dictionary<string, string> data = new() { { "method", "AccountSettings" } };
            Dictionary<string, string> mappings = new()
            {
                { "AccountSettings", "account settings page" },
                { "EMail", "e-mail" }
            };

            string result = textBuilder.GetMappedDataValue(data, "method", mappings);

            Assert.That(result, Is.EqualTo("account settings page"));
        }

        [Test]
        public void GivenDataWithNormalisedMatchingKey_WhenGetMappedDataValue_ThenReturnsMappedValue()
        {
            Dictionary<string, string> data = new() { { "method", "Account_Settings" } };
            Dictionary<string, string> mappings = new()
            {
                { "AccountSettings", "account settings page" },
                { "EMail", "e-mail" }
            };

            string result = textBuilder.GetMappedDataValue(data, "method", mappings);

            Assert.That(result, Is.EqualTo("account settings page"));
        }

        [Test]
        public void GivenDataWithKeyNormalisedWithSpaces_WhenGetMappedDataValue_ThenReturnsMappedValue()
        {
            Dictionary<string, string> data = new() { { "method", "Account Settings" } };
            Dictionary<string, string> mappings = new()
            {
                { "AccountSettings", "account settings page" }
            };

            string result = textBuilder.GetMappedDataValue(data, "method", mappings);

            Assert.That(result, Is.EqualTo("account settings page"));
        }

        [Test]
        public void GivenDataWithUnmatchedKey_WhenGetMappedDataValue_ThenReturnsOriginalValue()
        {
            Dictionary<string, string> data = new() { { "method", "Telegram" } };
            Dictionary<string, string> mappings = new()
            {
                { "AccountSettings", "account settings page" },
                { "EMail", "e-mail" }
            };

            string result = textBuilder.GetMappedDataValue(data, "method", mappings);

            Assert.That(result, Is.EqualTo("Telegram"));
        }

        [Test]
        public void GivenNullData_WhenGetMappedDataValue_ThenReturnsNull()
        {
            Dictionary<string, string> mappings = new()
            {
                { "AccountSettings", "account settings page" }
            };

            string result = textBuilder.GetMappedDataValue(null, "method", mappings);

            Assert.That(result, Is.Null);
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
