using System;
using System.Reflection;
using NuciText.Normalisation;
using NuciText.Obfuscation;
using PersonalLogManager.Service.Models;
using PersonalLogManager.Service.TextBuilding.Localisation;

namespace PersonalLogManager.Service.TextBuilding
{
    public class PersonalLogTextBuilderFactory(
        INuciTextNormaliser normaliser,
        INuciTextObfuscator obfuscator)
        : IPersonalLogTextBuilderFactory
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

        string BuildLogTextByTemplate(PersonalLog log, string localisation)
        {
            IPersonalLogTextBuilder personalLogTextBuilder = GetTextBuilder(localisation);
            string logText = personalLogTextBuilder
                .GetType()
                .GetMethod(
                    $"Build{log.Template}LogText",
                    BindingFlags.Public | BindingFlags.Instance)
                .Invoke(personalLogTextBuilder, [log]) as string;

            return normaliser.NormaliseSentence(logText);
        }

        IPersonalLogTextBuilder GetTextBuilder(string localisation)
        {
            if (localisation.Equals("ro-RO", StringComparison.InvariantCultureIgnoreCase) ||
                localisation.Equals("ro-MD", StringComparison.InvariantCultureIgnoreCase) ||
                localisation.Equals("ro", StringComparison.InvariantCultureIgnoreCase))
            {
                return new RomanianTextBuilder(obfuscator);
            }

            return new EnglishTextBuilder(obfuscator);
        }
    }
}
