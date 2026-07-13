using System;
using System.Collections.Generic;
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
        private static readonly HashSet<string> RomanianLocalisations =
            new(StringComparer.InvariantCultureIgnoreCase)
            {
                "ro",
                "ro-RO",
                "ro-MD"
            };

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
            string methodName = $"Build{log.Template}LogText";

            try
            {
                MethodInfo buildMethod = personalLogTextBuilder
                    .GetType()
                    .GetMethod(methodName, BindingFlags.Public | BindingFlags.Instance);

                if (buildMethod is null)
                {
                    throw new MissingMethodException(personalLogTextBuilder.GetType().Name, methodName);
                }

                string logText = buildMethod.Invoke(personalLogTextBuilder, [log]) as string;

                return normaliser.NormaliseSentence(logText);
            }
            catch (TargetInvocationException ex)
            {
                throw ex.InnerException ?? ex;
            }
        }

        IPersonalLogTextBuilder GetTextBuilder(string localisation)
        {
            if (!string.IsNullOrWhiteSpace(localisation) &&
                RomanianLocalisations.Contains(localisation))
            {
                return new RomanianTextBuilder(obfuscator);
            }

            return new EnglishTextBuilder(obfuscator);
        }
    }
}
