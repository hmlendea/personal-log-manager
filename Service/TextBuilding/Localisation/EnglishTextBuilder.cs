using PersonalLogManager.Service.Models;

namespace PersonalLogManager.Service.TextBuilding.Localisation
{
    public class EnglishTextBuilder() : PersonalLogTextBuilderBase, IPersonalLogTextBuilder
    {
        public string BuildAccountActivationLogText(PersonalLog log)
        {
            string text = $"I have activated the {log.Data["platform"]} account";
            string discriminator = GetDiscriminator(log.Data);

            if (!string.IsNullOrWhiteSpace(discriminator))
            {
                text += $" ({discriminator})";
            }

            return text;
        }

        public string BuildAccountBanningLogText(PersonalLog log)
        {
            string text = $"The {log.Data["platform"]} account";
            string discriminator = GetDiscriminator(log.Data);

            if (!string.IsNullOrWhiteSpace(discriminator))
            {
                text += $" ({discriminator})";
            }

            text += " has been banned";

            if (log.Data.TryGetValue("ban_reason", out string banReason))
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

        public string BuildAccountDataExportLogText(PersonalLog log)
        {
            string text = $"I have exported my data related to the {log.Data["platform"]} account";
            string discriminator = GetDiscriminator(log.Data);

            if (!string.IsNullOrWhiteSpace(discriminator))
            {
                text += $" ({discriminator})";
            }

            return text;
        }

        public string BuildAccountDataExportRequestLogText(PersonalLog log)
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

        public string BuildAccountDataExportRequestFulfillmentLogText(PersonalLog log)
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

        public string BuildAccountDataExportSaveLogText(PersonalLog log)
        {
            string text = $"I have saved the export of the data related to the {log.Data["platform"]} account";
            string discriminator = GetDiscriminator(log.Data);

            if (!string.IsNullOrWhiteSpace(discriminator))
            {
                text += $" ({discriminator})";
            }

            log.Data.TryGetValue("request_date", out string requestDate);
            log.Data.TryGetValue("request_id", out string requestId);

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
        {
            string text = $"I have obfuscated the data on the {log.Data["platform"]} account";
            string discriminator = GetDiscriminator(log.Data);

            if (!string.IsNullOrWhiteSpace(discriminator))
            {
                text += $" ({discriminator})";
            }

            return text;
        }

        public string BuildAccountDeactivationLogText(PersonalLog log)
        {
            string text = $"I have deactivated the {log.Data["platform"]} account";
            string discriminator = GetDiscriminator(log.Data);

            if (!string.IsNullOrWhiteSpace(discriminator))
            {
                text += $" ({discriminator})";
            }

            return text;
        }

        public string BuildAccountDeletionLogText(PersonalLog log)
        {
            string text = $"I have deleted the {log.Data["platform"]} account";
            string discriminator = GetDiscriminator(log.Data);

            if (!string.IsNullOrWhiteSpace(discriminator))
            {
                text += $" ({discriminator})";
            }

            return text;
        }

        public string BuildAccountDeletionRequestLogText(PersonalLog log)
        {
            string text = $"I have sent a request for the deletion of the {log.Data["platform"]} account";
            string discriminator = GetDiscriminator(log.Data);

            if (!string.IsNullOrWhiteSpace(discriminator))
            {
                text += $" ({discriminator})";
            }

            if (log.Data.TryGetValue("request_id", out string requestId))
            {
                text += $" with the {requestId} identification code";
            }

            if (log.Data.TryGetValue("request_method", out string requestMethod))
            {
                text += $" via {requestMethod}";
            }

            return text;
        }

        public string BuildAccountDeletionRequestCancellationLogText(PersonalLog log)
        {
            string text = $"I have cancelled the account deletion request for the {log.Data["platform"]} account";
            string discriminator = GetDiscriminator(log.Data);

            if (!string.IsNullOrWhiteSpace(discriminator))
            {
                text += $" ({discriminator})";
            }

            if (log.Data.TryGetValue("request_id", out string requestId))
            {
                text += $" with the {requestId} identification code";
            }

            if (log.Data.TryGetValue("request_date", out string requestDate))
            {
                text += $", sent on {requestDate},";
            }

            return text;
        }

        public string BuildAccountDeletionRequestFulfillmentLogText(PersonalLog log)
        {
            string text = $"My account deletion request for the {log.Data["platform"]} account";
            string discriminator = GetDiscriminator(log.Data);

            if (!string.IsNullOrWhiteSpace(discriminator))
            {
                text += $" ({discriminator})";
            }

            if (log.Data.TryGetValue("request_id", out string requestId))
            {
                text += $" with the {requestId} identification code";
            }

            if (log.Data.TryGetValue("request_date", out string requestDate))
            {
                text += $", sent on {requestDate},";
            }

            text += " has been fulfilled";

            return text;
        }

        public string BuildAccountDeletionValidationLogText(PersonalLog log)
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

        public string BuildAccountEmailAddressChangeLogText(PersonalLog log)
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

        public string BuildAccountEmailAddressChangeRequestLogText(PersonalLog log)
        {
            string text = $"I have sent a request to change the e-mail address of the {log.Data["platform"]} account";
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

            if (log.Data.TryGetValue("request_id", out string requestId))
            {
                text += $", with the {requestId} identification code";
            }

            if (log.Data.TryGetValue("request_method", out string requestMethod))
            {
                text += $", via {requestMethod}";
            }

            return text;
        }

        public string BuildAccountEmailAddressChangeRequestFulfillmentLogText(PersonalLog log)
        {
            string text = $"My request to change the e-mail address of the {log.Data["platform"]} account";

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

            if (log.Data.TryGetValue("request_id", out string requestId))
            {
                text += $", with the {requestId} identification code";
            }

            if (log.Data.TryGetValue("request_date", out string requestDate))
            {
                text += $", sent on {requestDate},";
            }

            text += " has been fulfilled";

            return text;
        }

        public string BuildAccountEmailAddressConfirmationLogText(PersonalLog log)
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

        public string BuildAccountFeatureEnablementLogText(PersonalLog log)
        {
            string text = $"I have enabled the {log.Data["feature_name"]} feature for the {log.Data["platform"]} account";
            string discriminator = GetDiscriminator(log.Data);

            if (!string.IsNullOrWhiteSpace(discriminator))
            {
                text += $" ({discriminator})";
            }

            return text;
        }

        public string BuildAccountFeatureDisablementLogText(PersonalLog log)
        {
            string text = $"I have disabled the {log.Data["feature_name"]} feature for the {log.Data["platform"]} account";
            string discriminator = GetDiscriminator(log.Data);

            if (!string.IsNullOrWhiteSpace(discriminator))
            {
                text += $" ({discriminator})";
            }

            return text;
        }

        public string BuildAccountFriendshipRequestReceivalLogText(PersonalLog log)
        {
            string text = $"I have received a friendship request on the {log.Data["platform"]} account";
            string discriminator = GetDiscriminator(log.Data);

            if (!string.IsNullOrWhiteSpace(discriminator))
            {
                text += $" ({discriminator})";
            }

            if (log.Data.TryGetValue("from_account", out string fromAccount))
            {
                text += $" from {fromAccount}";
            }

            return text;
        }

        public string BuildAccountIdentityVerificationLogText(PersonalLog log)
        {
            string text = $"I have verified my identity for the {log.Data["platform"]} account";
            string discriminator = GetDiscriminator(log.Data);

            if (!string.IsNullOrWhiteSpace(discriminator))
            {
                text += $" ({discriminator})";
            }

            return text;
        }

        public string BuildAccountLinkingLogText(PersonalLog log)
        {
            string text = $"I have linked the {log.Data["platform"]} account";
            string discriminator = GetDiscriminator(log.Data);

            if (!string.IsNullOrWhiteSpace(discriminator))
            {
                text += $" ({discriminator})";
            }

            text += $" with";

            if (log.Data.TryGetValue("platform_linked", out string platformLinked))
            {
                if (log.Data.TryGetValue("account_linked", out string accountLinked))
                {
                    text += $" the {log.Data["platform_linked"]} account ({log.Data["account_linked"]})";
                }
                else
                {
                    text += $" {log.Data["platform_linked"]}";
                }
            }
            else
            {
                text += $" {log.Data["account_linked"]}";
            }

            return text;
        }

        public string BuildAccountMessagesErasureLogText(PersonalLog log)
        {
            string text = $"I have erased all messages from the {log.Data["platform"]} account";
            string discriminator = GetDiscriminator(log.Data);

            if (!string.IsNullOrWhiteSpace(discriminator))
            {
                text += $" ({discriminator})";
            }

            return text;
        }

        public string BuildAccountPasswordChangeLogText(PersonalLog log)
        {
            string text = $"I have changed the password of the {log.Data["platform"]} account";
            string discriminator = GetDiscriminator(log.Data);

            if (!string.IsNullOrWhiteSpace(discriminator))
            {
                text += $" ({discriminator})";
            }

            return text;
        }

        public string BuildAccountPersonalNameChangeLogText(PersonalLog log)
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

        public string BuildAccountPhoneNumberAdditionLogText(PersonalLog log)
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

        public string BuildAccountPhoneNumberChangeLogText(PersonalLog log)
        {
            string verb = "changed";

            if (!log.Data.ContainsKey("old_phone_number") && log.Data.ContainsKey("new_phone_number"))
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

        public string BuildAccountPhoneNumberRemovalLogText(PersonalLog log)
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

        public string BuildAccountProfilePictureChangeLogText(PersonalLog log)
        {
            string text = $"I have changed the profile picture of the {log.Data["platform"]} account";
            string discriminator = GetDiscriminator(log.Data);

            if (!string.IsNullOrWhiteSpace(discriminator))
            {
                text += $" ({discriminator})";
            }

            return text;
        }

        public string BuildAccountRecoveryLogText(PersonalLog log)
        {
            string text = $"I have recovered the {log.Data["platform"]} account";
            string discriminator = GetDiscriminator(log.Data);

            if (!string.IsNullOrWhiteSpace(discriminator))
            {
                text += $" ({discriminator})";
            }

            return text;
        }

        public string BuildAccountRecoveryEmailAddressChangeLogText(PersonalLog log)
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

        public string BuildAccountRegistrationLogText(PersonalLog log)
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

            if (log.Data.TryGetValue("personal_name", out string personalName))
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

            if (log.Data.TryGetValue("request_id", out string requestId))
            {
                text += $" with the {requestId} identification code";
            }

            text += $" to register the {log.Data["platform"]} account";
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

            if (log.Data.TryGetValue("personal_name", out string personalName))
            {
                if (withCount > 0)
                {
                    text += ", and";
                }

                text += $" with the personal name {personalName}";
                withCount += 1;
            }

            return text;;
        }

        public string BuildAccountRegistrationRequestFulfillmentLogText(PersonalLog log)
        {
            string text = $"The account registration request for the {log.Data["platform"]} account";
            string discriminator = GetDiscriminator(log.Data);

            if (!string.IsNullOrWhiteSpace(discriminator))
            {
                text += $" ({discriminator})";
            }

            if (log.Data.TryGetValue("request_id", out string requestId))
            {
                text += $" with the {requestId} identification code";
            }

            if (log.Data.TryGetValue("request_date", out string requestDate))
            {
                text += $", sent on {requestDate},";
            }

            text += " has been fulfilled";

            return text;
        }

        public string BuildAccountSubscriptionPurchaseLogText(PersonalLog log)
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

        public string BuildAccountUnlinkingLogText(PersonalLog log)
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

        public string BuildAccountUsernameChangeLogText(PersonalLog log)
        {
            string verb = "changed";

            if (!log.Data.ContainsKey("old_username"))
            {
                verb = "set";
            }

            string text = $"I have {verb} the username of the {log.Data["platform"]} account";
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

        public string BuildAccountVisibilityMadePrivateLogText(PersonalLog log)
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

        public string BuildAccountVisibilityMadePublicLogText(PersonalLog log)
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

        public string BuildBloodDonationLogText(PersonalLog log)
        {
            string text = $"I have donated blood";

            if (log.Data.TryGetValue("donation_centre_name", out string donationCentreName))
            {
                text += $" at {donationCentreName}";
            }

            if (log.Data.TryGetValue("donation_code", out string donationCode))
            {
                text += $". The donation code was: {donationCode}";
            }

            return text;
        }

        public string BuildBloodGlucoseMeasurementLogText(PersonalLog log)
        {
            log.Data.TryGetValue("unit", out string unit);

            if (string.IsNullOrWhiteSpace(unit))
            {
                unit = "mg/dL";
            }

            string text = $"My blood glucose level measured {log.Data["glucose_level"]} {unit}";

            return text;
        }

        public string BuildBodyWaterRateMeasurementLogText(PersonalLog log)
        {
            decimal bodyWaterRate = decimal.Parse(log.Data["body_water_rate"]);

            return $"My body water rate measured {bodyWaterRate:F2}%";
        }

        public string BuildBodyWeightMeasurementLogText(PersonalLog log)
        {
            log.Data.TryGetValue("unit", out string unit);

            if (string.IsNullOrWhiteSpace(unit))
            {
                unit = "kg";
            }

            string text = $"My body weight measured {log.Data["body_weight"]} {unit}";

            if (log.Data.TryGetValue("scale_name", out string scaleName))
            {
                text += $" on the scale {scaleName}";
            }

            return text;
        }

        public string BuildChatGroupCreationLogText(PersonalLog log)
        {
            string text = $"I have created a chat group named {log.Data["group_name"]}";

            if (log.Data.TryGetValue("platform", out string platform))
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

        public string BuildChatGroupDeletionLogText(PersonalLog log)
        {
            string text = $"I have deleted the chat group named {log.Data["group_name"]}";

            if (log.Data.TryGetValue("platform", out string platform))
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

        public string BuildChatGroupJoiningLogText(PersonalLog log)
        {
            string text = $"I have joined the chat group named {log.Data["group_name"]}";

            if (log.Data.TryGetValue("platform", out string platform))
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

        public string BuildChatGroupLeavingLogText(PersonalLog log)
        {
            string text = $"I have left the chat group named {log.Data["group_name"]}";

            if (log.Data.TryGetValue("platform", out string platform))
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

        public string BuildDatingAppMatchLogText(PersonalLog log)
        {
            string text = $"I have matched with {log.Data["match_name"]} on {log.Data["platform"]}";
            string discriminator = GetDiscriminator(log.Data);

            if (!string.IsNullOrWhiteSpace(discriminator))
            {
                text += $" ({discriminator})";
            }

            return text;
        }

        public string BuildDeliveryReceivalLogText(PersonalLog log)
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

        public string BuildDentalScalingLogText(PersonalLog log)
        {
            string text = $"I have undergone a dental scaling procedure";

            if (log.Data.TryGetValue("clinic_name", out string clinicName))
            {
                text += $" at {clinicName}";
            }

            return text;
        }

        public string BuildDirectBilirubinMeasurementLogText(PersonalLog log)
        {
            log.Data.TryGetValue("unit", out string unit);

            if (string.IsNullOrWhiteSpace(unit))
            {
                unit = "mg/dL";
            }

            return $"My direct bilirubin level measured {log.Data["direct_bilirubin_level"]} {unit}";
        }

        public string BuildEmailExportLogText(PersonalLog log)
        {
            string text = $"I have exported all of the emails from the {log.Data["platform"]} account";

            if (log.Data.TryGetValue("account", out string account))
            {
                text += $" ({account})";
            }

            return text;
        }

        public string BuildEyeCheckupLogText(PersonalLog log)
        {
            string text = $"I have undergone an eye checkup";

            if (log.Data.TryGetValue("clinic_name", out string clinicName))
            {
                text += $" at {clinicName}";
            }

            if (log.Data.TryGetValue("optometrist_name", out string optometristName))
            {
                text += $", by {optometristName}";
            }

            return text;
        }

        public string BuildGameAchievementUnlockLogText(PersonalLog log)
        {
            string achievementAction = "unlocked";
            string achievementType = "achievement";
            string game = log.Data["game_name"];
            log.Data.TryGetValue("platform", out string platform);

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

        public string BuildGameConstructionLogText(PersonalLog log)
        {
            string text = $"I have built {log.Data["construction_name"]} in the game {log.Data["game_name"]}";

            if (log.Data.TryGetValue("platform", out string platform))
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

        public string BuildGameConstructionBeginningLogText(PersonalLog log)
        {
            string text = $"I have begun the construction of {log.Data["construction_name"]} in the game {log.Data["game_name"]}";

            if (log.Data.TryGetValue("platform", out string platform))
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

        public string BuildGameConstructionCompletionLogText(PersonalLog log)
        {
            string text = $"I have completed the construction of {log.Data["construction_name"]} in the game {log.Data["game_name"]}";

            if (log.Data.TryGetValue("platform", out string platform))
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

        public string BuildGameRankUpLogText(PersonalLog log)
        {
            string text = $"I have ranked up to {log.Data["new_rank"]} in {log.Data["game_name"]}";

            if (log.Data.TryGetValue("platform", out string platform))
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

        public string BuildGameLevelUpLogText(PersonalLog log)
        {
            string text = $"I have reached level {log.Data["new_level"]} in {log.Data["game_name"]}";

            if (log.Data.TryGetValue("platform", out string platform))
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

        public string BuildGettingInToBedLogText(PersonalLog log)
            => $"I have gotten in to bed";

        public string BuildGettingOutOfBedLogText(PersonalLog log)
            => $"I have gotten out of bed";

        public string BuildHairCuttingLogText(PersonalLog log)
        {
            string text = $"I have gotten my hair cut";

            if (log.Data.TryGetValue("salon_name", out string salonName))
            {
                text += $" at {salonName}";
            }

            if (log.Data.TryGetValue("hairdresser_name", out string hairdresserName))
            {
                text += $", by {hairdresserName}";
            }

            return text;
        }

        public string BuildHdlCholesterolMeasurementLogText(PersonalLog log)
        {
            log.Data.TryGetValue("unit", out string unit);

            if (string.IsNullOrWhiteSpace(unit))
            {
                unit = "mg/dL";
            }

            string text = $"My HDL cholesterol level measured {log.Data["hdl_cholesterol_level"]} {unit}";

            return text;
        }

        public string BuildIndirectBilirubinMeasurementLogText(PersonalLog log)
        {
            log.Data.TryGetValue("unit", out string unit);

            if (string.IsNullOrWhiteSpace(unit))
            {
                unit = "mg/dL";
            }

            return $"My indirect bilirubin level measured {log.Data["indirect_bilirubin_level"]} {unit}";
        }

        public string BuildInternshipApplicationSubmissionLogText(PersonalLog log)
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

        public string BuildLdlCholesterolMeasurementLogText(PersonalLog log)
        {
            log.Data.TryGetValue("unit", out string unit);

            if (string.IsNullOrWhiteSpace(unit))
            {
                unit = "mg/dL";
            }

            return $"My LDL cholesterol level measured {log.Data["ldl_cholesterol_level"]} {unit}";
        }

        public string BuildMealVoucherCardCreditationLogText(PersonalLog log)
        {
            string text = $"My meal voucher card was credited with {log.Data["amount"]} {log.Data["currency"]}";

            return text;
        }

        public string BuildObjectSaleLogText(PersonalLog log)
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

        public string BuildOnlineReviewSubmissionLogText(PersonalLog log)
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

        public string BuildOnlineStorePurchaseLogText(PersonalLog log)
        {
            string text = $"I have purchased {log.Data["product_name"]} from {log.Data["platform"]}";
            string discriminator = GetDiscriminator(log.Data);

            if (!string.IsNullOrWhiteSpace(discriminator))
            {
                text += $" ({discriminator})";
            }

            if (log.Data.TryGetValue("price_amount", out string priceAmount))
            {
                text += $", for {priceAmount} {log.Data["price_currency"]}";
            }

            return text;
        }

        public string BuildPetWeightMeasurementLogText(PersonalLog log)
        {
            log.Data.TryGetValue("unit", out string unit);

            if (string.IsNullOrWhiteSpace(unit))
            {
                unit = "kg";
            }

            string text = $"The weight of my pet {log.Data["pet_name"]} measured {log.Data["pet_weight"]} {unit}";

            if (log.Data.TryGetValue("scale_name", out string scaleName))
            {
                text += $" on the scale {scaleName}";
            }

            return text;
        }

        public string BuildShowerTakingLogText(PersonalLog log)
        {
            string text = $"I have taken a shower";

            if (log.Data.TryGetValue("duration_minutes", out string durationMinutes))
            {
                text += $" for {durationMinutes} minutes";
            }

            return text;
        }

        public string BuildSwimmingActivityLogText(PersonalLog log)
        {
            string text = $"I have gone swimming";

            if (log.Data.TryGetValue("location", out string location))
            {
                text += $" at {location}";
            }

            return text;
        }

        public string BuildTeethBrushingLogText(PersonalLog log)
        {
            string text = $"I have brushed my teeth";

            if (log.Data.TryGetValue("duration_minutes", out string durationMinutes))
            {
                text += $" for {durationMinutes} minutes";
            }

            return text;
        }

        public string BuildTollPaymentLogText(PersonalLog log)
        {
            string text = $"I have paid a toll";

            if (log.Data.TryGetValue("provider_name", out string providerName))
            {
                text += $" to {providerName}";
            }

            if (log.Data.TryGetValue("toll_location", out string tollLocation))
            {
                text += $" at {tollLocation}";
            }

            if (log.Data.TryGetValue("vehicle_registration_number", out string vehicleRegistrationNumber))
            {
                text += $" for the vehicle with the registration number {vehicleRegistrationNumber}";
            }

            if (log.Data.TryGetValue("cost_amount", out string costAmount))
            {
                text += $", amounting to {costAmount} {log.Data["cost_currency"]}";
            }

            return text;
        }

        public string BuildTotalBilirubinMeasurementLogText(PersonalLog log)
        {
            log.Data.TryGetValue("unit", out string unit);

            if (string.IsNullOrWhiteSpace(unit))
            {
                unit = "mg/dL";
            }

            return $"My total bilirubin level measured {log.Data["total_bilirubin_level"]} {unit}";
        }

        public string BuildTotalCholesterolMeasurementLogText(PersonalLog log)
        {
            string unit = "mg/dL";

            if (log.Data.TryGetValue("unit", out string unitValue))
            {
                unit = unitValue;
            }

            return $"My total cholesterol level measured {log.Data["total_cholesterol_level"]} {unit}";
        }

        public string BuildUtilityBillPaymentLogText(PersonalLog log)
        {
            log.Data.TryGetValue("utility_type", out string utilityType);

            if (string.IsNullOrWhiteSpace(utilityType))
            {
                utilityType = "utility";
            }

            string text = $"I have paid my {utilityType} bill to {log.Data["provider_name"]}";
            string discriminator = GetDiscriminator(log.Data);

            if (!string.IsNullOrWhiteSpace(discriminator))
            {
                text += $" ({discriminator})";
            }

            if (log.Data.TryGetValue("supply_point_number", out string supplyPointNumber))
            {
                text += $" for the {supplyPointNumber} supply point number";
            }
            else if (log.Data.TryGetValue("location", out string location))
            {
                text += $" at {location}";
            }

            if (log.Data.TryGetValue("cost_amount", out string costAmount))
            {
                text += $", amounting to {costAmount} {log.Data["cost_currency"]}";
            }

            return text;
        }

        public string BuildUtilityIndexMeasurementLogText(PersonalLog log)
        {
            log.Data.TryGetValue("utility_type", out string utilityType);

            if (string.IsNullOrWhiteSpace(utilityType))
            {
                utilityType = "utility";
            }

            string text = $"I have measured the {utilityType} index";

            if (log.Data.TryGetValue("supply_point_number", out string supplyPointNumber))
            {
                text += $" for the {supplyPointNumber} supply point number";
            }
            else if (log.Data.TryGetValue("location", out string location))
            {
                text += $" at {location}";
            }

            if (log.Data.TryGetValue("index_value", out string indexValue))
            {
                text += $", obtaining a value of {indexValue}";
            }

            return text;
        }

        public string BuildVideoUploadLogText(PersonalLog log)
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

        public string BuildWakingUpLogText(PersonalLog log)
            => $"I have woken up";

        public string BuildWeddingAttendanceLogText(PersonalLog log)
        {
            string text = $"I have attended the wedding of";

            log.Data.TryGetValue("bride_name", out string brideName);
            log.Data.TryGetValue("groom_name", out string groomName);

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

            if (log.Data.TryGetValue("location", out string location))
            {
                text += $" in {location}";
            }

            if (log.Data.TryGetValue("venue_name", out string venueName))
            {
                text += $" at {venueName}";
            }

            return text;
        }

        public string BuildWorkFromTheOfficeLogText(PersonalLog log)
        {
            string text = $"I have worked from the office";

            if (log.Data.TryGetValue("office_name", out string officeName))
            {
                text += $" ({officeName})";
            }

            return text;
        }
    }
}
