using System.Collections.Generic;

using NuciText.Obfuscation;

using PersonalLogManager.Service.Models;

namespace PersonalLogManager.Service.TextBuilding.Localisation
{
    public class EnglishTextBuilder(INuciTextObfuscator obfuscator)
        : PersonalLogTextBuilderBase(obfuscator), IPersonalLogTextBuilder
    {
        protected override string LanguageCode => "en";

        public string BuildAccessoryCleaningLogText(PersonalLog log)
            => $"I have cleaned {GetAccessoryType(log.Data, true)} by {GetCleaningMethod(log.Data)}" +
                GetLocation(log.Data);

        public string BuildAccountActivationLogText(PersonalLog log)
            => $"I have activated the {GetPlatform(log.Data)} account";

        public string BuildAccountBanningLogText(PersonalLog log)
        {
            string text = $"The {GetPlatform(log.Data)} account has been banned";

            string banReason = GetDataValue(log.Data, "ban_reason");

            if (!string.IsNullOrWhiteSpace(banReason))
            {
                text += $" for the following reason: {banReason}";
            }

            return text;
        }

        public string BuildAccountContactEmailAddressChangeLogText(PersonalLog log)
        {
            string verb = "changed";

            if (!log.Data.ContainsKey("old_contact_email_address") && log.Data.ContainsKey("new_contact_email_address"))
            {
                verb = "set";
            }

            string text = $"I have {verb} the contact e-mail address of the {GetPlatform(log.Data)} account";

            string oldContactEmailAddress = GetDataValue(log.Data, "old_contact_email_address");

            if (!string.IsNullOrWhiteSpace(oldContactEmailAddress))
            {
                text += $" from {oldContactEmailAddress}";
            }

            string newContactEmailAddress = GetDataValue(log.Data, "new_contact_email_address");

            if (!string.IsNullOrWhiteSpace(newContactEmailAddress))
            {
                text += $" to {newContactEmailAddress}";
            }

            return text;
        }

        public string BuildAccountDataExportLogText(PersonalLog log)
            => $"I have exported my data related to the {GetPlatform(log.Data)} account";

        public string BuildAccountDataExportRequestLogText(PersonalLog log)
        {
            string text = $"I have requested an export of my data related to the {GetPlatform(log.Data)} account";
            string requestMethod = GetMappedDataValue(
                log.Data,
                "request_method",
                new()
                {
                    { "AccountSettings", "account settings" },
                    { "ContactForm", "the contact form" },
                    { "EMail", "e-mail" },
                    { "SupportTicket", "support ticket" }
                });

            if (!string.IsNullOrWhiteSpace(requestMethod))
            {
                text += $", via {requestMethod}";

                string requestId = GetDataValue(log.Data, "request_id");

                if (!string.IsNullOrWhiteSpace(requestId))
                {
                    text += $" ({requestId})";
                }
            }

            return text;
        }

        public string BuildAccountDataExportRequestFulfillmentLogText(PersonalLog log)
        {
            string text = $"My data export request for the {GetPlatform(log.Data)} account";

            string requestDate = GetDataValue(log.Data, "request_date");

            if (!string.IsNullOrWhiteSpace(requestDate))
            {
                text += $", made on {requestDate},";
            }

            return $"{text} has been fulfilled";
        }

        public string BuildAccountDataExportSaveLogText(PersonalLog log)
        {
            string text = $"I have saved the export of the data related to the {GetPlatform(log.Data)} account";

            string requestDate = GetDataValue(log.Data, "request_date");
            string requestId = GetDataValue(log.Data, "request_id");

            if (!string.IsNullOrWhiteSpace(requestDate) && !string.IsNullOrWhiteSpace(requestId))
            {
                text += $", obtained following the request with the {requestId} identification code from {requestDate}";
            }
            else if (!string.IsNullOrWhiteSpace(requestDate) && string.IsNullOrWhiteSpace(requestId))
            {
                text += $", obtained following the request sent on {requestDate}";
            }
            else if (string.IsNullOrWhiteSpace(requestDate) && !string.IsNullOrWhiteSpace(requestId))
            {
                text += $", obtained following the request with the {requestId} identification code";
            }

            return text;
        }

        public string BuildAccountDataObfuscationLogText(PersonalLog log)
            => $"I have obfuscated the data on the {GetPlatform(log.Data)} account";

        public string BuildAccountDeactivationLogText(PersonalLog log)
            => $"I have deactivated the {GetPlatform(log.Data)} account";

        public string BuildAccountDeletionLogText(PersonalLog log)
            => $"I have deleted the {GetPlatform(log.Data)} account";

        public string BuildAccountDeletionRequestLogText(PersonalLog log)
        {
            string text = $"I have sent a request for the deletion of the {GetPlatform(log.Data)} account";

            string requestId = GetDataValue(log.Data, "request_id");

            if (!string.IsNullOrWhiteSpace(requestId))
            {
                text += $" with the {requestId} identification code";
            }

            string requestMethod = GetMappedDataValue(
                log.Data,
                "request_method",
                new()
                {
                    { "AccountSettings", "account settings" },
                    { "ContactForm", "the contact form" },
                    { "EMail", "e-mail" },
                    { "SupportTicket", "support ticket" }
                });

            if (!string.IsNullOrWhiteSpace(requestMethod))
            {
                text += $", via {requestMethod}";
            }

            return text;
        }

        public string BuildAccountDeletionRequestCancellationLogText(PersonalLog log)
        {
            string text = $"I have cancelled the account deletion request for the {GetPlatform(log.Data)} account";

            string requestId = GetDataValue(log.Data, "request_id");

            if (!string.IsNullOrWhiteSpace(requestId))
            {
                text += $" with the {requestId} identification code";
            }

            string requestDate = GetDataValue(log.Data, "request_date");

            if (!string.IsNullOrWhiteSpace(requestDate))
            {
                text += $", sent on {requestDate},";
            }

            return text;
        }

        public string BuildAccountDeletionRequestFulfillmentLogText(PersonalLog log)
        {
            string text = $"My account deletion request for the {GetPlatform(log.Data)} account";

            string requestId = GetDataValue(log.Data, "request_id");

            if (!string.IsNullOrWhiteSpace(requestId))
            {
                text += $" with the {requestId} identification code";
            }

            string requestDate = GetDataValue(log.Data, "request_date");

            if (!string.IsNullOrWhiteSpace(requestDate))
            {
                text += $", sent on {requestDate},";
            }

            text += " has been fulfilled";

            return text;
        }

        public string BuildAccountDeletionRequestRejectionLogText(PersonalLog log)
        {
            string text = $"My account deletion request for the {GetPlatform(log.Data)} account";

            string requestId = GetDataValue(log.Data, "request_id");

            if (!string.IsNullOrWhiteSpace(requestId))
            {
                text += $" with the {requestId} identification code";
            }

            string requestDate = GetDataValue(log.Data, "request_date");

            if (!string.IsNullOrWhiteSpace(requestDate))
            {
                text += $", sent on {requestDate},";
            }

            text += " has been rejected";

            return text;
        }

        public string BuildAccountDeletionValidationLogText(PersonalLog log)
        {
            string text = $"I have validated that the {GetPlatform(log.Data)} account has been deleted";

            string requestDate = GetDataValue(log.Data, "request_date");

            if (!string.IsNullOrWhiteSpace(requestDate))
            {
                text += $", following the deletion request sent on {requestDate}";
            }

            return text;
        }

        public string BuildAccountEmailAddressChangeLogText(PersonalLog log)
        {
            string verb = "changed";

            if (!log.Data.ContainsKey("old_email_address") && log.Data.ContainsKey("new_email_address"))
            {
                verb = "set";
            }

            string text = $"I have {verb} the e-mail address of the {GetPlatform(log.Data)} account";

            string oldEmailAddress = GetDataValue(log.Data, "old_email_address");

            if (!string.IsNullOrWhiteSpace(oldEmailAddress))
            {
                text += $" from {oldEmailAddress}";
            }

            string newEmailAddress = GetDataValue(log.Data, "new_email_address");

            if (!string.IsNullOrWhiteSpace(newEmailAddress))
            {
                text += $" to {newEmailAddress}";
            }

            return text;
        }

        public string BuildAccountEmailAddressChangeRequestLogText(PersonalLog log)
        {
            string text = $"I have sent a request to change the e-mail address of the {GetPlatform(log.Data)} account";

            string oldEmailAddress = GetDataValue(log.Data, "old_email_address");

            if (!string.IsNullOrWhiteSpace(oldEmailAddress))
            {
                text += $" from {oldEmailAddress}";
            }

            string newEmailAddress = GetDataValue(log.Data, "new_email_address");

            if (!string.IsNullOrWhiteSpace(newEmailAddress))
            {
                text += $" to {newEmailAddress}";
            }

            string requestId = GetDataValue(log.Data, "request_id");

            if (!string.IsNullOrWhiteSpace(requestId))
            {
                text += $", with the {requestId} identification code";
            }

            string requestMethod = GetMappedDataValue(
                log.Data,
                "request_method",
                new()
                {
                    { "AccountSettings", "account settings" },
                    { "ContactForm", "the contact form" },
                    { "EMail", "e-mail" },
                    { "SupportTicket", "support ticket" }
                });

            if (!string.IsNullOrWhiteSpace(requestMethod))
            {
                text += $", via {requestMethod}";
            }

            return text;
        }

        public string BuildAccountEmailAddressChangeRequestFulfillmentLogText(PersonalLog log)
        {
            string text = $"My request to change the e-mail address of the {GetPlatform(log.Data)} account";

            string oldEmailAddress = GetDataValue(log.Data, "old_email_address");

            if (!string.IsNullOrWhiteSpace(oldEmailAddress))
            {
                text += $" from {oldEmailAddress}";
            }

            string newEmailAddress = GetDataValue(log.Data, "new_email_address");

            if (!string.IsNullOrWhiteSpace(newEmailAddress))
            {
                text += $" to {newEmailAddress}";
            }

            string requestId = GetDataValue(log.Data, "request_id");

            if (!string.IsNullOrWhiteSpace(requestId))
            {
                text += $", with the {requestId} identification code";
            }

            string requestDate = GetDataValue(log.Data, "request_date");

            if (!string.IsNullOrWhiteSpace(requestDate))
            {
                text += $", sent on {requestDate},";
            }

            text += " has been fulfilled";

            return text;
        }

        public string BuildAccountEmailAddressConfirmationLogText(PersonalLog log)
        {
            string text = $"I have confirmed the e-mail address";

            string emailAddress = GetDataValue(log.Data, "email_address");

            if (!string.IsNullOrWhiteSpace(emailAddress))
            {
                text += $" ({emailAddress})";
            }

            return text + $" for the {GetPlatform(log.Data)} account";
        }

        public string BuildAccountFeatureEnablementLogText(PersonalLog log)
            => $"I have enabled the {log.Data["feature_name"]} feature for the {GetPlatform(log.Data)} account";

        public string BuildAccountFeatureDisablementLogText(PersonalLog log)
            => $"I have disabled the {log.Data["feature_name"]} feature for the {GetPlatform(log.Data)} account";

        public string BuildAccountFriendshipRequestReceivalLogText(PersonalLog log)
        {
            string text = $"I have received a friendship request on the {GetPlatform(log.Data)} account";

            string fromAccount = GetDataValue(log.Data, "from_account");

            if (!string.IsNullOrWhiteSpace(fromAccount))
            {
                text += $" from {fromAccount}";
            }

            return text;
        }

        public string BuildAccountIdentityVerificationLogText(PersonalLog log)
            => $"I have verified my identity for the {GetPlatform(log.Data)} account";

        public string BuildAccountLinkingLogText(PersonalLog log)
        {
            string text = $"I have linked the {GetPlatform(log.Data)} account with";

            string platformLinked = GetDataValue(log.Data, "platform_linked");

            if (!string.IsNullOrWhiteSpace(platformLinked))
            {
                string accountLinked = GetDataValue(log.Data, "account_linked");

                if (!string.IsNullOrWhiteSpace(accountLinked))
                {
                    text += $" the {platformLinked} account ({accountLinked})";
                }
                else
                {
                    text += $" {platformLinked}";
                }
            }
            else
            {
                text += $" {log.Data["account_linked"]}";
            }

            return text;
        }

        public string BuildAccountLoginLogText(PersonalLog log)
        {
            string text = $"I have logged in to the {GetPlatform(log.Data)} account";

            string ipAddress = GetDataValue(log.Data, "ip_address");

            if (!string.IsNullOrWhiteSpace(ipAddress))
            {
                text += $" from the {ipAddress} IP address";
            }

            return text;
        }

        public string BuildAccountMessagesErasureLogText(PersonalLog log)
            => $"I have erased all messages from the {GetPlatform(log.Data)} account";

        public string BuildAccountPasswordChangeLogText(PersonalLog log)
            => $"I have changed the password of the {GetPlatform(log.Data)} account";

        public string BuildAccountPersonalNameChangeLogText(PersonalLog log)
        {
            string text = $"I have changed the personal name for the {GetPlatform(log.Data)} account";

            string oldPersonalName = GetDataValue(log.Data, "old_personal_name");

            if (!string.IsNullOrWhiteSpace(oldPersonalName))
            {
                text += $" from {oldPersonalName}";
            }

            string newPersonalName = GetDataValue(log.Data, "new_personal_name");

            if (!string.IsNullOrWhiteSpace(newPersonalName))
            {
                text += $" to {newPersonalName}";
            }

            return text;
        }

        public string BuildAccountPhoneNumberAdditionLogText(PersonalLog log)
        {
            string text = $"I have added a phone number to the {GetPlatform(log.Data)} account";

            string newPhoneNumber = GetDataValue(log.Data, "phone_number");

            if (!string.IsNullOrWhiteSpace(newPhoneNumber))
            {
                text += $": {newPhoneNumber}";
            }

            return text;
        }

        public string BuildAccountPhoneNumberChangeLogText(PersonalLog log)
        {
            string verb = "changed";

            if (!log.Data.ContainsKey("old_phone_number") && log.Data.ContainsKey("new_phone_number"))
            {
                verb = "set";
            }

            string text = $"I have {verb} the phone number for the {GetPlatform(log.Data)} account";

            string oldPhoneNumber = GetDataValue(log.Data, "old_phone_number");

            if (!string.IsNullOrWhiteSpace(oldPhoneNumber))
            {
                text += $" from {oldPhoneNumber}";
            }

            string newPhoneNumber = GetDataValue(log.Data, "new_phone_number");

            if (!string.IsNullOrWhiteSpace(newPhoneNumber))
            {
                text += $" to {newPhoneNumber}";
            }

            return text;
        }

        public string BuildAccountPhoneNumberConfirmationLogText(PersonalLog log)
        {
            string text = $"I have confirmed the phone number";

            string phoneNumber = GetDataValue(log.Data, "phone_number");

            if (!string.IsNullOrWhiteSpace(phoneNumber))
            {
                text += $" ({phoneNumber})";
            }

            return text + $" for the {GetPlatform(log.Data)} account";
        }

        public string BuildAccountPhoneNumberRemovalLogText(PersonalLog log)
        {
            string text = $"I have removed a phone number from the {GetPlatform(log.Data)} account";

            string newPhoneNumber = GetDataValue(log.Data, "phone_number");

            if (!string.IsNullOrWhiteSpace(newPhoneNumber))
            {
                text += $": {newPhoneNumber}";
            }

            return text;
        }

        public string BuildAccountProfilePictureChangeLogText(PersonalLog log)
            => $"I have changed the profile picture of the {GetPlatform(log.Data)} account";

        public string BuildAccountRecoveryLogText(PersonalLog log)
            => $"I have recovered the {GetPlatform(log.Data)} account";

        public string BuildAccountRecoveryEmailAddressChangeLogText(PersonalLog log)
        {
            string verb = "changed";

            if (!log.Data.ContainsKey("old_email_address") && log.Data.ContainsKey("new_email_address"))
            {
                verb = "set";
            }

            string text = $"I have {verb} the recovery e-mail address of the {GetPlatform(log.Data)} account";

            string oldEmailAddress = GetDataValue(log.Data, "old_recovery_email_address");

            if (!string.IsNullOrWhiteSpace(oldEmailAddress))
            {
                text += $" from {oldEmailAddress}";
            }

            string newEmailAddress = GetDataValue(log.Data, "new_recovery_email_address");

            if (!string.IsNullOrWhiteSpace(newEmailAddress))
            {
                text += $" to {newEmailAddress}";
            }

            return text;
        }

        public string BuildAccountRecoveryRequestLogText(PersonalLog log)
        {
            string text = $"I have sent a recovery request for the {GetPlatform(log.Data)} account";

            string requestId = GetDataValue(log.Data, "request_id");

            if (!string.IsNullOrWhiteSpace(requestId))
            {
                text += $" with the {requestId} identification code";
            }

            string requestMethod = GetMappedDataValue(
                log.Data,
                "request_method",
                new()
                {
                    { "AccountSettings", "account settings" },
                    { "ContactForm", "the contact form" },
                    { "EMail", "e-mail" },
                    { "SupportTicket", "support ticket" }
                });

            if (!string.IsNullOrWhiteSpace(requestMethod))
            {
                text += $", via {requestMethod}";
            }

            return text;
        }

        public string BuildAccountRecoveryRequestFulfillmentLogText(PersonalLog log)
        {
            string text = $"My account recovery request for the {GetPlatform(log.Data)} account";

            string requestId = GetDataValue(log.Data, "request_id");

            if (!string.IsNullOrWhiteSpace(requestId))
            {
                text += $" with the {requestId} identification code";
            }

            string requestDate = GetDataValue(log.Data, "request_date");

            if (!string.IsNullOrWhiteSpace(requestDate))
            {
                text += $", sent on {requestDate},";
            }

            return $"{text} has been fulfilled";
        }

        public string BuildAccountRegistrationLogText(PersonalLog log)
        {
            string text = $"I have registered the {GetPlatform(log.Data)} account";
            int withCount = 0;

            string username = GetDataValue(log.Data, "username");

            if (!string.IsNullOrWhiteSpace(username))
            {
                text += $" with the username {username}";
                withCount += 1;
            }

            string phoneNumber = GetDataValue(log.Data, "phone_number");

            if (!string.IsNullOrWhiteSpace(phoneNumber))
            {
                if (withCount > 0)
                {
                    text += ", and";
                }

                text += $" with the phone number {phoneNumber}";
                withCount += 1;
            }

            string emailAddress = GetDataValue(log.Data, "email_address");

            if (!string.IsNullOrWhiteSpace(emailAddress))
            {
                if (withCount > 0)
                {
                    text += ", and";
                }

                text += $" with the email address {emailAddress}";
                withCount += 1;
            }

            string personalName = GetDataValue(log.Data, "personal_name");

            if (!string.IsNullOrWhiteSpace(personalName))
            {
                if (withCount > 0)
                {
                    text += ", and";
                }

                text += $" with the personal name {personalName}";
                withCount += 1;
            }

            return text;
        }

        public string BuildAccountRegistrationRequestLogText(PersonalLog log)
        {
            string text = $"I have sent a request";

            string requestId = GetDataValue(log.Data, "request_id");

            if (!string.IsNullOrWhiteSpace(requestId))
            {
                text += $" with the {requestId} identification code";
            }

            text += $" to register the {GetPlatform(log.Data)} account";
            int withCount = 0;

            string username = GetDataValue(log.Data, "username");

            if (!string.IsNullOrWhiteSpace(username))
            {
                text += $" with the username {username}";
                withCount += 1;
            }

            string phoneNumber = GetDataValue(log.Data, "phone_number");

            if (!string.IsNullOrWhiteSpace(phoneNumber))
            {
                if (withCount > 0)
                {
                    text += ", and";
                }

                text += $" with the phone number {phoneNumber}";
                withCount += 1;
            }

            string emailAddress = GetDataValue(log.Data, "email_address");

            if (!string.IsNullOrWhiteSpace(emailAddress))
            {
                if (withCount > 0)
                {
                    text += ", and";
                }

                text += $" with the email address {emailAddress}";
                withCount += 1;
            }

            string personalName = GetDataValue(log.Data, "personal_name");

            if (!string.IsNullOrWhiteSpace(personalName))
            {
                if (withCount > 0)
                {
                    text += ", and";
                }

                text += $" with the personal name {personalName}";
                withCount += 1;
            }

            return text; ;
        }

        public string BuildAccountRegistrationRequestFulfillmentLogText(PersonalLog log)
        {
            string text = $"The account registration request for the {GetPlatform(log.Data)} account";

            string requestId = GetDataValue(log.Data, "request_id");

            if (!string.IsNullOrWhiteSpace(requestId))
            {
                text += $" with the {requestId} identification code";
            }

            string requestDate = GetDataValue(log.Data, "request_date");

            if (!string.IsNullOrWhiteSpace(requestDate))
            {
                text += $", sent on {requestDate},";
            }

            text += " has been fulfilled";

            return text;
        }

        public string BuildAccountSecurityQuestionsChangeLogText(PersonalLog log)
            => $"I have changed the security questions for the {GetPlatform(log.Data)} account";

        public string BuildAccountSubscriptionPurchaseLogText(PersonalLog log)
        {
            string text = $"I have purchased a";

            string subscriptionType = GetDataValue(log.Data, "subscription_name");

            if (!string.IsNullOrWhiteSpace(subscriptionType))
            {
                text += $" {subscriptionType}";
            }

            text += $" subscription for the {GetPlatform(log.Data)} account";

            if (log.Data.ContainsKey("price_amount"))
            {
                text += $" for {GetBalance(log.Data)}";
            }

            return text;
        }

        public string BuildAccountUnlinkingLogText(PersonalLog log)
        {
            string text = $"I have removed the link between the {GetPlatform(log.Data)} account";

            text += $" and the {log.Data["platform_unlinked"]} account";

            string accountLinked = GetDataValue(log.Data, "account_unlinked");

            if (!string.IsNullOrWhiteSpace(accountLinked))
            {
                text += $" ({accountLinked})";
            }

            return text;
        }

        public string BuildAccountUsernameChangeLogText(PersonalLog log)
        {
            string verb = "changed";

            if (!log.Data.ContainsKey("old_username"))
            {
                verb = "set";
            }

            string text = $"I have {verb} the username of the {GetPlatform(log.Data)} account";

            string oldUsername = GetDataValue(log.Data, "old_username");

            if (!string.IsNullOrWhiteSpace(oldUsername))
            {
                text += $" from {oldUsername}";
            }

            string newUsername = GetDataValue(log.Data, "new_username");

            if (!string.IsNullOrWhiteSpace(newUsername))
            {
                text += $" to {newUsername}";
            }

            return text;
        }

        public string BuildAccountUsernameChangeRequestLogText(PersonalLog log)
        {
            string text = $"I have sent a request to change the username of the {GetPlatform(log.Data)} account";

            string oldUsername = GetDataValue(log.Data, "old_username");

            if (!string.IsNullOrWhiteSpace(oldUsername))
            {
                text += $" from {oldUsername}";
            }

            string newUsername = GetDataValue(log.Data, "new_username");

            if (!string.IsNullOrWhiteSpace(newUsername))
            {
                text += $" to {newUsername}";
            }

            string requestId = GetDataValue(log.Data, "request_id");

            if (!string.IsNullOrWhiteSpace(requestId))
            {
                text += $", with the {requestId} identification code";
            }

            string requestMethod = GetMappedDataValue(
                log.Data,
                "request_method",
                new()
                {
                    { "AccountSettings", "account settings" },
                    { "ContactForm", "the contact form" },
                    { "EMail", "e-mail" },
                    { "SupportTicket", "support ticket" }
                });

            if (!string.IsNullOrWhiteSpace(requestMethod))
            {
                text += $", via {requestMethod}";
            }

            return text;
        }

        public string BuildAccountUsernameChangeRequestFulfillmentLogText(PersonalLog log)
        {
            string text = $"My request to change the username of the {GetPlatform(log.Data)} account";

            string oldUsername = GetDataValue(log.Data, "old_username");

            if (!string.IsNullOrWhiteSpace(oldUsername))
            {
                text += $" from {oldUsername}";
            }

            string newUsername = GetDataValue(log.Data, "new_username");

            if (!string.IsNullOrWhiteSpace(newUsername))
            {
                text += $" to {newUsername}";
            }

            string requestId = GetDataValue(log.Data, "request_id");

            if (!string.IsNullOrWhiteSpace(requestId))
            {
                text += $", with the {requestId} identification code";
            }

            string requestDate = GetDataValue(log.Data, "request_date");

            if (!string.IsNullOrWhiteSpace(requestDate))
            {
                text += $", sent on {requestDate},";
            }

            return $"{text} has been fulfilled";
        }

        public string BuildAccountVisibilityMadePrivateLogText(PersonalLog log)
            => $"I have made my {GetPlatform(log.Data)} account private";

        public string BuildAccountVisibilityMadePublicLogText(PersonalLog log)
            => $"I have made my {GetPlatform(log.Data)} account public";

        public string BuildAlkalinePhosphataseMeasurementLogText(PersonalLog log)
            => $"My alkaline phosphatase level measured {GetDecimalValue(log.Data, "alkaline_phosphatase_level")} {GetDataValue(log.Data, "unit", "U/L")}" +
                GetLocation(log.Data);

        public string BuildApplicationInstallationLogText(PersonalLog log)
            => $"I have installed the application {GetDataValue(log.Data, "application_name")} on {GetDevice(log.Data)}";

        public string BuildApplicationUninstallationLogText(PersonalLog log)
            => $"I have uninstalled the application {GetDataValue(log.Data, "application_name")} from {GetDevice(log.Data)}";

        public string BuildBedLinenChangingLogText(PersonalLog log)
            => "I have changed the bed linen" + GetLocation(log.Data);

        public string BuildBedMakingLogText(PersonalLog log)
         => "I have made the bed" + GetLocation(log.Data);

        public string BuildBloodDonationLogText(PersonalLog log)
        {
            string text = $"I have donated blood" + GetLocation(log.Data);

            string donationCode = GetDataValue(log.Data, "donation_code");

            if (!string.IsNullOrWhiteSpace(donationCode))
            {
                text += $". The donation code was: {donationCode}";
            }

            return text;
        }

        public string BuildBloodGlucoseMeasurementLogText(PersonalLog log)
        {
            string text =
                "My blood glucose level measured" +
                $" {log.Data["glucose_level"]} {GetDataValue(log.Data, "unit", "mg/dL")}" +
                GetLocation(log.Data);

            string device = GetDevice(log.Data);

            if (!string.IsNullOrWhiteSpace(device) &&
                !string.Equals(device, MissingValue))
            {
                text += $", using the {device}";
            }

            return text;
        }

        public string BuildBloodPressureMeasurementLogText(PersonalLog log)
        {
            string text =
                $"My blood pressure measured {log.Data["systolic_pressure"]}/{log.Data["diastolic_pressure"]} {GetDataValue(log.Data, "unit", "mmHg")}" +
                GetLocation(log.Data);

            string device = GetDevice(log.Data);

            if (!string.IsNullOrWhiteSpace(device) &&
                !string.Equals(device, MissingValue))
            {
                text += $", using the {device}";
            }

            return text;
        }

        public string BuildBodyWaterRateMeasurementLogText(PersonalLog log)
        {
            string text =
                $"My body water rate measured {GetDecimalValue(log.Data, "body_water_rate")}%" +
                GetLocation(log.Data);

            string device = GetDevice(log.Data);

            if (!string.IsNullOrWhiteSpace(device) &&
                !string.Equals(device, MissingValue))
            {
                text += $", using the {device}";
            }

            return text;
        }

        public string BuildBodyWeightMeasurementLogText(PersonalLog log)
        {
            string text =
                $"My body weight measured {GetDataValue(log.Data, "body_weight")} {GetDataValue(log.Data, "unit", "kg")}" +
                GetLocation(log.Data);

            string device = GetDevice(log.Data);

            if (!string.IsNullOrWhiteSpace(device) &&
                !string.Equals(device, MissingValue))
            {
                text += $", using the {device}";
            }

            return text;
        }

        public string BuildBookBeginningLogText(PersonalLog log)
        {
            string verb = "reading";
            string bookType = GetMappedDataValue(
                log.Data,
                "book_type",
                new()
                {
                    { "Book", "book" },
                    { "ComicBook", "comic book" },
                    { "Audiobook", "audiobook" },
                },
                "book");

            if (bookType.Equals("audiobook"))
            {
                verb = "listening to";
            }

            string text = $"I have started {verb} the {bookType} '{log.Data["book_title"]}'";

            string bookSeriesName = GetDataValue(log.Data, "book_series_name");

            if (!string.IsNullOrWhiteSpace(bookSeriesName))
            {
                text += $" of the '{bookSeriesName}' series";
            }

            return text;
        }

        public string BuildBookChapterBeginningLogText(PersonalLog log)
        {
            string verb = "reading";
            string bookType = GetMappedDataValue(
                log.Data,
                "book_type",
                new()
                {
                    { "Book", "book" },
                    { "ComicBook", "comic book" },
                    { "Audiobook", "audiobook" },
                },
                "book");

            if (bookType.Equals("audiobook"))
            {
                verb = "listening to";
            }

            string text = $"I have started {verb} chapter {log.Data["chapter_number"]} of the {bookType} '{log.Data["book_title"]}'";

            string bookSeriesName = GetDataValue(log.Data, "book_series_name");

            if (!string.IsNullOrWhiteSpace(bookSeriesName))
            {
                text += $" of the '{bookSeriesName}' series";
            }

            return text;
        }

        public string BuildBookChapterCompletionLogText(PersonalLog log)
        {
            string verb = "reading";
            string bookType = GetMappedDataValue(
                log.Data,
                "book_type",
                new()
                {
                    { "Book", "book" },
                    { "ComicBook", "comic book" },
                    { "Audiobook", "audiobook" },
                },
                "book");

            if (bookType.Equals("audiobook"))
            {
                verb = "listening to";
            }

            string text = $"I have finished {verb} chapter {log.Data["chapter_number"]} of the {bookType} '{log.Data["book_title"]}'";

            string bookSeriesName = GetDataValue(log.Data, "book_series_name");

            if (!string.IsNullOrWhiteSpace(bookSeriesName))
            {
                text += $" of the '{bookSeriesName}' series";
            }

            return text;
        }

        public string BuildBookCompletionLogText(PersonalLog log)
        {
            string verb = "reading";
            string bookType = GetMappedDataValue(
                log.Data,
                "book_type",
                new()
                {
                    { "Book", "book" },
                    { "ComicBook", "comic book" },
                    { "Audiobook", "audiobook" },
                },
                "book");

            if (bookType.Equals("audiobook"))
            {
                verb = "listening to";
            }

            string text = $"I have finished {verb} the {bookType} '{log.Data["book_title"]}'";

            string bookSeriesName = GetDataValue(log.Data, "book_series_name");

            if (!string.IsNullOrWhiteSpace(bookSeriesName))
            {
                text += $" of the '{bookSeriesName}' series";
            }

            return text;
        }

        public string BuildBookResumingLogText(PersonalLog log)
        {
            string verb = "reading";
            string bookType = GetMappedDataValue(
                log.Data,
                "book_type",
                new()
                {
                    { "Book", "book" },
                    { "ComicBook", "comic book" },
                    { "Audiobook", "audiobook" },
                },
                "book");

            if (bookType.Equals("audiobook"))
            {
                verb = "listening to";
            }

            string text = $"I have resumed {verb} the {bookType} '{log.Data["book_title"]}'";

            string bookSeriesName = GetDataValue(log.Data, "book_series_name");

            if (!string.IsNullOrWhiteSpace(bookSeriesName))
            {
                text += $" of the '{bookSeriesName}' series";
            }

            return text;
        }

        public string BuildBookStoppingLogText(PersonalLog log)
        {
            string verb = "reading";
            string bookType = GetMappedDataValue(
                log.Data,
                "book_type",
                new()
                {
                    { "Book", "book" },
                    { "ComicBook", "comic book" },
                    { "Audiobook", "audiobook" },
                },
                "book");

            if (bookType.Equals("audiobook"))
            {
                verb = "listening to";
            }

            string text = $"I have stopped {verb} the {bookType} '{log.Data["book_title"]}'";

            string bookSeriesName = GetDataValue(log.Data, "book_series_name");

            if (!string.IsNullOrWhiteSpace(bookSeriesName))
            {
                text += $" of the '{bookSeriesName}' series";
            }

            return text;
        }

        public string BuildBotPrizeWinningLogText(PersonalLog log)
        {
            string text = $"I have won a prize with my";

            string platform = GetPlatform(log.Data);

            if (!string.IsNullOrWhiteSpace(platform))
            {
                text += $" {platform}";
            }

            text += " bot";

            string prizeDescription = GetDataValue(log.Data, "prize_description");

            if (!string.IsNullOrWhiteSpace(prizeDescription))
            {
                text += $": {prizeDescription}";
            }

            return text;
        }

        public string BuildBotsTotalBalanceMeasurementLogText(PersonalLog log)
        {
            string text = $"The total balance of the";

            string platform = GetPlatform(log.Data);

            if (!string.IsNullOrWhiteSpace(platform))
            {
                text += $" {platform}";
            }

            return $"{text} bots was measured at {GetBalance(log.Data)}";
        }

        public string BuildCalciumLevelMeasurementLogText(PersonalLog log)
        {
            string text =
                $"My calcium level measured {GetDecimalValue(log.Data, "calcium_level")} {GetDataValue(log.Data, "unit", "mg/dL")}" +
                GetLocation(log.Data);

            string device = GetDevice(log.Data);

            if (!string.IsNullOrWhiteSpace(device) &&
                !string.Equals(device, MissingValue))
            {
                text += $", using the {device}";
            }

            return text;
        }

        public string BuildCertificationObtainmentLogText(PersonalLog log)
        {
            string text = $"I have obtained the {log.Data["certification_name"]} certification";

            string certificationAuthority = GetDataValue(log.Data, "certification_authority");

            if (!string.IsNullOrWhiteSpace(certificationAuthority))
            {
                text += $" from {certificationAuthority}";
            }

            return text;
        }

        public string BuildChatGroupCreationLogText(PersonalLog log)
        {
            string text = $"I have created a chat group named {log.Data["group_name"]}";

            string platform = GetPlatform(log.Data);

            if (!string.IsNullOrWhiteSpace(platform))
            {
                text += $" {platform}";
            }

            return text;
        }

        public string BuildChatGroupDeletionLogText(PersonalLog log)
        {
            string text = $"I have deleted the chat group named {log.Data["group_name"]}";

            string platform = GetPlatform(log.Data);

            if (!string.IsNullOrWhiteSpace(platform))
            {
                text += $" {platform}";
            }

            return text;
        }

        public string BuildChatGroupJoiningLogText(PersonalLog log)
        {
            string text = $"I have joined the chat group named {log.Data["group_name"]}";

            string platform = GetPlatform(log.Data);

            if (!string.IsNullOrWhiteSpace(platform))
            {
                text += $" {platform}";
            }

            return text;
        }

        public string BuildChatGroupLeavingLogText(PersonalLog log)
        {
            string text = $"I have left the chat group named {log.Data["group_name"]}";

            string platform = GetPlatform(log.Data);

            if (!string.IsNullOrWhiteSpace(platform))
            {
                text += $" {platform}";
            }

            return text;
        }

        public string BuildCustomGptCreationLogText(PersonalLog log)
        {
            string text = $"I have created a custom GPT";

            string gptName = GetDataValue(log.Data, "gpt_name");

            if (!string.IsNullOrWhiteSpace(gptName))
            {
                text += $" named '{gptName}'";
            }

            string platform = GetPlatform(log.Data);

            if (!string.IsNullOrWhiteSpace(platform))
            {
                text += $" {platform}";
            }

            return text;
        }

        public string BuildDatingAppMatchLogText(PersonalLog log)
            => $"I have matched with {log.Data["match_name"]} on {GetPlatform(log.Data)}";

        public string BuildDeliveryReceivalLogText(PersonalLog log)
        {
            string text = $"I have received the delivery of {log.Data["package_description"]}";

            string trackingNumber = GetDataValue(log.Data, "tracking_number");

            if (!string.IsNullOrWhiteSpace(trackingNumber))
            {
                text += $" with the tracking number {trackingNumber}";
            }

            string companyName = GetDataValue(log.Data, "company_name");

            if (!string.IsNullOrWhiteSpace(companyName))
            {
                text += $" via {companyName}";
            }

            return text;
        }

        public string BuildDentalAppointmentLogText(PersonalLog log)
        {
            string text = $"I have attended a dental appointment" + GetLocation(log.Data);

            string dentistName = GetByPerson(log.Data);

            if (!string.IsNullOrWhiteSpace(dentistName) &&
                !string.Equals(dentistName, MissingValue))
            {
                text += $", by {dentistName}";
            }

            return text;
        }

        public string BuildDentalScalingLogText(PersonalLog log)
        {
            string text = $"I have undergone a dental scaling procedure" + GetLocation(log.Data);

            string dentistName = GetByPerson(log.Data);

            if (!string.IsNullOrWhiteSpace(dentistName) &&
                !string.Equals(dentistName, MissingValue))
            {
                text += $", by {dentistName}";
            }

            return text;
        }

        public string BuildDeviceBatteryHealthLogText(PersonalLog log)
            => $"The battery health of my {GetDevice(log.Data)} was measured at {log.Data["battery_health_percentage"]}%";

        public string BuildDeviceBatteryLevelLogText(PersonalLog log)
            => $"The battery level of my {GetDevice(log.Data)} was measured at {log.Data["battery_level_percentage"]}%";

        public string BuildDeviceBreakingLogText(PersonalLog log)
        {
            string text;

            string deviceOwnerName = GetDataValue(log.Data, "device_owner_name");

            if (!string.IsNullOrWhiteSpace(deviceOwnerName))
            {
                text = $"{deviceOwnerName}'s";
            }
            else
            {
                text = "My";
            }

            text += $" {log.Data["device_name"]}";

            string deviceType = GetDeviceType(log.Data);

            text += $" {deviceType}" + GetLocation(log.Data);

            if (deviceType.EndsWith('s'))
            {
                text += " have";
            }
            else
            {
                text += " has";
            }

            return text + " broken";
        }

        public string BuildDeviceChargingLogText(PersonalLog log)
            => $"I have charged my {GetDevice(log.Data)}" +
                GetLocation(log.Data);

        public string BuildDeviceContainerEmptyingLogText(PersonalLog log)
            => $"I have emptied the container of my {GetDevice(log.Data)}" +
                GetLocation(log.Data);

        public string BuildDeviceExternalCleaningLogText(PersonalLog log)
        {
            string text = $"I have cleaned the exterior of my {GetDevice(log.Data)}";

            string cleaningMethod = GetCleaningMethod(log.Data);

            if (!string.IsNullOrWhiteSpace(cleaningMethod) &&
                !string.Equals(cleaningMethod, MissingValue))
            {
                text += $" by {cleaningMethod}";
            }

            return text + GetLocation(log.Data);
        }

        public string BuildDeviceInternalCleaningLogText(PersonalLog log)
        {
            string text = $"I have cleaned the interior of my {GetDevice(log.Data)}";

            string cleaningMethod = GetCleaningMethod(log.Data);

            if (!string.IsNullOrWhiteSpace(cleaningMethod) &&
                !string.Equals(cleaningMethod, MissingValue))
            {
                text += $" by {cleaningMethod}";
            }

            return text + GetLocation(log.Data);
        }

        public string BuildDeviceRepairLogText(PersonalLog log)
        {
            string text = "I have repaired";

            string deviceOwnerName = GetDataValue(log.Data, "device_owner_name");

            if (!string.IsNullOrWhiteSpace(deviceOwnerName))
            {
                text += $" {deviceOwnerName}'s";
            }
            else
            {
                text += " my";
            }

            return $"{text} {GetDevice(log.Data)}" + GetLocation(log.Data);
        }

        public string BuildDeviceScreentimeMeasurementLogText(PersonalLog log)
        {
            string text = $"Today's screentime on my {GetDevice(log.Data)} was measured at";

            string screentimeHours = GetDataValue(log.Data, "screentime_hours");

            if (!string.IsNullOrWhiteSpace(screentimeHours))
            {
                text += $" {screentimeHours} hour";

                if (screentimeHours != "1")
                {
                    text += "s";
                }
            }

            string screentimeMinutes = GetDataValue(log.Data, "screentime_minutes");

            if (!string.IsNullOrWhiteSpace(screentimeMinutes))
            {
                if (log.Data.ContainsKey("screentime_hours"))
                {
                    text += $" and";
                }

                text += $" {screentimeMinutes} minute";

                if (!screentimeMinutes.Equals("1"))
                {
                    text += "s";
                }
            }

            return text;
        }

        public string BuildDirectBilirubinMeasurementLogText(PersonalLog log)
            => $"My direct bilirubin level measured {log.Data["direct_bilirubin_level"]} {GetDataValue(log.Data, "unit", "mg/dL")}";

        public string BuildDonationLogText(PersonalLog log)
            => $"I have donated {GetBalance(log.Data)} to {GetDataValue(log.Data, "recipient")}";

        public string BuildEarwaxCleaningLogText(PersonalLog log)
        {
            string text = "I have cleaned my earwax";

            string cleaningMethod = GetCleaningMethod(log.Data);

            if (!string.IsNullOrWhiteSpace(cleaningMethod) &&
                !string.Equals(cleaningMethod, MissingValue))
            {
                text += $" by {cleaningMethod}";
            }

            return text;
        }

        public string BuildEducationalGradeReceivalLogText(PersonalLog log)
        {
            string gradeType = GetMappedDataValue(
                log.Data,
                "grade_type",
                new()
                {
                    { "AverageGrade", "grade average" },
                    { "Grade", "grade" },
                    { "TestGrade", "test grade" },
                    { "ThesisGrade", "thesis grade" },
                    { "Qualifier", "qualifier" }
                },
                "grade");

            string text = $"I have obtained the {gradeType} {log.Data["grade_value"]} in the subject '{log.Data["subject_name"]}'";

            string subjectCode = GetDataValue(log.Data, "subject_code");

            if (!string.IsNullOrWhiteSpace(subjectCode))
            {
                text += $" ({subjectCode})";
            }

            string courseName = GetDataValue(log.Data, "course_name");

            if (!string.IsNullOrWhiteSpace(courseName))
            {
                text += $", in the course {courseName}";
            }

            string institutionName = GetDataValue(log.Data, "institution_name");

            if (!string.IsNullOrWhiteSpace(institutionName))
            {
                text += $", at";

                string institutionDepartment = GetDataValue(log.Data, "institution_department");

                if (!string.IsNullOrWhiteSpace(institutionDepartment))
                {
                    text += $" {institutionDepartment}";

                    string institutionDepartmentSpecialisation = GetDataValue(log.Data, "institution_department_specialisation");

                    if (!string.IsNullOrWhiteSpace(institutionDepartmentSpecialisation))
                    {
                        text += $", {institutionDepartmentSpecialisation} specialisation,";
                    }

                    text += $" at";
                }

                text += $" {institutionName}";
            }

            string educationalCycleYear = GetDataValue(log.Data, "educational_cycle_year");

            if (!string.IsNullOrWhiteSpace(educationalCycleYear))
            {
                text += $", in year {educationalCycleYear}";
            }
            else
            {
                string educationalCycleGrade = GetDataValue(log.Data, "educational_cycle_grade");

                if (!string.IsNullOrWhiteSpace(educationalCycleGrade))
                {
                    text += $", in grade {educationalCycleGrade}";
                }
            }

            string educationalCycleSemester = GetDataValue(log.Data, "educational_cycle_semester");

            if (!string.IsNullOrWhiteSpace(educationalCycleSemester))
            {
                text += $", in semester {educationalCycleSemester}";
            }

            return text;
        }

        public string BuildEmailExportLogText(PersonalLog log)
            => $"I have exported all of my emails on {GetPlatform(log.Data)}";

        public string BuildEmailAliasCreationLogText(PersonalLog log)
            => $"I have created the '{log.Data["email_alias"]}' e-mail alias on {GetPlatform(log.Data)}";

        public string BuildEmailAliasDeletionLogText(PersonalLog log)
            => $"I have deleted the '{log.Data["email_alias"]}' e-mail alias from {GetPlatform(log.Data)}";

        public string BuildEventTicketPurchaseLogText(PersonalLog log)
        {
            string text = $"I have purchased a";

            string ticketType = GetDataValue(log.Data, "ticket_type");

            if (!string.IsNullOrWhiteSpace(ticketType))
            {
                text += $" {ticketType}";
            }

            text += $"ticket for '{log.Data["event_name"]}'";

            string eventDate = GetDataValue(log.Data, "event_date");

            if (!string.IsNullOrWhiteSpace(eventDate))
            {
                text += $" on {eventDate}";
            }

            return text + GetLocation(log.Data);
        }

        public string BuildEyeCheckupLogText(PersonalLog log)
        {
            string text = $"I have undergone an eye checkup" + GetLocation(log.Data);

            string therapistName = GetByPerson(log.Data);

            if (!string.IsNullOrWhiteSpace(therapistName) &&
                !string.Equals(therapistName, MissingValue))
            {
                text += $", with {therapistName}";
            }

            return text;
        }

        public string BuildFireDrillLogText(PersonalLog log)
            => $"I have participated in a fire drill" + GetLocation(log.Data);

        public string BuildFurnitureCleaningLogText(PersonalLog log)
        {
            string text = $"I have cleaned the {GetFurnitureType(log.Data)}";

            string cleaningMethod = GetCleaningMethod(log.Data);

            if (!string.IsNullOrWhiteSpace(cleaningMethod) &&
                !string.Equals(cleaningMethod, MissingValue))
            {
                text += $", by {cleaningMethod}";
            }

            return text + GetLocation(log.Data);
        }

        public string BuildGameAchievementUnlockLogText(PersonalLog log)
        {
            string achievementAction = "unlocked";
            string achievementType = "achievement";
            string game = log.Data["game_name"];
            string platform = GetDataValue(log.Data, "platform");

            if (game.Equals("eRepublik"))
            {
                achievementAction = "earned";
                achievementType = "medal";
            }
            else if (platform is not null)
            {
                if (platform.Equals("Xbox") || platform.Equals("PlayStation"))
                {
                    achievementAction = "earned";
                    achievementType = "trophy";
                }
            }

            string text = $"I have {achievementAction} the {achievementType} '{log.Data["achievement_name"]}' in the game {game}";

            if (!string.IsNullOrWhiteSpace(platform))
            {
                text += $" on {platform}";
            }

            string discriminator = GetDiscriminator(log.Data);

            if (!string.IsNullOrWhiteSpace(discriminator))
            {
                text += $" ({discriminator})";
            }

            return text;
        }

        public string BuildGameArticlePublishingLogText(PersonalLog log)
        {
            string gameName = GetDataValue(log.Data, "game_name");
            string text = $"I have published an article titled '{log.Data["article_title"]}' in the game {gameName}";

            if (string.Equals(gameName, "eRepublik"))
            {
                string newspaperName = GetDataValue(log.Data, "newspaper_name");

                if (!string.IsNullOrWhiteSpace(newspaperName))
                {
                    text += $" in the '{newspaperName}' newspaper";
                }
            }

            string platform = GetPlatform(log.Data);

            if (!string.IsNullOrWhiteSpace(platform))
            {
                text += $" on {platform}";
            }

            return text;
        }

        public string BuildGameBuildingBoughtLogText(PersonalLog log)
        {
            string text = $"I have bought {log.Data["building_name"]} in the game {log.Data["game_name"]}";

            string platform = GetPlatform(log.Data);

            if (!string.IsNullOrWhiteSpace(platform))
            {
                text += $" on {platform}";
            }

            return text;
        }

        public string BuildGameBuildingLevelUpgradeLogText(PersonalLog log)
        {
            string text = $"I have upgraded {log.Data["building_name"]} to level {log.Data["new_level"]} in the game {log.Data["game_name"]}";

            string platform = GetPlatform(log.Data);

            if (!string.IsNullOrWhiteSpace(platform))
            {
                text += $" on {platform}";
            }

            return text;
        }

        public string BuildGameConstructionLogText(PersonalLog log)
        {
            string text = $"I have built {log.Data["construction_name"]} in the game {log.Data["game_name"]}";

            string platform = GetPlatform(log.Data);

            if (!string.IsNullOrWhiteSpace(platform))
            {
                text += $" on {platform}";
            }

            return text;
        }

        public string BuildGameConstructionBeginningLogText(PersonalLog log)
        {
            string text = $"I have begun the construction of {log.Data["construction_name"]} in the game {log.Data["game_name"]}";

            string platform = GetPlatform(log.Data);

            if (!string.IsNullOrWhiteSpace(platform))
            {
                text += $" on {platform}";
            }

            return text;
        }

        public string BuildGameConstructionCompletionLogText(PersonalLog log)
        {
            string text = $"I have completed the construction of {log.Data["construction_name"]} in the game {log.Data["game_name"]}";

            string platform = GetPlatform(log.Data);

            if (!string.IsNullOrWhiteSpace(platform))
            {
                text += $" on {platform}";
            }

            return text;
        }

        public string BuildGameGuildJoiningLogText(PersonalLog log)
        {
            string guildType = GetMappedDataValue(
                log.Data,
                "guild_type",
                new Dictionary<string, string>
                {
                    { "Clan", "clan" },
                    { "MilitaryUnit", "military unit" },
                    { "PoliticalParty", "political party" }
                },
                "guild"
            );

            string text = $"I have joined the '{log.Data["guild_name"]}' {guildType} in the game {log.Data["game_name"]}";

            string platform = GetPlatform(log.Data);

            if (!string.IsNullOrWhiteSpace(platform))
            {
                text += $" on {platform}";
            }

            return text;
        }

        public string BuildGameGuildLeavingLogText(PersonalLog log)
        {
            string guildType = GetMappedDataValue(
                log.Data,
                "guild_type",
                new Dictionary<string, string>
                {
                    { "Clan", "clan" },
                    { "MilitaryUnit", "military unit" },
                    { "PolititicalParty", "political party" }
                },
                "guild"
            );

            string text = $"I have left the '{log.Data["party_name"]}' {guildType} in the game {log.Data["game_name"]}";

            string platform = GetPlatform(log.Data);

            if (!string.IsNullOrWhiteSpace(platform))
            {
                text += $" on {platform}";
            }

            return text;
        }

        public string BuildGameModPublishingLogText(PersonalLog log)
            => $"I have published the mod '{log.Data["mod_name"]}' for the game {log.Data["game_name"]} on {GetPlatform(log.Data)}";

        public string BuildGameOfficeTermBeginningLogText(PersonalLog log)
        {
            string text = $"I have begun my term as {log.Data["office_name"]}";

            string officeLocation = GetDataValue(log.Data, "office_location");

            if (!string.IsNullOrWhiteSpace(officeLocation))
            {
                text += $" in {officeLocation}";
            }

            string factionName = GetDataValue(log.Data, "faction_name");

            if (!string.IsNullOrWhiteSpace(factionName))
            {
                text += $" as part of {factionName},";
            }

            text += $" in {log.Data["game_name"]}";

            string platform = GetPlatform(log.Data);

            if (!string.IsNullOrWhiteSpace(platform))
            {
                text += $" on {platform}";
            }

            return text;
        }

        public string BuildGameOfficeTermEndingLogText(PersonalLog log)
        {
            string text = $"I have ended my term as {log.Data["office_name"]}";

            string officeLocation = GetDataValue(log.Data, "office_location");

            if (!string.IsNullOrWhiteSpace(officeLocation))
            {
                text += $" in {officeLocation}";
            }

            string factionName = GetDataValue(log.Data, "faction_name");

            if (!string.IsNullOrWhiteSpace(factionName))
            {
                text += $" as part of {factionName},";
            }

            text += $" in {log.Data["game_name"]}";

            string platform = GetPlatform(log.Data);

            if (!string.IsNullOrWhiteSpace(platform))
            {
                text += $" on {platform}";
            }

            return text;
        }

        public string BuildGameRankUpLogText(PersonalLog log)
        {
            string text = $"I have ranked up to {log.Data["new_rank"]} in {log.Data["game_name"]}";

            string platform = GetPlatform(log.Data);

            if (!string.IsNullOrWhiteSpace(platform))
            {
                text += $" on {platform}";
            }

            return text;
        }

        public string BuildGameStartedPlayingLogText(PersonalLog log)
        {
            string text = $"I have started playing {log.Data["game_name"]}";

            string platform = GetPlatform(log.Data);

            if (!string.IsNullOrWhiteSpace(platform))
            {
                text += $" on {platform}";
            }

            return text;
        }

        public string BuildGameLevelUpLogText(PersonalLog log)
        {
            string text = $"I have reached level {log.Data["new_level"]} in {log.Data["game_name"]}";

            string platform = GetPlatform(log.Data);

            if (!string.IsNullOrWhiteSpace(platform))
            {
                text += $" on {platform}";
            }

            return text;
        }

        public string BuildGarbageDisposalLogText(PersonalLog log)
            => $"I have taken out the garbage" + GetLocation(log.Data);

        public string BuildGettingInToBedLogText(PersonalLog log)
        {
            string text = "I have gotten in to bed" + GetLocation(log.Data);

            string side = GetSide(log.Data);

            if (!string.IsNullOrWhiteSpace(side) &&
                !string.Equals(side, MissingValue))
            {
                text += $", on the {side} side";
            }

            return text;
        }

        public string BuildGettingOutOfBedLogText(PersonalLog log)
        {
            string text = "I have gotten out of bed" + GetLocation(log.Data);

            string side = GetSide(log.Data);

            if (!string.IsNullOrWhiteSpace(side) &&
                !string.Equals(side, MissingValue))
            {
                text += $", on the {side} side";
            }

            return text;
        }

        public string BuildGiftReceivalLogText(PersonalLog log)
        {
            string text = $"I have received a";
            string giftOccasion = GetMappedDataValue(
                log.Data,
                "gift_occasion",
                new()
                {
                    { "Birthday", "birthday" },
                    { "Christmas", "Christmas" },
                    { "Easter", "Easter" },
                    { "NameDay", "name day" },
                    { "RelationshipAnniversary", "relationship anniversary" },
                    { "ValentinesDay", "Valentine's Day" }
                });

            if (!string.IsNullOrWhiteSpace(giftOccasion))
            {
                text += $" {giftOccasion}";
            }

            text += " gift";

            string giverName = GetByPerson(log.Data);

            if (!string.IsNullOrWhiteSpace(giverName) &&
                !string.Equals(giverName, MissingValue))
            {
                text += $" from {giverName}";
            }

            string giftContent = GetDataValue(log.Data, "gift_content");

            if (!string.IsNullOrWhiteSpace(giftContent))
            {
                text += $": {giftContent}";
            }

            return text;
        }

        public string BuildGitContributionsMeasurementLogText(PersonalLog log)
            => $"I have made {log.Data["contributions_count"]} contributions on {GetPlatform(log.Data)}";

        public string BuildGitReleaseLogText(PersonalLog log)
            => $"I have released version {GetDataValue(log.Data, "version")} of the `{GetDataValue(log.Data, "repository")}` repository on {GetPlatform(log.Data)}";

        public string BuildGitRepositoryArchivalLogText(PersonalLog log)
            => $"I have archived the `{GetDataValue(log.Data, "repository")}` repository on {GetPlatform(log.Data)}";

        public string BuildGitRepositoryCreationLogText(PersonalLog log)
            => $"I have created the `{GetDataValue(log.Data, "repository")}` repository on {GetPlatform(log.Data)}";

        public string BuildGitRepositoryDeletionLogText(PersonalLog log)
            => $"I have deleted the `{GetDataValue(log.Data, "repository")}` repository from {GetPlatform(log.Data)}";

        public string BuildGitRepositoryRenameLogText(PersonalLog log)
            => $"I have renamed the `{GetDataValue(log.Data, "old_repository")}` repository from {GetPlatform(log.Data)} to `{GetDataValue(log.Data, "new_repository")}`";

        public string BuildGoingToSleepLogText(PersonalLog log)
        {
            string text = "I have gone to sleep" + GetLocation(log.Data);

            string side = GetSide(log.Data);

            if (!string.IsNullOrWhiteSpace(side) &&
                !string.Equals(side, MissingValue))
            {
                text += $", on the {side} side of the bed";
            }

            return text;
        }

        public string BuildGoingToTheChurchLogText(PersonalLog log)
            => "I have gone to the church" + GetLocation(log.Data);

        public string BuildGoingToTheToiletLogText(PersonalLog log)
            => "I have gone to the toilet" + GetLocation(log.Data);

        public string BuildGraduationCeremonyAttendanceLogText(PersonalLog log)
        {
            string text =
                $"I have attended {GetLocalisedValue(log.Data, "graduate_name")}'s graduation ceremony" +
                GetLocation(log.Data);

            string degreeLevel = GetMappedDataValue(
                log.Data,
                "degree_level",
                new()
                {
                    { "Bachelor", "bachelor" },
                    { "Master", "master" },
                    { "Doctorate", "doctorate" }
                });

            if (!string.IsNullOrWhiteSpace(degreeLevel))
            {
                text += $", for obtaining their {degreeLevel} degree";

                string institutionName = GetDataValue(log.Data, "institution_name");

                if (!string.IsNullOrWhiteSpace(institutionName))
                {
                    text += $" at {institutionName}";

                }
            }

            return text;
        }

        public string BuildGraduationCeremonyParticipationLogText(PersonalLog log)
        {
            string text = $"I have participated in my graduation ceremony";

            string degreeLevel = GetMappedDataValue(
                log.Data,
                "degree_level",
                new()
                {
                    { "Bachelor", "bachelor" },
                    { "Master", "master" },
                    { "Doctorate", "doctorate" }
                });

            if (!string.IsNullOrWhiteSpace(degreeLevel))
            {
                text += $", for obtaining my {degreeLevel} degree";

                string institutionName = GetDataValue(log.Data, "institution_name");

                if (!string.IsNullOrWhiteSpace(institutionName))
                {
                    text += $" at {institutionName}";

                }
            }

            return text + GetLocation(log.Data);
        }

        public string BuildHairCuttingLogText(PersonalLog log)
        {
            string text = string.Empty;
            string hairdresserName = GetByPerson(log.Data);
            bool hasHairdresser = !string.IsNullOrWhiteSpace(hairdresserName) &&
                !string.Equals(hairdresserName, MissingValue);

            if (hasHairdresser)
            {
                text += $"My {GetHairType(log.Data)} was cut";
            }
            else
            {
                text += $"I have cut my {GetHairType(log.Data)}";
            }

            text += GetLocation(log.Data);

            if (hasHairdresser)
            {
                text += $", by {hairdresserName}";
            }

            return text;
        }

        public string BuildHairTrimmingLogText(PersonalLog log)
            => $"I have trimmed my {GetHairType(log.Data)}" + GetLocation(log.Data);

        public string BuildHdlCholesterolMeasurementLogText(PersonalLog log)
            => $"My HDL cholesterol level measured {log.Data["hdl_cholesterol_level"]} {GetDataValue(log.Data, "unit", "mg/dL")}";

        public string BuildHeartRateMeasurementLogText(PersonalLog log)
        {
            string unit = GetDataValue(log.Data, "unit", "bpm");
            string text = $"My heart rate measured {log.Data["heart_rate"]} {unit}";

            string device = GetDevice(log.Data);

            if (!string.IsNullOrWhiteSpace(device) &&
                !string.Equals(device, MissingValue))
            {
                text += $" on the {device}";
            }

            return text;
        }

        public string BuildIndirectBilirubinMeasurementLogText(PersonalLog log)
            => $"My indirect bilirubin level measured {log.Data["indirect_bilirubin_level"]} {GetDataValue(log.Data, "unit", "mg/dL")}";

        public string BuildInternshipApplicationSubmissionLogText(PersonalLog log)
        {
            string internshipType = "internship";

            string period = GetDataValue(log.Data, "period");

            if (!string.IsNullOrWhiteSpace(period))
            {
                internshipType = $"{period} {internshipType}";
            }

            string text = $"I have submitted an application";


            string contactPersonName = GetDataValue(log.Data, "contact_person_name");

            if (!string.IsNullOrWhiteSpace(contactPersonName))
            {
                text += $" to {contactPersonName}";
            }

            text += $" for an {internshipType} at {log.Data["company_name"]}";

            string positionName = GetDataValue(log.Data, "position_name");

            if (!string.IsNullOrWhiteSpace(positionName))
            {
                text += $" for the position of {positionName}";
            }

            text = text.Replace("an summer", "a summer");

            return text;
        }

        public string BuildKinetotherapySessionLogText(PersonalLog log)
        {
            string text = $"I have undergone a kinetotherapy session" + GetLocation(log.Data);

            string therapistName = GetByPerson(log.Data);

            if (!string.IsNullOrWhiteSpace(therapistName) &&
                !string.Equals(therapistName, MissingValue))
            {
                text += $", with {therapistName}";
            }

            return text;
        }

        public string BuildLdlCholesterolMeasurementLogText(PersonalLog log)
            => $"My LDL cholesterol level measured {log.Data["ldl_cholesterol_level"]} {GetDataValue(log.Data, "unit", "mg/dL")}";

        public string BuildMagnesiumLevelMeasurementLogText(PersonalLog log)
            => $"My magnesium level measured {GetDecimalValue(log.Data, "magnesium_level")} {GetDataValue(log.Data, "unit", "mg/dL")}";

        public string BuildMealVoucherCardCreditationLogText(PersonalLog log)
            => $"My meal voucher card was credited with {GetBalance(log.Data)}";

        public string BuildMedicationIntakeLogText(PersonalLog log)
        {
            string text = $"I have taken the following";

            if (IsDataValuePlural(log.Data, "medication_name"))
            {
                text += $" {GetMedicationType(log.Data, true)}";
            }
            else
            {
                text += $" {GetMedicationType(log.Data, false)}";
            }

            return $"{text}: {GetLocalisedValue(log.Data, "medication_name")}";
        }

        public string BuildMicronationExternalRelationsRequestSendingLogText(PersonalLog log)
        {
            string relationTypeWord = GetMappedDataValue(
                log.Data,
                "relation_type",
                new()
                {
                    { "Alliance", "an alliance" },
                    { "DiplomaticRelations", "diplomatic relations" },
                    { "NonAggressionPact", "a non-agression pact" },
                    { "TradeAgreement", "a trade agreement" }
                },
                "diplomatic relations");

            return $"{log.Data["source_micronation_name"]} sent {relationTypeWord} request to {log.Data["target_micronation_name"]} din partea micronațiunii ";
        }

        public string BuildMicronationExternalRelationsRequestReceivalLogText(PersonalLog log)
        {
            string relationTypeWord = GetMappedDataValue(
                log.Data,
                "relation_type",
                new()
                {
                    { "Alliance", "an alliance" },
                    { "DiplomaticRelations", "diplomatic relations" },
                    { "NonAggressionPact", "a non-agression pact" },
                    { "TradeAgreement", "a trade agreement" }
                },
                "diplomatic relations");

            return $"{log.Data["target_micronation_name"]} has received {relationTypeWord} request from {log.Data["source_micronation_name"]}";
        }

        public string BuildMicronationExternalRelationsRequestRejectionLogText(PersonalLog log)
        {
            string relationTypeWord = GetMappedDataValue(
                log.Data,
                "relation_type",
                new()
                {
                    { "Alliance", "an alliance" },
                    { "DiplomaticRelations", "diplomatic relations" },
                    { "NonAggressionPact", "a non-agression pact" },
                    { "TradeAgreement", "a trade agreement" }
                },
                "diplomatic relations");

            string text = $"{log.Data["source_micronation_name"]}'s request for {relationTypeWord} with {log.Data["target_micronation_name"]}";

            string requestDate = GetDataValue(log.Data, "request_date");

            if (!string.IsNullOrWhiteSpace(requestDate))
            {
                text += $", sent on {requestDate},";
            }

            return $"{text} has been rejected";
        }

        public string BuildMicronationExternalRelationsEstablishmentLogText(PersonalLog log)
        {
            string relationshipTypeWord = GetMappedDataValue(
                log.Data,
                "relation_type",
                new()
                {
                    { "Alliance", "an alliance" },
                    { "DiplomaticRelations", "diplomatic relations" },
                    { "NonAggressionPact", "a non-agression pact" },
                    { "TradeAgreement", "a trade agreement" }
                },
                "diplomatic relations");

            string text = $"{log.Data["source_micronation_name"]} has established {relationshipTypeWord} with {log.Data["target_micronation_name"]}";

            string requestDate = GetDataValue(log.Data, "request_date");

            if (!string.IsNullOrWhiteSpace(requestDate))
            {
                text += $", following the request sent on {requestDate}";
            }

            return text;
        }

        public string BuildMicronationFoundingLogText(PersonalLog log)
            => $"I have founded the micronation of {GetDataValue(log.Data, "name")}";

        public string BuildMicronationLegalActIssuanceLogText(PersonalLog log)
        {
            string legalActTypeWord = GetMappedDataValue(
                log.Data,
                "legal_act_type",
                new()
                {
                    { "NucalDecree", "nucal decree" },
                    { "PalatinalDecree", "palatinal decree" },
                    { "PrefecturalDecree", "prefectural decree" },
                    { "VoivodalDecree", "voivodal decree" }
                },
                "legal act");

            string text = $"I have issued the {legalActTypeWord} '{log.Data["legal_act_name"]}'";

            if (log.Data.ContainsKey("administrative_unit_type"))
            {
                string administrativeUnitTypeWord = GetMappedDataValue(
                    log.Data,
                    "administrative_unit_type",
                    new()
                    {
                        { "Castle", "castle" },
                        { "City", "city" },
                        { "Town", "town" },
                        { "Village", "village" },
                        { "Land", "land" },
                        { "County", "county" },
                        { "District", "district" },
                        { "Zhupanate", "zhupanate" },
                        { "Voivodeship", "voivodeship" },
                        { "Prefecture", "prefecture" }
                    });

                text += $" in the {administrativeUnitTypeWord} of {log.Data["administrative_unit_name"]},";
            }

            text += $" in the micronation of {log.Data["micronation_name"]}";

            return text;
        }

        public string BuildMicronationNameChangeLogText(PersonalLog log)
            => $"I have changed the name of the micronation of {GetDataValue(log.Data, "old_name")} to {GetDataValue(log.Data, "new_name")}";

        public string BuildMicronationSettlementFoundingLogText(PersonalLog log)
        {
            string settlementType = GetMappedDataValue(
                log.Data,
                "settlement_type",
                new()
                {
                    { "Castle", "castle" },
                    { "City", "city" },
                    { "Town", "town" },
                    { "Village", "village" }
                },
                "settlement");

            string text = $"I have founded the {settlementType} of {log.Data["settlement_name"]}";

            string administrativeUnitName = GetDataValue(log.Data, "administrative_unit_name");
            if (!string.IsNullOrWhiteSpace(administrativeUnitName))
            {
                string administrativeUnitType = GetMappedDataValue(
                    log.Data,
                    "administrative_unit_type",
                    new()
                    {
                        { "Land", "land" },
                        { "County", "county" },
                        { "District", "district" },
                        { "Zhupanate", "zhupanate" },
                        { "Voivodeship", "voievodeship" },
                        { "Prefecture", "prefecture" }
                    });

                text += $" in the {administrativeUnitType} {administrativeUnitName},";
            }

            text += $" in the micronation of {log.Data["micronation_name"]}";

            return text;
        }

        public string BuildMicronationSettlementRankDowngradeLogText(PersonalLog log)
        {
            string oldRank = GetMappedDataValue(
                log.Data,
                "old_rank",
                new()
                {
                    { "Castle", "castle" },
                    { "City", "city" },
                    { "Town", "town" },
                    { "Village", "village" }
                },
                "settlement");

            string newRank = GetMappedDataValue(
                log.Data,
                "old_rank",
                new()
                {
                    { "Castle", "castle" },
                    { "City", "city" },
                    { "Town", "town" },
                    { "Village", "village" }
                });

            return $"I have downgraded the {oldRank} {log.Data["settlement_name"]} in the micronation of {log.Data["micronation_name"]} to the rank of {newRank}";
        }

        public string BuildMicronationSettlementRankUpgradeLogText(PersonalLog log)
        {
            string oldRank = GetMappedDataValue(
                log.Data,
                "old_rank",
                new()
                {
                    { "Castle", "castle" },
                    { "City", "city" },
                    { "Town", "town" },
                    { "Village", "village" }
                },
                "settlement");

            string newRank = GetMappedDataValue(
                log.Data,
                "old_rank",
                new()
                {
                    { "Castle", "castle" },
                    { "City", "city" },
                    { "Town", "town" },
                    { "Village", "village" }
                });

            return $"I have upgraded the {oldRank} {log.Data["settlement_name"]} in the micronation of {log.Data["micronation_name"]} to the rank of {newRank}";
        }

        public string BuildMovieBeginningLogText(PersonalLog log)
        {
            string text = $"I have begun watching the movie {log.Data["episode_number"]}";

            string platform = GetPlatform(log.Data);

            if (!string.IsNullOrWhiteSpace(platform))
            {
                text += $" on {platform}";
            }

            return text + GetLocation(log.Data);
        }

        public string BuildMovieCompletionLogText(PersonalLog log)
        {
            string text = $"I have finished watching the movie {log.Data["episode_number"]}";

            string platform = GetPlatform(log.Data);

            if (!string.IsNullOrWhiteSpace(platform))
            {
                text += $" on {platform}";
            }

            return text + GetLocation(log.Data);
        }

        public string BuildMovieWatchingLogText(PersonalLog log)
        {
            string text = $"I have watched the movie '{log.Data["movie_name"]}'";

            string platform = GetPlatform(log.Data);

            if (!string.IsNullOrWhiteSpace(platform))
            {
                text += $" on {platform}";
            }

            return text + GetLocation(log.Data);
        }

        public string BuildNailCuttingLogText(PersonalLog log)
            => $"I have cut my {GetNailsType(log.Data)}" + GetLocation(log.Data);

        public string BuildObjectSaleLogText(PersonalLog log)
        {
            string text = $"I have sold the {log.Data["object_name"]}";

            string platform = GetPlatform(log.Data);

            if (!string.IsNullOrWhiteSpace(platform))
            {
                text += $" on {platform}";
            }

            if (log.Data.ContainsKey("price_amount"))
            {
                text += $" for {GetBalance(log.Data)}";
            }

            return text;
        }

        public string BuildOnlineReviewSubmissionLogText(PersonalLog log)
        {
            string text = $"I have submitted a";

            string starsCount = GetDataValue(log.Data, "stars_count");

            if (!string.IsNullOrWhiteSpace(starsCount))
            {
                text += $" {starsCount}-star";
            }

            return text + $" review on {GetPlatform(log.Data)} for {log.Data["subject_name"]}";
        }

        public string BuildOnlineStorePurchaseLogText(PersonalLog log)
        {
            string text = $"I have purchased {log.Data["product_name"]} from {GetPlatform(log.Data)}";

            if (log.Data.ContainsKey("price_amount"))
            {
                text += $", for {GetBalance(log.Data)}";
            }

            return text;
        }

        public string BuildPetAdoptionLogText(PersonalLog log)
            => $"I have adopted {GetPet(log.Data)}" +
                GetLocation(log.Data);

        public string BuildPetBathingLogText(PersonalLog log)
            => $"I have bathed {GetPet(log.Data)}" +
                GetLocation(log.Data);

        public string BuildPetBrushingLogText(PersonalLog log)
            => $"I have brushed the fur of {GetPet(log.Data)}" +
                GetLocation(log.Data);

        public string BuildPetLitterCleaningLogText(PersonalLog log)
            => $"I have cleaned the {GetPetType(log.Data)} litter" + GetLocation(log.Data);

        public string BuildPetLitterEmptyingLogText(PersonalLog log)
            => $"I have emptied the {GetPetType(log.Data)} litter" + GetLocation(log.Data);

        public string BuildPetLitterRefillLogText(PersonalLog log)
            => $"I have refilled the {GetPetType(log.Data)} litter" + GetLocation(log.Data);

        public string BuildPetMedicationAdministrationLogText(PersonalLog log)
        {
            string text = $"I have administered the following";

            if (IsDataValuePlural(log.Data, "medication_name"))
            {
                text += $" {GetMedicationType(log.Data, true)}";
            }
            else
            {
                text += $" {GetMedicationType(log.Data, false)}";
            }

            return $"{text} to {GetPet(log.Data)}: {GetLocalisedValue(log.Data, "medication_name")}";
        }

        public string BuildPetNailsTrimmingLogText(PersonalLog log)
            => $"I have trimmed the nails of {GetPet(log.Data)}" + GetLocation(log.Data);

        public string BuildPetWeightMeasurementLogText(PersonalLog log)
        {
            string unit = GetDataValue(log.Data, "unit", "kg");
            string text = $"The weight of {GetPet(log.Data)} measured {log.Data["pet_weight"]} {unit}";

            string scaleName = GetDataValue(log.Data, "scale_name");

            if (!string.IsNullOrWhiteSpace(scaleName))
            {
                text += $" on the {scaleName} scale";
            }

            return text + GetLocation(log.Data);
        }

        public string BuildPhysiotherapySessionLogText(PersonalLog log)
        {
            string text = $"I have undergone a physiotherapy session" + GetLocation(log.Data);

            string therapistName = GetByPerson(log.Data);

            if (!string.IsNullOrWhiteSpace(therapistName) &&
                !string.Equals(therapistName, MissingValue))
            {
                text += $", with {therapistName}";
            }

            return text;
        }

        public string BuildPlantWateringLogText(PersonalLog log)
            => $"I have watered the {GetPlantType(log.Data, true, true)}" +
                GetLocation(log.Data);

        public string BuildProductKeyActivationLogText(PersonalLog log)
        {
            string text = "I have activated the product key";

            if (log.Data.ContainsKey("product_key"))
            {
                 text += $" '{GetDataValue(log.Data, "product_key")}'";
            }

            return $"{text} for {log.Data["product_name"]} on {GetPlatform(log.Data)}";
        }

        public string BuildPsychotherapySessionLogText(PersonalLog log)
        {
            string text = $"I have undergone a psychotherapy session" + GetLocation(log.Data);

            string therapistName = GetByPerson(log.Data);

            if (!string.IsNullOrWhiteSpace(therapistName) &&
                !string.Equals(therapistName, MissingValue))
            {
                text += $", with {therapistName}";
            }

            return text;
        }

        public string BuildPublicIpAddressMeasurementLogText(PersonalLog log)
            => $"The public IP address was {log.Data["ip_address"]}" + GetLocation(log.Data);

        public string BuildRestaurantVisitLogText(PersonalLog log)
            => $"I have been to the restaurant at" + GetLocation(log.Data);

        public string BuildSaunaSessionLogText(PersonalLog log)
        {
            string text = $"I have been to the sauna" + GetLocation(log.Data);

            string durationMinutes = GetDataValue(log.Data, "duration_minutes");

            if (!string.IsNullOrWhiteSpace(durationMinutes))
            {
                text += $", for {durationMinutes} minute";

                if (durationMinutes != "1")
                {
                    text += "s";
                }
            }

            return text;
        }

        public string BuildSeriesBeginningLogText(PersonalLog log)
        {
            string text = $"I began watching the '{log.Data["series_name"]}' series";

            string platform = GetPlatform(log.Data);

            if (!string.IsNullOrWhiteSpace(platform))
            {
                text += $" on {platform}";
            }

            return text + GetLocation(log.Data);
        }

        public string BuildSeriesCompletionLogText(PersonalLog log)
        {
            string text = $"I completed watching the '{log.Data["series_name"]}' series";

            string platform = GetPlatform(log.Data);

            if (!string.IsNullOrWhiteSpace(platform))
            {
                text += $" on {platform}";
            }

            return text + GetLocation(log.Data);
        }

        public string BuildSeriesEpisodeBeginningLogText(PersonalLog log)
        {
            string text = $"I have begun watching episode {log.Data["episode_number"]}";

            string episodeName = GetDataValue(log.Data, "episode_name");

            if (!string.IsNullOrWhiteSpace(episodeName))
            {
                text += $" '{episodeName}'";
            }

            string seasonNumber = GetDataValue(log.Data, "season_number");

            if (!string.IsNullOrWhiteSpace(seasonNumber))
            {
                text += $" of season {seasonNumber}";
            }

            string seriesName = GetDataValue(log.Data, "series_name");

            if (!string.IsNullOrWhiteSpace(seriesName))
            {
                text += $" of '{seriesName}'";
            }

            string platform = GetPlatform(log.Data);

            if (!string.IsNullOrWhiteSpace(platform))
            {
                text += $" on {platform}";
            }

            return text + GetLocation(log.Data);
        }

        public string BuildSeriesEpisodeCompletionLogText(PersonalLog log)
        {
            string text = $"I have finished watching episode {log.Data["episode_number"]}";

            string episodeName = GetDataValue(log.Data, "episode_name");

            if (!string.IsNullOrWhiteSpace(episodeName))
            {
                text += $" '{episodeName}'";
            }

            string seasonNumber = GetDataValue(log.Data, "season_number");

            if (!string.IsNullOrWhiteSpace(seasonNumber))
            {
                text += $" of season {seasonNumber}";
            }

            string seriesName = GetDataValue(log.Data, "series_name");

            if (!string.IsNullOrWhiteSpace(seriesName))
            {
                text += $" of '{seriesName}'";
            }

            string platform = GetPlatform(log.Data);

            if (!string.IsNullOrWhiteSpace(platform))
            {
                text += $" on {platform}";
            }

            return text + GetLocation(log.Data);
        }

        public string BuildSeriesEpisodeWatchingLogText(PersonalLog log)
        {
            string text = $"I have watched episode {log.Data["episode_number"]}";

            string episodeName = GetDataValue(log.Data, "episode_name");

            if (!string.IsNullOrWhiteSpace(episodeName))
            {
                text += $" '{episodeName}'";
            }

            string seasonNumber = GetDataValue(log.Data, "season_number");

            if (!string.IsNullOrWhiteSpace(seasonNumber))
            {
                text += $" of season {seasonNumber}";
            }

            string seriesName = GetDataValue(log.Data, "series_name");

            if (!string.IsNullOrWhiteSpace(seriesName))
            {
                text += $" of '{seriesName}'";
            }

            string platform = GetPlatform(log.Data);

            if (!string.IsNullOrWhiteSpace(platform))
            {
                text += $" on {platform}";
            }

            return text + GetLocation(log.Data);
        }

        public string BuildSeriesSeasonBeginningLogText(PersonalLog log)
        {
            string text = $"I began watching season {log.Data["season_number"]}";

            string seriesName = GetDataValue(log.Data, "series_name");

            if (!string.IsNullOrWhiteSpace(seriesName))
            {
                text += $" of '{seriesName}'";
            }

            string platform = GetPlatform(log.Data);

            if (!string.IsNullOrWhiteSpace(platform))
            {
                text += $" on {platform}";
            }

            return text + GetLocation(log.Data);
        }

        public string BuildSeriesSeasonCompletionLogText(PersonalLog log)
        {
            string text = $"I completed season {log.Data["season_number"]}";

            string seriesName = GetDataValue(log.Data, "series_name");

            if (!string.IsNullOrWhiteSpace(seriesName))
            {
                text += $" of '{seriesName}'";
            }

            string platform = GetPlatform(log.Data);

            if (!string.IsNullOrWhiteSpace(platform))
            {
                text += $" on {platform}";
            }

            return text + GetLocation(log.Data);
        }

        public string BuildShavingLogText(PersonalLog log)
            => $"I have shaved my {GetHairType(log.Data)}" + GetLocation(log.Data);

        public string BuildShowerBeginningLogText(PersonalLog log)
            => $"I have started taking a shower" + GetLocation(log.Data);

        public string BuildShowerCompletionLogText(PersonalLog log)
            => $"I have finished taking a shower" + GetLocation(log.Data);

        public string BuildShowerTakingLogText(PersonalLog log)
        {
            string text = $"I have taken a shower";

            string durationMinutes = GetDataValue(log.Data, "duration_minutes");

            if (!string.IsNullOrWhiteSpace(durationMinutes))
            {
                text += $" for {durationMinutes} minutes";
            }

            return text + GetLocation(log.Data);
        }

        public string BuildStepCountMeasurementLogText(PersonalLog log)
        {
            string text = $"I have walked {log.Data["step_count"]} steps";

            string distanceMetres = GetDataValue(log.Data, "distance_metres");

            if (!string.IsNullOrWhiteSpace(distanceMetres))
            {
                text += $", over {distanceMetres} de metres";
            }

            string caloriesBurned = GetDataValue(log.Data, "calories_burned");

            if (!string.IsNullOrWhiteSpace(caloriesBurned))
            {
                text += $", burning {caloriesBurned} kilocalories";
            }

            string device = GetDevice(log.Data);

            if (!string.IsNullOrWhiteSpace(device) &&
                !string.Equals(device, MissingValue))
            {
                text += $", according to the measurements made by the {device}";
            }

            return text;
        }

        public string BuildSwimmingActivityLogText(PersonalLog log)
            => $"I have gone swimming" + GetLocation(log.Data);

        public string BuildTeethBrushingLogText(PersonalLog log)
        {
            string text = $"I have brushed my teeth";

            string durationMinutes = GetDataValue(log.Data, "duration_minutes");

            if (!string.IsNullOrWhiteSpace(durationMinutes))
            {
                text += $" for {durationMinutes} minutes";
            }

            return text + GetLocation(log.Data);
        }

        public string BuildTheatricalPlayAttendanceLogText(PersonalLog log)
            => $"I have attended the '{log.Data["play_name"]}' theatrical play" + GetLocation(log.Data);

        public string BuildTollPaymentLogText(PersonalLog log)
        {
            string text = $"I have paid a toll";

            string providerName = GetDataValue(log.Data, "provider_name");

            if (!string.IsNullOrWhiteSpace(providerName))
            {
                text += $" to {providerName}";
            }

            string tollLocation = GetDataValue(log.Data, "toll_location");

            if (!string.IsNullOrWhiteSpace(tollLocation))
            {
                text += $" at {tollLocation}";
            }

            string vehicleRegistrationNumber = GetDataValue(log.Data, "vehicle_registration_number");

            if (!string.IsNullOrWhiteSpace(vehicleRegistrationNumber))
            {
                text += $" for the vehicle with the registration number {vehicleRegistrationNumber}";
            }

            if (log.Data.ContainsKey("cost_amount"))
            {
                text += $", amounting to {GetBalance(log.Data)}";
            }

            return text;
        }

        public string BuildTotalBilirubinMeasurementLogText(PersonalLog log)
            => $"My total bilirubin level measured {log.Data["total_bilirubin_level"]} {GetDataValue(log.Data, "unit", "mg/dL")}" +
                GetLocation(log.Data);

        public string BuildTotalCholesterolMeasurementLogText(PersonalLog log)
            => $"My total cholesterol level measured {log.Data["total_cholesterol_level"]} {GetDataValue(log.Data, "unit", "mg/dL")}" +
                GetLocation(log.Data);

        public string BuildTreePlantingLogText(PersonalLog log)
        {
            string text = $"I have planted";

            string treesCount = GetDataValue(log.Data, "trees_count", "1");

            if (treesCount.Equals("1"))
            {
                text += " a";
            }
            else
            {
                text += $" {treesCount}";
            }

            string treeSpecies = GetMappedDataValue(
                log.Data,
                "tree_species",
                new Dictionary<string, string>
                {
                    { "Oak", "oak" },
                    { "Pine", "pine" },
                    { "Maple", "maple" },
                    { "Birch", "birch" },
                    { "Cherry", "cherry" },
                    { "Apple", "apple" },
                    { "Walnut", "walnut" },
                    { "Willow", "willow" }
                });

            if (string.IsNullOrWhiteSpace(treeSpecies))
            {
                text += " tree";
            }
            else
            {
                text += $" {treeSpecies}";
            }

            if (!treesCount.Equals("1"))
            {
                text += "s";
            }

            return text + GetLocation(log.Data);
        }

        public string BuildUtilityBillPaymentLogText(PersonalLog log)
        {
            string utilityType = GetMappedDataValue(
                log.Data,
                "utility_type",
                new Dictionary<string, string>
                {
                    { "Electricity", "electricity" },
                    { "Gas", "gas" },
                    { "InternetAndTV", "internet and TV" },
                    { "Water", "water" }
                },
                "utilități"
            );

            string text = $"I have paid my {utilityType} bill to {log.Data["provider_name"]}";
            string discriminator = GetDiscriminator(log.Data);

            if (!string.IsNullOrWhiteSpace(discriminator))
            {
                text += $" ({discriminator})";
            }

            string supplyPointNumber = GetDataValue(log.Data, "supply_point_number");

            if (!string.IsNullOrWhiteSpace(supplyPointNumber))
            {
                text += $" for the {supplyPointNumber} supply point number";
            }

            text += GetLocation(log.Data);

            if (log.Data.ContainsKey("cost_amount"))
            {
                text += $", amounting to {GetBalance(log.Data)}";
            }

            return text;
        }

        public string BuildUtilityIndexMeasurementLogText(PersonalLog log)
        {
            string utilityType = GetMappedDataValue(
                log.Data,
                "utility_type",
                new Dictionary<string, string>
                {
                    { "Electricity", "electricity" },
                    { "Gas", "gas" },
                    { "InternetAndTV", "internet and TV" },
                    { "Water", "water" }
                },
                "utilitate"
            );

            string text = $"I have measured the {utilityType} index";

            string supplyPointNumber = GetDataValue(log.Data, "supply_point_number");

            if (!string.IsNullOrWhiteSpace(supplyPointNumber))
            {
                text += $" for the {supplyPointNumber} supply point number";
            }

            text += GetLocation(log.Data);

            string indexValue = GetDataValue(log.Data, "index_value");

            if (!string.IsNullOrWhiteSpace(indexValue))
            {
                text += $", obtaining a value of {indexValue}";
            }

            return text;
        }

        public string BuildVacuumCleaningLogText(PersonalLog log)
            => $"I have vacuum cleaned" + GetLocation(log.Data);

        public string BuildVehicleFluidChangingLogText(PersonalLog log)
        {
            string text = $"The {GetFluidType(log.Data, false)} of";

            string vehicleModel = GetDataValue(log.Data, "vehicle_model");

            if (!string.IsNullOrWhiteSpace(vehicleModel))
            {
                text += $" {vehicleModel}";
            }

            text += $" {GetVehicleType(log.Data, true)}";

            string vehicleName = GetDataValue(log.Data, "vehicle_name");

            if (!string.IsNullOrWhiteSpace(vehicleName))
            {
                text += $" '{vehicleName}'";
            }

            string vehicleRegistrationNumber = GetDataValue(log.Data, "vehicle_registration_number");

            if (!string.IsNullOrWhiteSpace(vehicleRegistrationNumber))
            {
                text += $" with the '{vehicleRegistrationNumber}' registration number";
            }

            text += " has been changed" + GetLocation(log.Data);

            string mechanicName = GetByPerson(log.Data);

            if (!string.IsNullOrWhiteSpace(mechanicName) &&
                !string.Equals(mechanicName, MissingValue))
            {
                text += $", by {mechanicName}";
            }

            return text;
        }

        public string BuildVehicleFluidRefillingLogText(PersonalLog log)
        {
            string text = $"The {GetFluidType(log.Data, false)} of";

            string vehicleModel = GetDataValue(log.Data, "vehicle_model");

            if (!string.IsNullOrWhiteSpace(vehicleModel))
            {
                text += $" {vehicleModel}";
            }

            text += $" {GetVehicleType(log.Data, true)}";

            string vehicleName = GetDataValue(log.Data, "vehicle_name");

            if (!string.IsNullOrWhiteSpace(vehicleName))
            {
                text += $" '{vehicleName}'";
            }

            string vehicleRegistrationNumber = GetDataValue(log.Data, "vehicle_registration_number");

            if (!string.IsNullOrWhiteSpace(vehicleRegistrationNumber))
            {
                text += $" with the '{vehicleRegistrationNumber}' registration number";
            }

            text += " has been refilled" + GetLocation(log.Data);

            string mechanicName = GetByPerson(log.Data);

            if (!string.IsNullOrWhiteSpace(mechanicName) &&
                !string.Equals(mechanicName, MissingValue))
            {
                text += $", by {mechanicName}";
            }

            return text;
        }

        public string BuildVehicleMileageMeasurementLogText(PersonalLog log)
        {
            string text = $"The total mileage of the";

            string vehicleModel = GetDataValue(log.Data, "vehicle_model");

            if (!string.IsNullOrWhiteSpace(vehicleModel))
            {
                text += $" {vehicleModel}";
            }

            text += $" {GetVehicleType(log.Data, true)}";

            string vehicleName = GetDataValue(log.Data, "vehicle_name");

            if (!string.IsNullOrWhiteSpace(vehicleName))
            {
                text += $" '{vehicleName}'";
            }

            string vehicleRegistrationNumber = GetDataValue(log.Data, "vehicle_registration_number");

            if (!string.IsNullOrWhiteSpace(vehicleRegistrationNumber))
            {
                text += $" with the '{vehicleRegistrationNumber}' registration number";
            }

            return $"{text} was measured at {GetDataValue(log.Data, "distance")} {GetDataValue(log.Data, "unit", "km")}";
        }

        public string BuildVideoUploadLogText(PersonalLog log)
        {
            string text = $"I have uploaded a video titled '{log.Data["video_title"]}' to {GetPlatform(log.Data)}";

            string uploadedFileName = GetDataValue(log.Data, "uploaded_file_name");

            if (!string.IsNullOrWhiteSpace(uploadedFileName))
            {
                text += $", from the '{uploadedFileName}' file";
            }

            string videoId = GetDataValue(log.Data, "video_url");

            if (!string.IsNullOrWhiteSpace(videoId))
            {
                text += $", at {videoId}";
            }

            return text;
        }

        public string BuildVideoWatchingLogText(PersonalLog log)
        {
            string text = $"I have watched the video '{log.Data["video_title"]}'";

            string videoId = GetDataValue(log.Data, "video_id");

            if (!string.IsNullOrWhiteSpace(videoId))
            {
                text += $" ({videoId})";
            }

            string channelName = GetDataValue(log.Data, "channel_name");

            if (!string.IsNullOrWhiteSpace(channelName))
            {
                text += $" from the '{channelName}' channel";
            }

            string platform = GetPlatform(log.Data);

            if (!string.IsNullOrWhiteSpace(platform))
            {
                text += $" on {platform}";
            }

            return text + GetLocation(log.Data);
        }

        public string BuildWakingUpLogText(PersonalLog log)
        {
            string text = "I have woken up" + GetLocation(log.Data);

            string side = GetSide(log.Data);

            if (!string.IsNullOrWhiteSpace(side) &&
                !string.Equals(side, MissingValue))
            {
                text += $", on the {side} side of the bed";
            }

            return text;
        }

        public string BuildWaterDrinkingLogText(PersonalLog log)
            => $"I have drunk water" + GetLocation(log.Data);

        public string BuildWeddingAttendanceLogText(PersonalLog log)
        {
            string text = $"I have attended the wedding of";

            string brideName = GetDataValue(log.Data, "bride_name");
            string groomName = GetDataValue(log.Data, "groom_name");

            if (!string.IsNullOrWhiteSpace(brideName) && !string.IsNullOrWhiteSpace(groomName))
            {
                text += $" {groomName} and {brideName}";
            }
            else if (!string.IsNullOrWhiteSpace(brideName))
            {
                text += $" {brideName}";
            }
            else if (!string.IsNullOrWhiteSpace(groomName))
            {
                text += $" {groomName}";
            }

            return text + GetLocation(log.Data);
        }

        public string BuildWindowClosingLogText(PersonalLog log)
            =>$"I have closed the window" + GetLocation(log.Data);

        public string BuildWindowOpeningLogText(PersonalLog log)
            => $"I have opened the window" + GetLocation(log.Data);

        public string BuildWorkFromTheOfficeLogText(PersonalLog log)
            => $"I have worked from the office" + GetLocation(log.Data);

        public string BuildWorkMandatoryCourseBeginningLogText(PersonalLog log)
        {
            string text = $"I have started the mandatory work course '{log.Data["course_name"]}'";

            string employerName = GetDataValue(log.Data, "employer_name");

            if (!string.IsNullOrWhiteSpace(employerName))
            {
                text += $" for {employerName}";
            }

            return text;
        }

        public string BuildWorkMandatoryCourseCompletionLogText(PersonalLog log)
        {
            string text = $"I have completed the mandatory work course '{log.Data["course_name"]}'";

            string employerName = GetDataValue(log.Data, "employer_name");

            if (!string.IsNullOrWhiteSpace(employerName))
            {
                text += $" for {employerName}";
            }

            string scorePercentage = GetDataValue(log.Data, "score_obtained");

            if (!string.IsNullOrWhiteSpace(scorePercentage))
            {
                text += $", obtaining a score of {scorePercentage}%";
            }

            return text;
        }

        public string BuildWorkOnCallAlertReceivalLogText(PersonalLog log)
        {
            string text = $"I have received an on-call alert for {GetDataValue(log.Data, "employer_name")}";

            string platform = GetPlatform(log.Data);

            if (!string.IsNullOrWhiteSpace(platform))
            {
                text += $" on {platform}";
            }

            string alertSubject = GetDataValue(log.Data, "alert_subject");

            if (!string.IsNullOrWhiteSpace(alertSubject))
            {
                text += $": {alertSubject}";
            }

            return text;
        }

        public string BuildWorkOnCallShiftBeginningLogText(PersonalLog log)
            => $"My on-call work shift for {GetDataValue(log.Data, "employer_name")} has begun";

        public string BuildWorkOnCallShiftEndingLogText(PersonalLog log)
            => $"My on-call work shift for {GetDataValue(log.Data, "employer_name")} has ended";

        public string BuildWorkTimesheetSubmissionLogText(PersonalLog log)
        {
            string text = $"I have submitted my {GetDataValue(log.Data, "employer_name")} timesheets";

            string weekNumber = GetDataValue(log.Data, "week_number");

            if (!string.IsNullOrWhiteSpace(weekNumber))
            {
                text += $", for week #{weekNumber} of ";

                string year = GetDataValue(log.Data, "year");

                if (!string.IsNullOrWhiteSpace(year))
                {
                    text += year;
                }
                else
                {
                    text += "the current year";
                }
            }

            return text;
        }

        protected override string GetAccessoryType(
            Dictionary<string, string> data,
            bool useDefinitiveForm)
        {
            string accessoryType = GetMappedDataValue(data, "accessory_type", new()
            {
                { "Glasses", "glasses" }
            },
            "accessory");

            if (useDefinitiveForm)
            {
                return $"the {accessoryType}";
            }

            return accessoryType;
        }

        protected override string GetByPerson(Dictionary<string, string> data)
        {
            if (data.ContainsKey("by"))
            {
                return GetLocalisedValue(data, "by");
            }
            else if (data.ContainsKey("dentist_name"))
            {
                return GetLocalisedValue(data, "dentist_name");
            }
            else if (data.ContainsKey("from"))
            {
                return GetLocalisedValue(data, "from");
            }
            else if (data.ContainsKey("giver_name"))
            {
                return GetLocalisedValue(data, "giver_name");
            }
            else if (data.ContainsKey("hairdresser_name"))
            {
                return GetLocalisedValue(data, "hairdresser_name");
            }
            else if (data.ContainsKey("mechanic_name"))
            {
                return GetLocalisedValue(data, "mechanic_name");
            }
            else if (data.ContainsKey("optometrist_name"))
            {
                return GetLocalisedValue(data, "optometrist_name");
            }
            else if (data.ContainsKey("therapist_name"))
            {
                return GetLocalisedValue(data, "therapist_name");
            }
            else if (data.ContainsKey("with"))
            {
                return GetLocalisedValue(data, "with");
            }

            return MissingValue;
        }

        protected override string GetCleaningMethod(Dictionary<string, string> data)
            => GetMappedDataValue(data, "cleaning_method", new()
            {
                { "AirBlower", "using an air blower" },
                { "Brushing", "periere" },
                { "CottonBuds", "using cotton buds" },
                { "Dusting", "dusting" },
                { "LintRemover", "using a lint remover" },
                { "LintRoller", "using a lint roller" },
                { "SpiralEarCleaner", "using a spiral ear cleaner" },
                { "Vacuuming", "vacuuming" },
                { "Washing", "washing" },
                { "Wiping", "wiping" },
            });

        protected override string GetDevice(Dictionary<string, string> data)
        {
            string text = string.Empty;

            string deviceName = string.Empty;

            if (data.ContainsKey("device_name"))
            {
                deviceName = GetDataValue(data, "device_name");
            }
            else if (data.ContainsKey("scale_name"))
            {
                deviceName = GetDataValue(data, "scale_name");
            }

            if (!string.IsNullOrWhiteSpace(deviceName))
            {
                text += deviceName;
            }

            if (data.ContainsKey("device_type"))
            {
                if (!string.IsNullOrWhiteSpace(text))
                {
                    text += " ";
                }

                text += GetDeviceType(data);
            }

            return text;
        }

        protected override string GetDeviceType(Dictionary<string, string> data)
            => GetMappedDataValue(data, "device_type", new()
                {
                    { "AirBlower", "air blower" },
                    { "BloodGlucoseMeter", "blood glucose meter" },
                    { "BodyScale", "body scale" },
                    { "BodyTrimmer", "body trimmer" },
                    { "Computer", "calculatorul" },
                    { "Console", "console" },
                    { "Dehumidifier", "dehumidifier" },
                    { "DesktopComputer", "desktop computer" },
                    { "FitnessTracker", "fitness tracker" },
                    { "HairTrimmer", "hair trimmer" },
                    { "Headphones", "headphones" },
                    { "HeadTorch", "head torch" },
                    { "HeartRateMonitor", "heart rate monitor" },
                    { "Laptop", "laptop" },
                    { "LaserPetToy", "laser toy for pets" },
                    { "LintRemover", "lint remover" },
                    { "Phone", "phone" },
                    { "PrecissionTrimmer", "precission trimmer" },
                    { "Scale", "scale" },
                    { "Scooter", "scooter" },
                    { "Tablet", "tablet" },
                    { "Toothbrush", "toothbrush" },
                    { "VacuumCleaner", "vacuum cleaner" },
                    { "Watch", "watch" },
                    { "WaterFlosser", "water flosser" },
                    { "WirelessSpeaker", "wireless speaker" }
                },
                data["device_type"].ToLower()
            );

        protected override string GetFluidType(
            Dictionary<string, string> data,
            bool useDefinitiveForm)
        {
            string fluidType = GetMappedDataValue(data, "fluid_type", new()
            {
                { "Coolant", "coolant" },
                { "MotorOil", "motor oil" },
                { "WindscreenWashingFluid", "windscreen washing fluid" }
            },
            "liquid");

            if (useDefinitiveForm)
            {
                return $"the {fluidType}";
            }

            return fluidType;
        }

        protected override string GetFurnitureType(Dictionary<string, string> data)
            => GetMappedDataValue(data, "furniture_type", new()
            {
                { "Armchair", "armchair" },
                { "BathroomSink", "sink" },
                { "CatTree", "cat tree" },
                { "Chair", "chair" },
                { "Chairs", "chairs" },
                { "CoffeeTable", "coffee table" },
                { "Couch", "couch" },
                { "Countertop", "countertop" },
                { "Desk", "desk" },
                { "Doorstop", "doorstop" },
                { "kitchenSink", "sink" },
                { "Radiator", "radiator" },
                { "Sink", "sink" },
                { "Table", "table" },
                { "Toilet", "toilet" },
                { "TvStand", "television stand" },
                { "Washbasin", "washbasin" },
                { "WindowSill", "window sill" }
            },
            "furniture");

        protected override string GetHairType(Dictionary<string, string> data)
            => GetMappedDataValue(data, "hair_type", new()
            {
                { "Beard", "beard" },
                { "ChestHair", "chest hair" },
                { "EyebrowHair", "eyebrow hair" },
                { "FacialHair", "facial hair" },
                { "FootHair", "foot hair" },
                { "GenitalHair", "pubic hair" },
                { "HeadHair", "head hair" },
                { "LegHair", "leg hair" },
                { "Mustache", "mustache" },
                { "NoseHair", "nose hair" },
                { "Sideburns", "sideburns" },
                { "UnderarmHair", "underarm hair" },
                { "Unibrow", "unibrow" }
            },
            "hair");

        protected override string GetLocation(Dictionary<string, string> data)
        {
            string text = string.Empty;
            string room = string.Empty;

            if (data.ContainsKey("room"))
            {
                room = GetRoom(data);
            }

            if (!string.IsNullOrWhiteSpace(room))
            {
                text += $", in the {room}";
            }

            string buildingName = string.Empty;

            if (data.ContainsKey("building_name"))
            {
                buildingName = GetDataValue(data, "building_name");
            }
            else if (data.ContainsKey("church_name"))
            {
                buildingName = GetDataValue(data, "church_name");
            }
            else if (data.ContainsKey("clinic_name"))
            {
                buildingName = GetDataValue(data, "clinic_name");
            }
            else if (data.ContainsKey("donation_centre_name"))
            {
                buildingName = GetDataValue(data, "donation_centre_name");
            }
            else if (data.ContainsKey("location_name"))
            {
                buildingName = GetDataValue(data, "location_name");
            }
            else if (data.ContainsKey("office_name"))
            {
                buildingName = GetDataValue(data, "office_name");
            }
            else if (data.ContainsKey("restaurant_name"))
            {
                buildingName = GetDataValue(data, "restaurant_name");
            }
            else if (data.ContainsKey("salon_name"))
            {
                buildingName = GetDataValue(data, "salon_name");
            }
            else if (data.ContainsKey("theatre_name"))
            {
                buildingName = GetDataValue(data, "theatre_name");
            }
            else if (data.ContainsKey("venue_name"))
            {
                buildingName = GetDataValue(data, "venue_name");
            }

            if (!string.IsNullOrWhiteSpace(buildingName))
            {
                if (string.IsNullOrWhiteSpace(text))
                {
                    text += ",";
                }

                text += $" at {buildingName}";
            }

            string location = string.Empty;

            if (data.ContainsKey("location"))
            {
                location = GetDataValue(data, "location");
            }
            else if (data.ContainsKey("location_city"))
            {
                location = GetDataValue(data, "location_city");
            }
            else if (data.ContainsKey("event_location"))
            {
                location = GetDataValue(data, "event_location");
            }

            if (!string.IsNullOrWhiteSpace(location))
            {
                if (string.IsNullOrWhiteSpace(text))
                {
                    text += ",";
                }

                if (string.IsNullOrWhiteSpace(buildingName))
                {
                    text += " at";
                }
                else
                {
                    text += " in";
                }

                text += $" {location}";
            }

            if (!string.IsNullOrWhiteSpace(text) &&
                data.ContainsKey("floor_index"))
            {
                text += $", on floor {GetDataValue(data, "floor_index")}";
            }

            string with = string.Empty;

            if (data.ContainsKey("with"))
            {
                with = GetLocalisedValue(data, "with");
            }
            else if (data.ContainsKey("together_with"))
            {
                with = GetLocalisedValue(data, "together_with");
            }
            else if (data.ContainsKey("watched_with"))
            {
                with = GetLocalisedValue(data, "watched_with");
            }

            if (!string.IsNullOrWhiteSpace(with))
            {
                text += $", together with {with}";
            }

            return text;
        }

        protected override string GetMedicationType(
            Dictionary<string, string> data,
            bool usePluralForm)
        {
            if (usePluralForm)
            {
                return GetMappedDataValue(
                    data,
                    "medication_type",
                    new()
                    {
                        { "Antiacid", "antiacids" },
                        { "Antibiotic", "antibiotics" },
                        { "Antifungal", "antifungals" },
                        { "Antiinflammatory", "anti-inflammatories" },
                        { "Antiparasitic", "antiparasitics" },
                        { "Antiseptic", "antiseptics" },
                        { "Anxiolytic", "anxiolytics" },
                        { "Corticosteroid", "corticosteroids" },
                        { "Enzymatic", "enzymatics" },
                        { "Gastroprotective", "gastroprotectives" },
                        { "Painkiller", "painkillers" },
                        { "Probiotic", "probiotics" },
                        { "Supplement", "supplements" },
                        { "Vaccine", "vaccines" },
                    },
                    "medications");
            }

            return GetMappedDataValue(
                data,
                "medication_type",
                new()
                {
                    { "Antiacid", "antiacid" },
                    { "Antibiotic", "antibiotic" },
                    { "Antifungal", "antifungal" },
                    { "Antiinflammatory", "anti-inflammatory" },
                    { "Antiparasitic", "antiparasitic" },
                    { "Antiseptic", "antiseptic" },
                    { "Anxiolytic", "anxiolytic" },
                    { "Corticosteroid", "corticosteroid" },
                    { "Enzymatic", "enzymatic" },
                    { "Gastroprotective", "gastroprotective" },
                    { "Painkiller", "painkiller" },
                    { "Probiotic", "probiotic" },
                    { "Supplement", "supplement" },
                    { "Vaccine", "vaccine" },
                },
                "medication");
        }

        protected override string GetNailsType(Dictionary<string, string> data)
            => GetMappedDataValue(data, "nails_type", new()
            {
                { "FingerNails", "finger nails" },
                { "ToeNails", "toe nails" }
            }, "nails");

        protected override string GetPet(Dictionary<string, string> data)
            => $"{GetDataValue(data, "pet_name")} {GetPetType(data, true)}";

        protected override string GetPetType(
            Dictionary<string, string> data,
            bool useDefinitiveForm,
            bool usePluralForm)
        {
            string petType = GetMappedDataValue(data, "pet_type", new()
            {
                { "Cat", "cat" },
                { "Dog", "dog" },
                { "Rabbit", "rabbit" },
                { "Ferret", "ferret" },
                { "GuineaPig", "guinea pig" }
            }, "pet");

            if (useDefinitiveForm)
            {
                petType = $"the {petType}";
            }

            if (usePluralForm)
            {
                if (petType.EndsWith("s"))
                {
                    petType += "es";
                }
                else
                {
                    petType += "s";
                }
            }

            return petType;
        }

        protected override string GetPlantType(
            Dictionary<string, string> data,
            bool useDefinitiveForm,
            bool usePluralForm)
        {
            string plantType = null;

            if (usePluralForm)
            {
                plantType = GetMappedDataValue(
                    data,
                    "plant_type",
                    new()
                    {
                        { "Flower", "flowers" },
                        { "Succulent", "succulents" },
                    },
                    "plants");

                if (useDefinitiveForm)
                {
                    return $"the {plantType}";
                }

                return plantType;
            }

            plantType = GetMappedDataValue(
                data,
                "plant_type",
                new()
                {
                    { "Flower", "flower" },
                    { "Succulent", "succulent" },
                },
                "plant");

            if (useDefinitiveForm)
            {
                return $"the {plantType}";
            }

            return plantType;
        }

        protected override string GetRoom(Dictionary<string, string> data)
            => GetMappedDataValue(data, "room", new()
                {
                    { "AccessibleBathroom", "accessible bathroom" },
                    { "Attic", "attic" },
                    { "BackBalcony", "back balcony" },
                    { "BackPorch", "back porch" },
                    { "Balcony", "balcony" },
                    { "Bathroom", "bathroom" },
                    { "Bedroom", "bedroom" },
                    { "DressingRoom", "dressing room" },
                    { "FemaleBathroom", "female bathroom" },
                    { "FrontBalcony", "front balcony" },
                    { "FrontPorch", "front porch" },
                    { "Hallway", "hallway" },
                    { "LargerBathroom", "larger bathroom" },
                    { "LargerBedroom", "larger bedroom" },
                    { "LivingRoom", "living room" },
                    { "LowerBathroom", "lower bathroom" },
                    { "LowerBedroom", "lower bedroom" },
                    { "LowerHallway", "lower hallway" },
                    { "MaleBathroom", "male bathroom" },
                    { "Kitchen", "kitchen" },
                    { "Office", "office" },
                    { "Pantry", "pantry" },
                    { "Porch", "porch" },
                    { "SmallerBathroom", "smaller bathroom" },
                    { "SmallerBedroom", "smaller bedroom" },
                    { "Stairway", "stairway" },
                    { "UppperBathroom", "upper bathroom" },
                    { "UpperBedroom", "upper bedroom" },
                    { "UpperHallway", "upper hallway" }
                },
                data["room"].ToLower()
            );

        protected override string GetSide(Dictionary<string, string> data)
            => GetMappedDataValue(data, "side", new()
            {
                { "central", "central" },
                { "right", "right" },
                { "left", "left" }
            }, MissingValue);

        protected override string GetVehicleType(Dictionary<string, string> data, bool useDefinitiveForm)
        {
            string vehicleType = GetMappedDataValue(data, "vehicle_type", new()
            {
                { "Bicycle", "bicycle" },
                { "Car", "car" },
                { "ElectricScooter", "electric scooter" },
            },
            "vehicle");

            if (useDefinitiveForm)
            {
                return $"the {vehicleType}";
            }

            return vehicleType;
        }
    }
}
