using System.Collections.Generic;
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
                prefix += $": {log.Time:HH\\:mm} {log.TimeZone}";
            }

            string text = BuildLogTextByTemplate(log);

            return $"{prefix}: {text}";
        }

        static string BuildLogTextByTemplate(PersonalLog log)
        {
            if (log.Template.Equals(PersonalLogTemplate.AccountActivation))
            {
                return BuildAccountActivationLogText(log);
            }
            else if (log.Template.Equals(PersonalLogTemplate.AccountDeactivation))
            {
                return BuildAccountDeactivationLogText(log);
            }
            else if (log.Template.Equals(PersonalLogTemplate.AccountDeletion))
            {
                return BuildAccountDeletionLogText(log);
            }
            else if (log.Template.Equals(PersonalLogTemplate.AccountDeletionRequest))
            {
                return BuildAccountDeletionRequestLogText(log);
            }
            else if (log.Template.Equals(PersonalLogTemplate.AccountRegistration))
            {
                return BuildAccountRegistrationLogText(log);
            }
            else if (log.Template.Equals(PersonalLogTemplate.AccountEmailAddressChange))
            {
                return BuildAccountEmailAddressChangeLogText(log);
            }
            else if (log.Template.Equals(PersonalLogTemplate.AccountFeatureEnablement))
            {
                return BuildAccountFeatureEnablementLogText(log);
            }
            else if (log.Template.Equals(PersonalLogTemplate.AccountFeatureDisablement))
            {
                return BuildAccountFeatureDisablementLogText(log);
            }
            else if (log.Template.Equals(PersonalLogTemplate.AccountPasswordChange))
            {
                return BuildAccountPasswordChangeLogText(log);
            }
            else if (log.Template.Equals(PersonalLogTemplate.AccountPersonalNameChange))
            {
                return BuildAccountPersonalNameChangeLogText(log);
            }
            else if (log.Template.Equals(PersonalLogTemplate.AccountPhoneNumberAddition))
            {
                return BuildAccountPhoneNumberAdditionLogText(log);
            }
            else if (log.Template.Equals(PersonalLogTemplate.AccountPhoneNumberChange))
            {
                return BuildAccountPhoneNumberChangeLogText(log);
            }
            else if (log.Template.Equals(PersonalLogTemplate.AccountPhoneNumberRemoval))
            {
                return BuildAccountPhoneNumberRemovalLogText(log);
            }
            else if (log.Template.Equals(PersonalLogTemplate.AccountProfilePictureChange))
            {
                return BuildAccountProfilePictureChangeLogText(log);
            }
            else if (log.Template.Equals(PersonalLogTemplate.AccountSubscriptionPurchase))
            {
                return BuildAccountSubscriptionPurchaseLogText(log);
            }
            else if (log.Template.Equals(PersonalLogTemplate.AccountUsernameChange))
            {
                return BuildAccountUsernameChangeLogText(log);
            }
            else if (log.Template.Equals(PersonalLogTemplate.BloodDonation))
            {
                return BuildBloodDonationLogText(log);
            }
            else if (log.Template.Equals(PersonalLogTemplate.DeliveryReceival))
            {
                return BuildDeliveryReceivalLogText(log);
            }
            else if (log.Template.Equals(PersonalLogTemplate.InternshipApplicationSubmission))
            {
                return BuildInternshipApplicationSubmissionLogText(log);
            }
            else if (log.Template.Equals(PersonalLogTemplate.MealVoucherCardCreditation))
            {
                return BuildMealVoucherCardCreditationLogText(log);
            }
            else if (log.Template.Equals(PersonalLogTemplate.OnlineReviewSubmission))
            {
                return BuildOnlineReviewSubmissionLogText(log);
            }
            else if (log.Template.Equals(PersonalLogTemplate.OnlineStorePurchase))
            {
                return BuildOnlineStorePurchaseLogText(log);
            }
            else if (log.Template.Equals(PersonalLogTemplate.VideoUpload))
            {
                return BuildVideoUploadLogText(log);
            }

            return log.Data["text"];
        }

        static string BuildAccountActivationLogText(PersonalLog log)
        {
            string text = $"I have activated the {log.Data["platform"]} account";
            string discriminator = GetDiscriminator(log.Data);

            if (!string.IsNullOrWhiteSpace(discriminator))
            {
                text += $" ({discriminator})";
            }

            return text;
        }

        static string BuildAccountDeactivationLogText(PersonalLog log)
        {
            string text = $"I have deactivated the {log.Data["platform"]} account";
            string discriminator = GetDiscriminator(log.Data);

            if (!string.IsNullOrWhiteSpace(discriminator))
            {
                text += $" ({discriminator})";
            }

            return text;
        }

        static string BuildAccountDeletionLogText(PersonalLog log)
        {
            string text = $"I have deleted the {log.Data["platform"]} account";
            string discriminator = GetDiscriminator(log.Data);

            if (!string.IsNullOrWhiteSpace(discriminator))
            {
                text += $" ({discriminator})";
            }

            return text;
        }

        static string BuildAccountDeletionRequestLogText(PersonalLog log)
        {
            string text = $"I have requested the deletion of the {log.Data["platform"]} account";
            string discriminator = GetDiscriminator(log.Data);

            if (!string.IsNullOrWhiteSpace(discriminator))
            {
                text += $" ({discriminator})";
            }

            if (log.Data.TryGetValue("request_method", out string requestMethod))
            {
                text += $" via {requestMethod}";
            }

            return text;
        }

        static string BuildAccountRegistrationLogText(PersonalLog log)
        {
            string text = $"I have registered the {log.Data["platform"]} account";
            string discriminator = GetDiscriminator(log.Data);

            if (!string.IsNullOrWhiteSpace(discriminator))
            {
                text += $" ({discriminator})";
            }

            int withCount = 0;

            if (log.Data.TryGetValue("username", out string username))
            {
                text += $" with the username {username}";
                withCount += 1;
            }

            if (log.Data.TryGetValue("email_address", out string emailAddress))
            {
                if (withCount > 0)
                {
                    text += " and";
                }

                text += $" with the email address {emailAddress}";
                withCount += 1;
            }

            return text;
        }

        static string BuildAccountEmailAddressChangeLogText(PersonalLog log)
        {
            string verb = "changed";

            if (!log.Data.ContainsKey("old_email_address") && log.Data.ContainsKey("new_email_address"))
            {
                verb = "set";
            }

            string text = $"I have {verb} the e-mail address of the {log.Data["platform"]} account";
            string discriminator = GetDiscriminator(log.Data);

            if (!string.IsNullOrWhiteSpace(discriminator))
            {
                text += $" ({discriminator})";
            }

            if (log.Data.TryGetValue("old_email_address", out string oldEmailAddress))
            {
                text += $" from {oldEmailAddress}";
            }

            if (log.Data.TryGetValue("new_email_address", out string newEmailAddress))
            {
                text += $" to {newEmailAddress}";
            }

            return text;
        }

        static string BuildAccountFeatureEnablementLogText(PersonalLog log)
        {
            string text = $"I have enabled the {log.Data["feature_name"]} feature for the {log.Data["platform"]} account";
            string discriminator = GetDiscriminator(log.Data);

            if (!string.IsNullOrWhiteSpace(discriminator))
            {
                text += $" ({discriminator})";
            }

            return text;
        }

        static string BuildAccountFeatureDisablementLogText(PersonalLog log)
        {
            string text = $"I have disabled the {log.Data["feature_name"]} feature for the {log.Data["platform"]} account";
            string discriminator = GetDiscriminator(log.Data);

            if (!string.IsNullOrWhiteSpace(discriminator))
            {
                text += $" ({discriminator})";
            }

            return text;
        }

        static string BuildAccountPasswordChangeLogText(PersonalLog log)
        {
            string text = $"I have changed the password for the {log.Data["platform"]} account";
            string discriminator = GetDiscriminator(log.Data);

            if (!string.IsNullOrWhiteSpace(discriminator))
            {
                text += $" ({discriminator})";
            }

            return text;
        }

        static string BuildAccountPersonalNameChangeLogText(PersonalLog log)
        {
            string text = $"I have changed the personal name for the {log.Data["platform"]} account";
            string discriminator = GetDiscriminator(log.Data);

            if (!string.IsNullOrWhiteSpace(discriminator))
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

        static string BuildAccountPhoneNumberAdditionLogText(PersonalLog log)
        {
            string text = $"I have added a phone number to the {log.Data["platform"]} account";
            string discriminator = GetDiscriminator(log.Data);

            if (!string.IsNullOrWhiteSpace(discriminator))
            {
                text += $" ({discriminator})";
            }

            if (log.Data.TryGetValue("phone_number", out string newPhoneNumber))
            {
                text += $": {newPhoneNumber}";
            }

            return text;
        }

        static string BuildAccountPhoneNumberChangeLogText(PersonalLog log)
        {
            string verb = "changed";

            if (!log.Data.ContainsKey("old_phone_number"))
            {
                verb = "set";
            }

            string text = $"I have {verb} the phone number for the {log.Data["platform"]} account";
            string discriminator = GetDiscriminator(log.Data);

            if (!string.IsNullOrWhiteSpace(discriminator))
            {
                text += $" ({discriminator})";
            }

            if (log.Data.TryGetValue("old_phone_number", out string oldPhoneNumber))
            {
                text += $" from {oldPhoneNumber}";
            }

            if (log.Data.TryGetValue("new_phone_number", out string newPhoneNumber))
            {
                text += $" to {newPhoneNumber}";
            }

            return text;
        }

        static string BuildAccountPhoneNumberRemovalLogText(PersonalLog log)
        {
            string text = $"I have removed a phone number from the {log.Data["platform"]} account";
            string discriminator = GetDiscriminator(log.Data);

            if (!string.IsNullOrWhiteSpace(discriminator))
            {
                text += $" ({discriminator})";
            }

            if (log.Data.TryGetValue("phone_number", out string newPhoneNumber))
            {
                text += $": {newPhoneNumber}";
            }

            return text;
        }

        static string BuildAccountProfilePictureChangeLogText(PersonalLog log)
        {
            string text = $"I have changed the profile picture of the {log.Data["platform"]} account";
            string discriminator = GetDiscriminator(log.Data);

            if (!string.IsNullOrWhiteSpace(discriminator))
            {
                text += $" ({discriminator})";
            }

            return text;
        }

        static string BuildAccountSubscriptionPurchaseLogText(PersonalLog log)
        {
            string text = $"I have purchased a";

            if (log.Data.TryGetValue("subscription_name", out string subscriptionType))
            {
                text += $" {subscriptionType}";
            }

            text += $" subscription for the {log.Data["platform"]} account";
            string discriminator = GetDiscriminator(log.Data);

            if (!string.IsNullOrWhiteSpace(discriminator))
            {
                text += $" ({discriminator})";
            }

            return text;
        }

        static string BuildAccountUsernameChangeLogText(PersonalLog log)
        {
            string verb = "changed";

            if (!log.Data.ContainsKey("old_username"))
            {
                verb = "set";
            }

            string text = $"I have {verb} the username for the {log.Data["platform"]} account";
            string discriminator = GetDiscriminator(log.Data);

            if (!string.IsNullOrWhiteSpace(discriminator))
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

        static string BuildBloodDonationLogText(PersonalLog log)
        {
            string text = $"I have donated blood";

            if (log.Data.TryGetValue("donation_centre_name", out string donationCentreName))
            {
                text += $" at {donationCentreName}";
            }

            return text;
        }

        static string BuildDeliveryReceivalLogText(PersonalLog log)
        {
            string text = $"I have received the delivery of {log.Data["package_description"]}";

            if (log.Data.TryGetValue("tracking_number", out string trackingNumber))
            {
                text += $" with the tracking number {trackingNumber}";
            }

            if (log.Data.TryGetValue("company_name", out string companyName))
            {
                text += $" via {companyName}";
            }

            return text;
        }

        static string BuildInternshipApplicationSubmissionLogText(PersonalLog log)
        {
            string internshipType = "internship";

            if (log.Data.TryGetValue("period", out string period))
            {
                internshipType = $"{period} {internshipType}";
            }

            string text = $"I have submitted an application";


            if (log.Data.TryGetValue("contact_person_name", out string contactPersonName))
            {
                text += $" to {contactPersonName}";
            }

            text += $" for an {internshipType} at {log.Data["company_name"]}";

            if (log.Data.TryGetValue("position_name", out string positionName))
            {
                text += $" for the position of {positionName}";
            }

            text = text.Replace("an summer", "a summer");

            return text;
        }

        static string BuildMealVoucherCardCreditationLogText(PersonalLog log)
        {
            string text = $"My meal voucher card was credited with {log.Data["amount"]} {log.Data["currency"]}";

            return text;
        }

        static string BuildOnlineReviewSubmissionLogText(PersonalLog log)
        {
            string text = $"I have submitted a";

            if (log.Data.TryGetValue("stars_count", out string starsCount))
            {
                text += $" {starsCount}-star";
            }

            text += $" review on {log.Data["platform"]} for {log.Data["subject_name"]}";

            if (log.Data.TryGetValue("account", out string account))
            {
                text += $" using the {account} account";
            }

            return text;
        }

        static string BuildOnlineStorePurchaseLogText(PersonalLog log)
        {
            string text = $"I have purchased {log.Data["product_name"]} from {log.Data["platform"]}";

            if (log.Data.TryGetValue("account", out string account))
            {
                text += $" using the {account} account";
            }

            return text;
        }

        static string BuildVideoUploadLogText(PersonalLog log)
        {
            string text = $"I have uploaded a video titled '{log.Data["video_title"]}' to {log.Data["platform"]}";

            if (log.Data.TryGetValue("account", out string account))
            {
                text += $" using the {account} account";
            }

            if (log.Data.TryGetValue("uploaded_file_name", out string uploadedFileName))
            {
                text += $", from the '{uploadedFileName}' file";
            }

            if (log.Data.TryGetValue("video_url", out string videoId))
            {
                text += $", at {videoId}";
            }

            return text;
        }

        static string GetDiscriminator(Dictionary<string, string> data)
        {
            data.TryGetValue("discriminator", out string discriminator);

            if (string.IsNullOrWhiteSpace(discriminator))
            {
                data.TryGetValue("username", out discriminator);
            }

            if (string.IsNullOrWhiteSpace(discriminator))
            {
                data.TryGetValue("email_address", out discriminator);
            }

            return discriminator;
        }
    }
}
