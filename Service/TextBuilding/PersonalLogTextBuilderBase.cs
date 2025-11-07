using System.Collections.Generic;

namespace PersonalLogManager.Service.TextBuilding
{
    public abstract class PersonalLogTextBuilderBase
    {
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
