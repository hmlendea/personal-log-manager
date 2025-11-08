using System.Collections.Generic;

namespace PersonalLogManager.Service.TextBuilding
{
    public abstract class PersonalLogTextBuilderBase
    {
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

            return value;
        }

        public string GetLocalisedValue(Dictionary<string, string> data, string key, string localisation)
            => GetLocalisedValue(data, key, localisation, null);

        public string GetLocalisedValue(Dictionary<string, string> data, string key, string localisation, string defaultValue)
        {
            string value = GetDataValue(data, key);

            if (string.IsNullOrWhiteSpace(value))
            {
                return defaultValue;
            }

            if (localisation.Equals("ro-RO") ||
                localisation.Equals("ro-MD") ||
                localisation.Equals("ro"))
            {
                return value
                    .Replace(" and ", " și ")
                    .Replace("&", "și");
            }

            return value
                .Replace(" și ", " and ")
                .Replace("&", "and");
        }

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

                if (normalisedMapKey.Equals(expectedMapKey, System.StringComparison.InvariantCultureIgnoreCase))
                {
                    mappedValue = mappings[expectedMapKey];
                    break;
                }
            }

            return mappedValue;
        }
    }
}
