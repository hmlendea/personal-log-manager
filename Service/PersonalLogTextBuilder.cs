using System.Collections.Generic;
using Microsoft.Extensions.WebEncoders.Testing;
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
            else if (log.Template.Equals(PersonalLogTemplate.AccountContactEmailAddressChange))
            {
                return BuildAccountContactEmailAddressChangeLogText(log);
            }
            else if (log.Template.Equals(PersonalLogTemplate.AccountDataExport))
            {
                return BuildAccountDataExportLogText(log);
            }
            else if (log.Template.Equals(PersonalLogTemplate.AccountDataExportRequest))
            {
                return BuildAccountDataExportRequestLogText(log);
            }
            else if (log.Template.Equals(PersonalLogTemplate.AccountDataExportRequestFulfillment))
            {
                return BuildAccountDataExportRequestFulfillmentLogText(log);
            }
            else if (log.Template.Equals(PersonalLogTemplate.AccountDataExportSave))
            {
                return BuildAccountDataExportSaveLogText(log);
            }
            else if (log.Template.Equals(PersonalLogTemplate.AccountDataObfuscation))
            {
                return BuildAccountDataObfuscationLogText(log);
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
            else if (log.Template.Equals(PersonalLogTemplate.AccountDeletionRequestFulfillment))
            {
                return BuildAccountDeletionRequestFulfillmentLogText(log);
            }
            else if (log.Template.Equals(PersonalLogTemplate.AccountDeletionValidation))
            {
                return BuildAccountDeletionValidationLogText(log);
            }
            else if (log.Template.Equals(PersonalLogTemplate.AccountEmailAddressChange))
            {
                return BuildAccountEmailAddressChangeLogText(log);
            }
            else if (log.Template.Equals(PersonalLogTemplate.AccountEmailAddressChangeRequest))
            {
                return BuildAccountEmailAddressChangeRequestLogText(log);
            }
            else if (log.Template.Equals(PersonalLogTemplate.AccountEmailAddressChangeRequestFulfillment))
            {
                return BuildAccountEmailAddressChangeRequestFulfillmentLogText(log);
            }
            else if (log.Template.Equals(PersonalLogTemplate.AccountEmailAddressConfirmation))
            {
                return BuildAccountEmailAddressConfirmationLogText(log);
            }
            else if (log.Template.Equals(PersonalLogTemplate.AccountFeatureEnablement))
            {
                return BuildAccountFeatureEnablementLogText(log);
            }
            else if (log.Template.Equals(PersonalLogTemplate.AccountFeatureDisablement))
            {
                return BuildAccountFeatureDisablementLogText(log);
            }
            else if (log.Template.Equals(PersonalLogTemplate.AccountIdentityVerification))
            {
                return BuildAccountIdentityVerificationLogText(log);
            }
            else if (log.Template.Equals(PersonalLogTemplate.AccountLinking))
            {
                return BuildAccountLinkingLogText(log);
            }
            else if (log.Template.Equals(PersonalLogTemplate.AccountMessagesErasure))
            {
                return BuildAccountMessagesErasureLogText(log);
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
            else if (log.Template.Equals(PersonalLogTemplate.AccountRecovery))
            {
                return BuildAccountRecoveryLogText(log);
            }
            else if (log.Template.Equals(PersonalLogTemplate.AccountRecoveryEmailAddressChange))
            {
                return BuildAccountRecoveryEmailAddressChangeLogText(log);
            }
            else if (log.Template.Equals(PersonalLogTemplate.AccountRegistration))
            {
                return BuildAccountRegistrationLogText(log);
            }
            else if (log.Template.Equals(PersonalLogTemplate.AccountRegistrationRequest))
            {
                return BuildAccountRegistrationRequestLogText(log);
            }
            else if (log.Template.Equals(PersonalLogTemplate.AccountRegistrationRequestFulfillment))
            {
                return BuildAccountRegistrationRequestFulfillmentLogText(log);
            }
            else if (log.Template.Equals(PersonalLogTemplate.AccountSubscriptionPurchase))
            {
                return BuildAccountSubscriptionPurchaseLogText(log);
            }
            else if (log.Template.Equals(PersonalLogTemplate.AccountUnlinking))
            {
                return BuildAccountUnlinkingLogText(log);
            }
            else if (log.Template.Equals(PersonalLogTemplate.AccountUsernameChange))
            {
                return BuildAccountUsernameChangeLogText(log);
            }
            else if (log.Template.Equals(PersonalLogTemplate.AccountVisibilityMadePrivate))
            {
                return BuildAccountVisibilityMadePrivateLogText(log);
            }
            else if (log.Template.Equals(PersonalLogTemplate.AccountVisibilityMadePublic))
            {
                return BuildAccountVisibilityMadePublicLogText(log);
            }
            else if (log.Template.Equals(PersonalLogTemplate.BloodDonation))
            {
                return BuildBloodDonationLogText(log);
            }
            else if (log.Template.Equals(PersonalLogTemplate.BloodGlucoseMeasurement))
            {
                return BuildBloodGlucoseMeasurementLogText(log);
            }
            else if (log.Template.Equals(PersonalLogTemplate.BodyWeightMeasurement))
            {
                return BuildBodyWeightMeasurementLogText(log);
            }
            else if (log.Template.Equals(PersonalLogTemplate.DatingAppMatch))
            {
                return BuildDatingAppMatchLogText(log);
            }
            else if (log.Template.Equals(PersonalLogTemplate.DeliveryReceival))
            {
                return BuildDeliveryReceivalLogText(log);
            }
            else if (log.Template.Equals(PersonalLogTemplate.DentalScaling))
            {
                return BuildDentalScalingLogText(log);
            }
            else if (log.Template.Equals(PersonalLogTemplate.EmailExport))
            {
                return BuildEmailExportLogText(log);
            }
            else if (log.Template.Equals(PersonalLogTemplate.InternshipApplicationSubmission))
            {
                return BuildInternshipApplicationSubmissionLogText(log);
            }
            else if (log.Template.Equals(PersonalLogTemplate.MealVoucherCardCreditation))
            {
                return BuildMealVoucherCardCreditationLogText(log);
            }
            else if (log.Template.Equals(PersonalLogTemplate.ObjectSale))
            {
                return BuildObjectSaleLogText(log);
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
            else if (log.Template.Equals(PersonalLogTemplate.WorkFromTheOffice))
            {
                return BuildWorkFromTheOfficeLogText(log);
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

        static string BuildAccountContactEmailAddressChangeLogText(PersonalLog log)
        {
            string verb = "changed";

            if (!log.Data.ContainsKey("old_contact_email_address") && log.Data.ContainsKey("new_contact_email_address"))
            {
                verb = "set";
            }

            string text = $"I have {verb} the contact e-mail address of the {log.Data["platform"]} account";
            string discriminator = GetDiscriminator(log.Data);

            if (!string.IsNullOrWhiteSpace(discriminator))
            {
                text += $" ({discriminator})";
            }

            if (log.Data.TryGetValue("old_contact_email_address", out string oldContactEmailAddress))
            {
                text += $" from {oldContactEmailAddress}";
            }

            if (log.Data.TryGetValue("new_contact_email_address", out string newContactEmailAddress))
            {
                text += $" to {newContactEmailAddress}";
            }

            return text;
        }

        static string BuildAccountDataExportLogText(PersonalLog log)
        {
            string text = $"I have exported my data related to the {log.Data["platform"]} account";
            string discriminator = GetDiscriminator(log.Data);

            if (!string.IsNullOrWhiteSpace(discriminator))
            {
                text += $" ({discriminator})";
            }

            return text;
        }

        static string BuildAccountDataExportRequestLogText(PersonalLog log)
        {
            string text = $"I have requested an export of my data related to the {log.Data["platform"]} account";
            string discriminator = GetDiscriminator(log.Data);

            if (!string.IsNullOrWhiteSpace(discriminator))
            {
                text += $" ({discriminator})";
            }

            if (log.Data.TryGetValue("request_method", out string requestMethod))
            {
                text += $" via {requestMethod}";

                if (log.Data.TryGetValue("request_id", out string requestId))
                {
                    text += $" ({requestId})";
                }
            }

            return text;
        }

        static string BuildAccountDataExportRequestFulfillmentLogText(PersonalLog log)
        {
            string text = $"My data export request for the {log.Data["platform"]} account";

            string discriminator = GetDiscriminator(log.Data);

            if (!string.IsNullOrWhiteSpace(discriminator))
            {
                text += $" ({discriminator})";
            }

            if (log.Data.TryGetValue("request_date", out string requestDate))
            {
                text += $", made on {requestDate},";
            }

            text += " has been fulfilled";

            return text;
        }

        static string BuildAccountDataExportSaveLogText(PersonalLog log)
        {
            string text = $"I have saved the export of the data related to the {log.Data["platform"]} account";
            string discriminator = GetDiscriminator(log.Data);

            if (!string.IsNullOrWhiteSpace(discriminator))
            {
                text += $" ({discriminator})";
            }

            if (log.Data.TryGetValue("request_date", out string requestDate))
            {
                text += $", following the data export request sent on {requestDate}";
            }

            return text;
        }

        static string BuildAccountDataObfuscationLogText(PersonalLog log)
        {
            string text = $"I have obfuscated the data related of the {log.Data["platform"]} account";
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

        static string BuildAccountDeletionRequestFulfillmentLogText(PersonalLog log)
        {
            string text = $"My account deletion request for the {log.Data["platform"]} account";
            string discriminator = GetDiscriminator(log.Data);

            if (!string.IsNullOrWhiteSpace(discriminator))
            {
                text += $" ({discriminator})";
            }

            if (log.Data.TryGetValue("request_date", out string requestDate))
            {
                text += $", made on {requestDate},";
            }

            text += " has been fulfilled";

            return text;
        }

        static string BuildAccountDeletionValidationLogText(PersonalLog log)
        {
            string text = $"I have validated that the {log.Data["platform"]} account";
            string discriminator = GetDiscriminator(log.Data);

            if (!string.IsNullOrWhiteSpace(discriminator))
            {
                text += $" ({discriminator})";
            }

            text += " has been deleted";

            if (log.Data.TryGetValue("request_date", out string requestDate))
            {
                text += $", following the deletion request sent on {requestDate}";
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

        static string BuildAccountEmailAddressChangeRequestLogText(PersonalLog log)
        {
            string text = $"I have requested to change the e-mail address of the {log.Data["platform"]} account";
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

            if (log.Data.TryGetValue("request_method", out string requestMethod))
            {
                text += $", via {requestMethod}";
            }

            return text;
        }

        static string BuildAccountEmailAddressChangeRequestFulfillmentLogText(PersonalLog log)
        {
            string text = $"My e-mail address change request for the {log.Data["platform"]} account";

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

            if (log.Data.TryGetValue("request_date", out string requestDate))
            {
                text += $", made on {requestDate},";
            }

            text += " has been fulfilled";

            return text;
        }

        static string BuildAccountEmailAddressConfirmationLogText(PersonalLog log)
        {
            string text = $"I have confirmed the e-mail address";

            if (log.Data.TryGetValue("email_address", out string emailAddress))
            {
                text += $" ({emailAddress})";
            }

            text += $" for the {log.Data["platform"]} account";

            string discriminator = GetDiscriminator(log.Data);

            if (!string.IsNullOrWhiteSpace(discriminator))
            {
                text += $" ({discriminator})";
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

        static string BuildAccountIdentityVerificationLogText(PersonalLog log)
        {
            string text = $"I have verified my identity for the {log.Data["platform"]} account";
            string discriminator = GetDiscriminator(log.Data);

            if (!string.IsNullOrWhiteSpace(discriminator))
            {
                text += $" ({discriminator})";
            }

            return text;
        }

        static string BuildAccountLinkingLogText(PersonalLog log)
        {
            string text = $"I have linked the {log.Data["platform"]} account";
            string discriminator = GetDiscriminator(log.Data);

            if (!string.IsNullOrWhiteSpace(discriminator))
            {
                text += $" ({discriminator})";
            }

            text += $" with the {log.Data["platform_linked"]} account";

            if (log.Data.TryGetValue("account_linked", out string accountLinked))
            {
                text += $" ({accountLinked})";
            }

            return text;
        }

        static string BuildAccountMessagesErasureLogText(PersonalLog log)
        {
            string text = $"I have erased all messages from the {log.Data["platform"]} account";
            string discriminator = GetDiscriminator(log.Data);

            if (!string.IsNullOrWhiteSpace(discriminator))
            {
                text += $" ({discriminator})";
            }

            return text;
        }

        static string BuildAccountPasswordChangeLogText(PersonalLog log)
        {
            string text = $"I have changed the password of the {log.Data["platform"]} account";
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

        static string BuildAccountRecoveryLogText(PersonalLog log)
        {
            string text = $"I have recovered the {log.Data["platform"]} account";
            string discriminator = GetDiscriminator(log.Data);

            if (!string.IsNullOrWhiteSpace(discriminator))
            {
                text += $" ({discriminator})";
            }

            return text;
        }

        static string BuildAccountRecoveryEmailAddressChangeLogText(PersonalLog log)
        {
            string verb = "changed";

            if (!log.Data.ContainsKey("old_email_address") && log.Data.ContainsKey("new_email_address"))
            {
                verb = "set";
            }

            string text = $"I have {verb} the recovery e-mail address of the {log.Data["platform"]} account";
            string discriminator = GetDiscriminator(log.Data);

            if (!string.IsNullOrWhiteSpace(discriminator))
            {
                text += $" ({discriminator})";
            }

            if (log.Data.TryGetValue("old_recovery_email_address", out string oldEmailAddress))
            {
                text += $" from {oldEmailAddress}";
            }

            if (log.Data.TryGetValue("new_recovery_email_address", out string newEmailAddress))
            {
                text += $" to {newEmailAddress}";
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

            if (log.Data.TryGetValue("phone_number", out string phoneNumber))
            {
                if (withCount > 0)
                {
                    text += ", and";
                }

                text += $" with the phone number {phoneNumber}";
                withCount += 1;
            }

            if (log.Data.TryGetValue("email_address", out string emailAddress))
            {
                if (withCount > 0)
                {
                    text += ", and";
                }

                text += $" with the email address {emailAddress}";
                withCount += 1;
            }

            return text;
        }

        static string BuildAccountRegistrationRequestLogText(PersonalLog log)
        {
            string text = $"I have requested the registration of the {log.Data["platform"]} account";
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

            if (log.Data.TryGetValue("phone_number", out string phoneNumber))
            {
                if (withCount > 0)
                {
                    text += ", and";
                }

                text += $" with the phone number {phoneNumber}";
                withCount += 1;
            }

            if (log.Data.TryGetValue("email_address", out string emailAddress))
            {
                if (withCount > 0)
                {
                    text += ", and";
                }

                text += $" with the email address {emailAddress}";
                withCount += 1;
            }

            return text;;
        }

        static string BuildAccountRegistrationRequestFulfillmentLogText(PersonalLog log)
        {
            string text = $"My account registration request for the {log.Data["platform"]} account";
            string discriminator = GetDiscriminator(log.Data);

            if (!string.IsNullOrWhiteSpace(discriminator))
            {
                text += $" ({discriminator})";
            }

            if (log.Data.TryGetValue("request_date", out string requestDate))
            {
                text += $", made on {requestDate},";
            }

            text += " has been fulfilled";

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

            if (log.Data.TryGetValue("price_amount", out string priceAmount))
            {
                text += $" for {priceAmount} {log.Data["price_currency"]}";
            }

            return text;
        }

        static string BuildAccountUnlinkingLogText(PersonalLog log)
        {
            string text = $"I have removed the link between the {log.Data["platform"]} account";
            string discriminator = GetDiscriminator(log.Data);

            if (!string.IsNullOrWhiteSpace(discriminator))
            {
                text += $" ({discriminator})";
            }

            text += $" and the {log.Data["platform_unlinked"]} account";

            if (log.Data.TryGetValue("account_unlinked", out string accountLinked))
            {
                text += $" ({accountLinked})";
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

        static string BuildAccountVisibilityMadePrivateLogText(PersonalLog log)
        {
            string text = $"I have made my {log.Data["platform"]} account";
            string discriminator = GetDiscriminator(log.Data);

            if (!string.IsNullOrWhiteSpace(discriminator))
            {
                text += $" ({discriminator})";
            }

            text += " private";

            return text;
        }

        static string BuildAccountVisibilityMadePublicLogText(PersonalLog log)
        {
            string text = $"I have made my {log.Data["platform"]} account";
            string discriminator = GetDiscriminator(log.Data);

            if (!string.IsNullOrWhiteSpace(discriminator))
            {
                text += $" ({discriminator})";
            }

            text += " public";

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

        static string BuildBloodGlucoseMeasurementLogText(PersonalLog log)
        {
            log.Data.TryGetValue("unit", out string unit);

            if (string.IsNullOrWhiteSpace(unit))
            {
                unit = "mg/dL";
            }

            string text = $"My blood glucose level measured {log.Data["glucose_level"]} {unit}";

            return text;
        }

        static string BuildBodyWeightMeasurementLogText(PersonalLog log)
        {
            log.Data.TryGetValue("unit", out string unit);

            if (string.IsNullOrWhiteSpace(unit))
            {
                unit = "kg";
            }

            string text = $"My body weight measured {log.Data["body_weight"]} {unit}";

            return text;
        }

        static string BuildDatingAppMatchLogText(PersonalLog log)
        {
            string text = $"I have matched with {log.Data["match_name"]} on {log.Data["platform"]}";
            string discriminator = GetDiscriminator(log.Data);

            if (!string.IsNullOrWhiteSpace(discriminator))
            {
                text += $" ({discriminator})";
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

        static string BuildDentalScalingLogText(PersonalLog log)
        {
            string text = $"I have undergone a dental scaling procedure";

            if (log.Data.TryGetValue("clinic_name", out string clinicName))
            {
                text += $" at {clinicName}";
            }

            return text;
        }

        static string BuildEmailExportLogText(PersonalLog log)
        {
            string text = $"I have exported all of the emails from the {log.Data["platform"]} account";

            if (log.Data.TryGetValue("account", out string account))
            {
                text += $" ({account})";
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

        static string BuildObjectSaleLogText(PersonalLog log)
        {
            string text = $"I have sold the {log.Data["object_name"]}";

            if (log.Data.TryGetValue("platform", out string platform))
            {
                text += $" on {platform}";
            }

            string discriminator = GetDiscriminator(log.Data);

            if (!string.IsNullOrWhiteSpace(discriminator))
            {
                text += $" ({discriminator})";
            }

            if (log.Data.TryGetValue("price_amount", out string priceAmount))
            {
                text += $" for {priceAmount} {log.Data["price_currency"]}";
            }

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

        static string BuildWorkFromTheOfficeLogText(PersonalLog log)
        {
            string text = $"I have worked from the office";

            if (log.Data.TryGetValue("office_name", out string officeName))
            {
                text += $" ({officeName})";
            }

            return text;
        }

        static string GetDiscriminator(Dictionary<string, string> data)
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
    }
}
