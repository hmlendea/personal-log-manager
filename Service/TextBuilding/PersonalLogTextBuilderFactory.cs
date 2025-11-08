using System;
using System.Reflection;
using PersonalLogManager.Service.Models;
using PersonalLogManager.Service.TextBuilding.Localisation;

namespace PersonalLogManager.Service.TextBuilding
{
    public class PersonalLogTextBuilderFactory() : IPersonalLogTextBuilderFactory
    {
        public string BuildLogText(PersonalLog log, string localisation)
        {
            string prefix = $"{log.Date:yyyy-MM-dd}";

            if (log.Time is not null)
            {
                prefix += $": {log.Time:HH\\:mm} {log.TimeZone}";
            }

            string text = BuildLogTextByTemplate(log, localisation);

            return $"{prefix}: {text}";
        }

        static string BuildLogTextByTemplate(PersonalLog log, string localisation)
        {
            var personalLogTextBuilder = GetTextBuilder(localisation);

            return personalLogTextBuilder.GetType()
                .GetMethod(
                    $"Build{log.Template}LogText",
                    BindingFlags.Public | BindingFlags.Instance)
                .Invoke(personalLogTextBuilder, [log]) as string;
        }

        static IPersonalLogTextBuilder GetTextBuilder(string localisation)
        {
            if (localisation.Equals("ro-RO", StringComparison.InvariantCultureIgnoreCase) ||
                localisation.Equals("ro-MD", StringComparison.InvariantCultureIgnoreCase) ||
                localisation.Equals("ro", StringComparison.InvariantCultureIgnoreCase))
            {
                return new RomanianTextBuilder();
            }

            return new EnglishTextBuilder();
        }
    }
}
