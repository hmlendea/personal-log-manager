using System;
using System.Collections.Generic;
using NuciText.Obfuscation;
using PersonalLogManager.Service.Models;

namespace PersonalLogManager.Service.TextBuilding
{
    public abstract class PersonalLogTextBuilderBase(
        INuciTextObfuscator obfuscator)
    {
        protected abstract string LanguageCode { get; }

        protected string MissingValue => "[MISSING_VALUE]";

        public string BuildTextLogText(PersonalLog log)
            => GetDataValue(log.Data, "text");

        public string GetPlatform(Dictionary<string, string> data)
        {
            data.TryGetValue("platform", out string platform);

            if (string.IsNullOrWhiteSpace(platform))
            {
                return null;
            }

            string text = platform;
            string discriminator = GetDiscriminator(data);

            if (!string.IsNullOrWhiteSpace(discriminator))
            {
                text += $" ({discriminator})";
            }

            return text;
        }

        public string GetDiscriminator(Dictionary<string, string> data)
        {
            data.TryGetValue("discriminator", out string discriminator);

            if (string.IsNullOrWhiteSpace(discriminator))
            {
                data.TryGetValue("account", out discriminator);
            }

            if (string.IsNullOrWhiteSpace(discriminator))
            {
                data.TryGetValue("account_id", out discriminator);
            }

            if (string.IsNullOrWhiteSpace(discriminator))
            {
                data.TryGetValue("username", out discriminator);
            }

            if (string.IsNullOrWhiteSpace(discriminator))
            {
                data.TryGetValue("phone_number", out discriminator);
            }

            if (string.IsNullOrWhiteSpace(discriminator))
            {
                data.TryGetValue("email_address", out discriminator);
            }

            return discriminator;
        }

        public string GetDataValue(Dictionary<string, string> data, string key)
            => GetDataValue(data, key, null);

        public string GetDataValue(Dictionary<string, string> data, string key, string defaultValue)
        {
            data.TryGetValue(key, out string value);

            if (string.IsNullOrWhiteSpace(value))
            {
                return defaultValue;
            }

            return obfuscator.Deobfuscate(value);
        }

        public string GetDecimalValue(Dictionary<string, string> data, string key)
        {
            string value = GetDataValue(data, key);

            ArgumentException.ThrowIfNullOrWhiteSpace(value, key);

            return $"{decimal.Parse(value):F2}".Replace(".00", string.Empty);
        }

        public string GetLocalisedValue(Dictionary<string, string> data, string key)
            => GetLocalisedValue(data, key, defaultValue: null);

        public string GetLocalisedValue(Dictionary<string, string> data, string key, string defaultValue)
        {
            string value = GetDataValue(data, key);

            if (string.IsNullOrWhiteSpace(value))
            {
                return defaultValue;
            }

            if (LanguageCode.Equals("ro"))
            {
                return value
                    .Replace(" and ", " și ")
                    .Replace("&", "și");
            }

            return value
                .Replace(" și ", " and ")
                .Replace("&", "and");
        }

        public bool IsDataValuePlural(Dictionary<string, string> data, string key)
            => IsDataValuePlural(data, key, null);

        public bool IsDataValuePlural(Dictionary<string, string> data, string key, string defaultValue)
        {
            string value = GetDataValue(data, key);

            if (string.IsNullOrWhiteSpace(value))
            {
                value = defaultValue;
            }

            if (string.IsNullOrWhiteSpace(value))
            {
                return false;
            }

            if (value.Contains(" & ", StringComparison.InvariantCultureIgnoreCase) ||
                value.Contains(", ", StringComparison.InvariantCultureIgnoreCase) ||
                value.Contains(" and ", StringComparison.InvariantCultureIgnoreCase) ||
                value.Contains(" și ", StringComparison.InvariantCultureIgnoreCase))
            {
                return true;
            }

            return false;
        }

        public bool IsDataValuePresent(Dictionary<string, string> data, string key)
            => data.ContainsKey(key);

        public string GetMappedDataValue(Dictionary<string, string> data, string key, Dictionary<string, string> mappings)
        {
            string mapKey = GetDataValue(data, key);

            if (string.IsNullOrWhiteSpace(mapKey))
            {
                return null;
            }

            return GetMappedDataValue(data, key, mappings, mapKey);
        }

        public string GetMappedDataValue(Dictionary<string, string> data, string key, Dictionary<string, string> mappings, string defaultValue)
        {
            string mapKey = GetDataValue(data, key);

            if (string.IsNullOrWhiteSpace(mapKey))
            {
                return defaultValue;
            }

            string mappedValue = defaultValue;

            foreach (string expectedMapKey in mappings.Keys)
            {
                string normalisedMapKey = mapKey
                    .Replace(" ", string.Empty)
                    .Replace("-", string.Empty)
                    .Replace("_", string.Empty)
                    .Replace("&", "And");

                if (normalisedMapKey.Equals(expectedMapKey, StringComparison.InvariantCultureIgnoreCase))
                {
                    mappedValue = mappings[expectedMapKey];
                    break;
                }
            }

            return mappedValue;
        }

        protected abstract string GetAccessoryType(
            Dictionary<string, string> data,
            bool useDefinitiveForm = false);

        protected string GetBalance(Dictionary<string, string> data)
        {
            decimal amount = 0.0m;

            if (data.ContainsKey("amount"))
            {
                amount = decimal.Parse(GetDataValue(data, "amount"));
            }
            else if (data.ContainsKey("balance"))
            {
                amount = decimal.Parse(GetDataValue(data, "balance"));
            }
            else if (data.ContainsKey("price"))
            {
                amount = decimal.Parse(GetDataValue(data, "price"));
            }
            else if (data.ContainsKey("price_amount"))
            {
                amount = decimal.Parse(GetDataValue(data, "price_amount"));
            }
            else if (data.ContainsKey("cost_amount"))
            {
                amount = decimal.Parse(GetDataValue(data, "cost_amount"));
            }
            else if (data.ContainsKey("total_amount"))
            {
                amount = decimal.Parse(GetDataValue(data, "total_amount"));
            }

            string currency = string.Empty;

            if (data.ContainsKey("currency"))
            {
                currency = GetDataValue(data, "currency");
            }
            else if (data.ContainsKey("amount_currency"))
            {
                currency = GetDataValue(data, "amount_currency");
            }
            else if (data.ContainsKey("balance_currency"))
            {
                currency = GetDataValue(data, "balance_currency");
            }
            else if (data.ContainsKey("cost_currency"))
            {
                currency = GetDataValue(data, "cost_currency");
            }
            else if (data.ContainsKey("price_currency"))
            {
                currency = GetDataValue(data, "price_currency");
            }
            else if (data.ContainsKey("total_amount_currency"))
            {
                currency = GetDataValue(data, "total_amount_currency");
            }

            string text = $"{amount}";

            if (!string.IsNullOrWhiteSpace(currency))
            {
                text += $" {currency}";
            }

            return text;
        }

        protected abstract string GetByPerson(Dictionary<string, string> data);

        protected abstract string GetCleaningMethod(
            Dictionary<string, string> data);

        protected abstract string GetDevice(Dictionary<string, string> data);

        protected abstract string GetDeviceType(Dictionary<string, string> data);

        protected abstract string GetFluidType(Dictionary<string, string> data, bool useDefinitiveForm);

        protected abstract string GetHairType(Dictionary<string, string> data);

        protected abstract string GetLocation(Dictionary<string, string> data);

        protected abstract string GetMedicationType(
            Dictionary<string, string> data,
            bool usePluralForm);

        protected abstract string GetNailsType(
            Dictionary<string, string> data);

        protected abstract string GetPet(Dictionary<string, string> data);

        protected abstract string GetPetType(
            Dictionary<string, string> data,
            bool useDefinitiveForm = false,
            bool usePluralForm = false);

        protected abstract string GetPlantType(
            Dictionary<string, string> data,
            bool useDefinitiveForm,
            bool usePluralForm);

        protected abstract string GetRoom(Dictionary<string, string> data);

        protected abstract string GetSide(Dictionary<string, string> data);

        protected abstract string GetVehicleType(Dictionary<string, string> data, bool useDefinitiveForm);

        protected bool TryGetDevice(Dictionary<string, string> data, out string device)
        {
            device = GetDevice(data);

            return
                !string.IsNullOrWhiteSpace(device) &&
                !MissingValue.Equals(device);
        }

        protected bool TryGetByPerson(Dictionary<string, string> data, out string byPerson)
        {
            byPerson = GetByPerson(data);

            return
                !string.IsNullOrWhiteSpace(byPerson) &&
                !MissingValue.Equals(byPerson);
        }

        public bool TryGetDataValue(Dictionary<string, string> data, string key, out string value)
        {
            value = GetDataValue(data, key, null);

            return
                !string.IsNullOrWhiteSpace(value) &&
                !MissingValue.Equals(value);
        }

        protected bool TryGetPlatform(Dictionary<string, string> data, out string platform)
        {
            platform = GetPlatform(data);

            return
                !string.IsNullOrWhiteSpace(platform) &&
                !MissingValue.Equals(platform);
        }

        protected bool TryGetSide(Dictionary<string, string> data, out string side)
        {
            side = GetSide(data);

            return
                !string.IsNullOrWhiteSpace(side) &&
                !MissingValue.Equals(side);
        }
    }
}
