using PersonalLogManager.Service.Models;

namespace PersonalLogManager.Service
{
    public class PersonalLogTextBuilder() : IPersonalLogTextBuilder
    {
        public string BuildLogText(PersonalLog log)
        {
            string prefix = $"{log.Date:yyyy-MM-dd}";

            if (log.Time is not null)
            {
                prefix += $" {log.Time:hh\\:mm}";
            }

            string text = BuildLogTextByTemplate(log);

            return $"{prefix}: {text}";
        }

        string BuildLogTextByTemplate(PersonalLog log)
        {
            if (log.Template.Equals(PersonalLogTemplate.AccountRegistration))
            {
                return BuildAccountRegistrationLogText(log);
            }
            else if (log.Template.Equals(PersonalLogTemplate.AccountPasswordChange))
            {
                return BuildAccountPasswordChangeLogText(log);
            }
            else if (log.Template.Equals(PersonalLogTemplate.AccountPersonalNameChange))
            {
                return BuildAccountPersonalNameChangeLogText(log);
            }
            else if (log.Template.Equals(PersonalLogTemplate.AccountUsernameChange))
            {
                return BuildAccountUsernameChangeLogText(log);
            }

            return log.Data["text"];
        }

        string BuildAccountRegistrationLogText(PersonalLog log)
        {
            string text = $"I have registered the {log.Data["platform"]} account";

            if (log.Data.TryGetValue("discriminator", out string discriminator))
            {
                text += $" ({discriminator})";
            }

            if (log.Data.TryGetValue("email_address", out string emailAddress))
            {
                text += $" with the email address {emailAddress}";
            }

            return text;
        }

        string BuildAccountPasswordChangeLogText(PersonalLog log)
        {
            string text = $"I have changed the password for the {log.Data["platform"]} account";

            if (log.Data.TryGetValue("discriminator", out string discriminator))
            {
                text += $" ({discriminator})";
            }

            return text;
        }

        string BuildAccountPersonalNameChangeLogText(PersonalLog log)
        {
            string text = $"I have changed the personal name for the {log.Data["platform"]} account";

            if (log.Data.TryGetValue("discriminator", out string discriminator))
            {
                text += $" ({discriminator})";
            }

            if (log.Data.TryGetValue("old_personal_name", out string oldPersonalName))
            {
                text += $" from {oldPersonalName}";
            }

            if (log.Data.TryGetValue("new_personal_name", out string newPersonalName))
            {
                text += $" to {newPersonalName}";
            }

            return text;
        }

        string BuildAccountUsernameChangeLogText(PersonalLog log)
        {
            string verb = "changed";

            if (!log.Data.ContainsKey("old_username"))
            {
                verb = "set";
            }

            string text = $"I have {verb} the username for the {log.Data["platform"]} account";

            if (log.Data.TryGetValue("discriminator", out string discriminator))
            {
                text += $" ({discriminator})";
            }

            if (log.Data.TryGetValue("old_username", out string oldUsername))
            {
                text += $" from {oldUsername}";
            }

            if (log.Data.TryGetValue("new_username", out string newUsername))
            {
                text += $" to {newUsername}";
            }

            return text;
        }
    }
}
