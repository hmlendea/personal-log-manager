using PersonalLogManager.Service.Models;

namespace PersonalLogManager.Service.TextBuilding.Localisation
{
    public class RomanianTextBuilder() : PersonalLogTextBuilderBase, IPersonalLogTextBuilder
    {
        public string BuildAccountActivationLogText(PersonalLog log)
        {
            string text = $"Am activat contul de {log.Data["platform"]}";
            string discriminator = GetDiscriminator(log.Data);

            if (!string.IsNullOrWhiteSpace(discriminator))
            {
                text += $" ({discriminator})";
            }

            return text;
        }

        public string BuildAccountBanningLogText(PersonalLog log)
        {
            string text = $"Contul de {log.Data["platform"]}";
            string discriminator = GetDiscriminator(log.Data);

            if (!string.IsNullOrWhiteSpace(discriminator))
            {
                text += $" ({discriminator})";
            }

            text += " a fost banat";

            if (log.Data.TryGetValue("ban_reason", out string banReason))
            {
                text += $" pentru următorul motiv: {banReason}";
            }

            return text;
        }

        public string BuildAccountContactEmailAddressChangeLogText(PersonalLog log)
        {
            string verb = "schimbat";

            if (!log.Data.ContainsKey("old_contact_email_address") && log.Data.ContainsKey("new_contact_email_address"))
            {
                verb = "setat";
            }

            string text = $"Am {verb} adresa de e-mail de contact a contului de {log.Data["platform"]}";
            string discriminator = GetDiscriminator(log.Data);

            if (!string.IsNullOrWhiteSpace(discriminator))
            {
                text += $" ({discriminator})";
            }

            if (log.Data.TryGetValue("old_contact_email_address", out string oldContactEmailAddress))
            {
                text += $" de la {oldContactEmailAddress}";
            }

            if (log.Data.TryGetValue("new_contact_email_address", out string newContactEmailAddress))
            {
                text += $" la {newContactEmailAddress}";
            }

            return text;
        }

        public string BuildAccountDataExportLogText(PersonalLog log)
        {
            string text = $"Am exportat datele contului de {log.Data["platform"]}";
            string discriminator = GetDiscriminator(log.Data);

            if (!string.IsNullOrWhiteSpace(discriminator))
            {
                text += $" ({discriminator})";
            }

            return text;
        }

        public string BuildAccountDataExportRequestLogText(PersonalLog log)
        {
            string text = $"Am trimis o solicitare a unui export al datelor contului de {log.Data["platform"]}";
            string discriminator = GetDiscriminator(log.Data);

            if (!string.IsNullOrWhiteSpace(discriminator))
            {
                text += $" ({discriminator})";
            }

            if (log.Data.TryGetValue("request_id", out string requestId))
            {
                text += $" cu codul de identificare {requestId}";
            }

            if (log.Data.TryGetValue("request_method", out string requestMethod))
            {
                text += $", prin {requestMethod}";
            }

            return text;
        }

        public string BuildAccountDataExportRequestFulfillmentLogText(PersonalLog log)
        {
            string text = $"Solicitarea exportului de date ale contului de {log.Data["platform"]}";

            string discriminator = GetDiscriminator(log.Data);

            if (!string.IsNullOrWhiteSpace(discriminator))
            {
                text += $" ({discriminator})";
            }

            if (log.Data.TryGetValue("request_id", out string requestId))
            {
                text += $" cu codul de identificare {requestId}";
            }

            if (log.Data.TryGetValue("request_date", out string requestDate))
            {
                text += $", trimisă pe {requestDate},";
            }

            text += " a fost îndeplinită";

            return text;
        }

        public string BuildAccountDataExportSaveLogText(PersonalLog log)
        {
            string text = $"Am salvat exportul datelor contului de {log.Data["platform"]}";
            string discriminator = GetDiscriminator(log.Data);

            if (!string.IsNullOrWhiteSpace(discriminator))
            {
                text += $" ({discriminator})";
            }

            log.Data.TryGetValue("request_date", out string requestDate);
            log.Data.TryGetValue("request_id", out string requestId);

            if (!string.IsNullOrWhiteSpace(requestDate) && !string.IsNullOrWhiteSpace(requestId))
            {
                text += $", obținut în urma solicitarii cu codul de identificare {requestId} din {requestDate}";
            }
            else if (!string.IsNullOrWhiteSpace(requestDate) && string.IsNullOrWhiteSpace(requestId))
            {
                text += $", obținut în urma solicitarii trimise pe {requestDate}";
            }
            else if (string.IsNullOrWhiteSpace(requestDate) && !string.IsNullOrWhiteSpace(requestId))
            {
                text += $", obținut în urma solicitarii cu codul de identificare {requestId}";
            }

            return text;
        }

        public string BuildAccountDataObfuscationLogText(PersonalLog log)
        {
            string text = $"Am ofuscat datele contului de {log.Data["platform"]}";
            string discriminator = GetDiscriminator(log.Data);

            if (!string.IsNullOrWhiteSpace(discriminator))
            {
                text += $" ({discriminator})";
            }

            return text;
        }

        public string BuildAccountDeactivationLogText(PersonalLog log)
        {
            string text = $"Am dezactivat contul de {log.Data["platform"]}";
            string discriminator = GetDiscriminator(log.Data);

            if (!string.IsNullOrWhiteSpace(discriminator))
            {
                text += $" ({discriminator})";
            }

            return text;
        }

        public string BuildAccountDeletionLogText(PersonalLog log)
        {
            string text = $"Am șters contul de {log.Data["platform"]}";
            string discriminator = GetDiscriminator(log.Data);

            if (!string.IsNullOrWhiteSpace(discriminator))
            {
                text += $" ({discriminator})";
            }

            return text;
        }

        public string BuildAccountDeletionRequestLogText(PersonalLog log)
        {
            string text = $"Am trimis o solicitare de ștergere a contului de {log.Data["platform"]}";
            string discriminator = GetDiscriminator(log.Data);

            if (!string.IsNullOrWhiteSpace(discriminator))
            {
                text += $" ({discriminator})";
            }

            if (log.Data.TryGetValue("request_id", out string requestId))
            {
                text += $" cu codul de identificare {requestId}";
            }

            if (log.Data.TryGetValue("request_method", out string requestMethod))
            {
                text += $" prin {requestMethod}";
            }

            return text;
        }

        public string BuildAccountDeletionRequestCancellationLogText(PersonalLog log)
        {
            string text = $"Am anulat solicitarea ștergerii contului de {log.Data["platform"]}";
            string discriminator = GetDiscriminator(log.Data);

            if (!string.IsNullOrWhiteSpace(discriminator))
            {
                text += $" ({discriminator})";
            }

            if (log.Data.TryGetValue("request_id", out string requestId))
            {
                text += $" cu codul de identificare {requestId}";
            }

            if (log.Data.TryGetValue("request_date", out string requestDate))
            {
                text += $", trimisă pe {requestDate}";
            }

            return text;
        }

        public string BuildAccountDeletionRequestFulfillmentLogText(PersonalLog log)
        {
            string text = $"Cererea de ștergere a contului de {log.Data["platform"]}";
            string discriminator = GetDiscriminator(log.Data);

            if (!string.IsNullOrWhiteSpace(discriminator))
            {
                text += $" ({discriminator})";
            }

            if (log.Data.TryGetValue("request_id", out string requestId))
            {
                text += $" cu codul de identificare {requestId}";
            }

            if (log.Data.TryGetValue("request_date", out string requestDate))
            {
                text += $", trimisă pe {requestDate},";
            }

            text += " a fost îndeplinită";

            return text;
        }

        public string BuildAccountDeletionRequestRejectionLogText(PersonalLog log)
        {
            string text = $"Cererea de ștergere a contului de {log.Data["platform"]}";
            string discriminator = GetDiscriminator(log.Data);

            if (!string.IsNullOrWhiteSpace(discriminator))
            {
                text += $" ({discriminator})";
            }

            if (log.Data.TryGetValue("request_id", out string requestId))
            {
                text += $" cu codul de identificare {requestId}";
            }

            if (log.Data.TryGetValue("request_date", out string requestDate))
            {
                text += $", trimisă pe {requestDate},";
            }

            text += " a fost respinsă";

            return text;
        }

        public string BuildAccountDeletionValidationLogText(PersonalLog log)
        {
            string text = $"Am verificat că a fost șters contul de {log.Data["platform"]}";
            string discriminator = GetDiscriminator(log.Data);

            if (!string.IsNullOrWhiteSpace(discriminator))
            {
                text += $" ({discriminator})";
            }

            if (log.Data.TryGetValue("request_date", out string requestDate))
            {
                text += $", în urma solicitării de ștergere din {requestDate}";
            }

            return text;
        }

        public string BuildAccountEmailAddressChangeLogText(PersonalLog log)
        {
            string verb = "schimbat";

            if (!log.Data.ContainsKey("old_email_address") && log.Data.ContainsKey("new_email_address"))
            {
                verb = "setat";
            }

            string text = $"Am {verb} adresa de e-mail a contului de {log.Data["platform"]}";
            string discriminator = GetDiscriminator(log.Data);

            if (!string.IsNullOrWhiteSpace(discriminator))
            {
                text += $" ({discriminator})";
            }

            if (log.Data.TryGetValue("old_email_address", out string oldEmailAddress))
            {
                text += $" din {oldEmailAddress}";
            }

            if (log.Data.TryGetValue("new_email_address", out string newEmailAddress))
            {
                text += $" în {newEmailAddress}";
            }

            return text;
        }

        public string BuildAccountEmailAddressChangeRequestLogText(PersonalLog log)
        {
            string text = $"Am trimis o solicitare de schimbare a adresei de e-mail a contului de {log.Data["platform"]}";
            string discriminator = GetDiscriminator(log.Data);

            if (!string.IsNullOrWhiteSpace(discriminator))
            {
                text += $" ({discriminator})";
            }

            if (log.Data.TryGetValue("old_email_address", out string oldEmailAddress))
            {
                text += $" din {oldEmailAddress}";
            }

            if (log.Data.TryGetValue("new_email_address", out string newEmailAddress))
            {
                text += $" în {newEmailAddress}";
            }

            if (log.Data.TryGetValue("request_id", out string requestId))
            {
                text += $", cu codul de identificare {requestId}";
            }

            if (log.Data.TryGetValue("request_method", out string requestMethod))
            {
                text += $", prin {requestMethod}";
            }

            return text;
        }

        public string BuildAccountEmailAddressChangeRequestFulfillmentLogText(PersonalLog log)
        {
            string text = $"Solicitarea de schimbare a adresei de e-mail a contului de {log.Data["platform"]}";

            string discriminator = GetDiscriminator(log.Data);

            if (!string.IsNullOrWhiteSpace(discriminator))
            {
                text += $" ({discriminator})";
            }

            if (log.Data.TryGetValue("old_email_address", out string oldEmailAddress))
            {
                text += $" de la {oldEmailAddress}";
            }

            if (log.Data.TryGetValue("new_email_address", out string newEmailAddress))
            {
                text += $" la {newEmailAddress}";
            }

            if (log.Data.TryGetValue("request_id", out string requestId))
            {
                text += $", cu codul de identificare {requestId}";
            }

            if (log.Data.TryGetValue("request_date", out string requestDate))
            {
                text += $", trimisă pe {requestDate},";
            }

            text += " a fost îndeplinită";

            return text;
        }

        public string BuildAccountEmailAddressConfirmationLogText(PersonalLog log)
        {
            string text = $"Am confirmat adresa de e-mail";

            if (log.Data.TryGetValue("email_address", out string emailAddress))
            {
                text += $" ({emailAddress})";
            }

            text += $" a contului de {log.Data["platform"]}";

            string discriminator = GetDiscriminator(log.Data);

            if (!string.IsNullOrWhiteSpace(discriminator))
            {
                text += $" ({discriminator})";
            }

            return text;
        }

        public string BuildAccountFeatureEnablementLogText(PersonalLog log)
        {
            string text = $"Am activat funcția {log.Data["feature_name"]} pe contul de {log.Data["platform"]}";
            string discriminator = GetDiscriminator(log.Data);

            if (!string.IsNullOrWhiteSpace(discriminator))
            {
                text += $" ({discriminator})";
            }

            return text;
        }

        public string BuildAccountFeatureDisablementLogText(PersonalLog log)
        {
            string text = $"Am dezactivat funcția de {log.Data["feature_name"]} pe contul de {log.Data["platform"]}";
            string discriminator = GetDiscriminator(log.Data);

            if (!string.IsNullOrWhiteSpace(discriminator))
            {
                text += $" ({discriminator})";
            }

            return text;
        }

        public string BuildAccountFriendshipRequestReceivalLogText(PersonalLog log)
        {
            string text = $"Am primit o cerere de prietenie pe contul de {log.Data["platform"]}";
            string discriminator = GetDiscriminator(log.Data);

            if (!string.IsNullOrWhiteSpace(discriminator))
            {
                text += $" ({discriminator})";
            }

            if (log.Data.TryGetValue("from_account", out string fromAccount))
            {
                text += $" de la {fromAccount}";
            }

            return text;
        }

        public string BuildAccountIdentityVerificationLogText(PersonalLog log)
        {
            string text = $"Am verificat identitatea pentru contul de {log.Data["platform"]}";
            string discriminator = GetDiscriminator(log.Data);

            if (!string.IsNullOrWhiteSpace(discriminator))
            {
                text += $" ({discriminator})";
            }

            return text;
        }

        public string BuildAccountLinkingLogText(PersonalLog log)
        {
            string text = $"Am conectat contul de {log.Data["platform"]}";
            string discriminator = GetDiscriminator(log.Data);

            if (!string.IsNullOrWhiteSpace(discriminator))
            {
                text += $" ({discriminator})";
            }

            text += $" cu";

            if (log.Data.TryGetValue("platform_linked", out string platformLinked))
            {
                if (log.Data.TryGetValue("account_linked", out string accountLinked))
                {
                    text += $" contul de {platformLinked} ({accountLinked})";
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
            string text = $"Am șters toate mesajele trimise de pe contul de {log.Data["platform"]}";
            string discriminator = GetDiscriminator(log.Data);

            if (!string.IsNullOrWhiteSpace(discriminator))
            {
                text += $" ({discriminator})";
            }

            return text;
        }

        public string BuildAccountPasswordChangeLogText(PersonalLog log)
        {
            string text = $"Am schimbat parola contului de {log.Data["platform"]}";
            string discriminator = GetDiscriminator(log.Data);

            if (!string.IsNullOrWhiteSpace(discriminator))
            {
                text += $" ({discriminator})";
            }

            return text;
        }

        public string BuildAccountPersonalNameChangeLogText(PersonalLog log)
        {
            string text = $"Am schimbat numele personal al contului de {log.Data["platform"]}";
            string discriminator = GetDiscriminator(log.Data);

            if (!string.IsNullOrWhiteSpace(discriminator))
            {
                text += $" ({discriminator})";
            }

            if (log.Data.TryGetValue("old_personal_name", out string oldPersonalName))
            {
                text += $" de la {oldPersonalName}";
            }

            if (log.Data.TryGetValue("new_personal_name", out string newPersonalName))
            {
                text += $" la {newPersonalName}";
            }

            return text;
        }

        public string BuildAccountPhoneNumberAdditionLogText(PersonalLog log)
        {
            string text = $"Am adăugat un număr de telefon la contului de {log.Data["platform"]}";
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
            string verb = "schimbat";

            if (!log.Data.ContainsKey("old_phone_number") && log.Data.ContainsKey("new_phone_number"))
            {
                verb = "setat";
            }

            string text = $"Am {verb} numărul de telefon al contului de {log.Data["platform"]}";
            string discriminator = GetDiscriminator(log.Data);

            if (!string.IsNullOrWhiteSpace(discriminator))
            {
                text += $" ({discriminator})";
            }

            if (log.Data.TryGetValue("old_phone_number", out string oldPhoneNumber))
            {
                text += $" de la {oldPhoneNumber}";
            }

            if (log.Data.TryGetValue("new_phone_number", out string newPhoneNumber))
            {
                text += $" la {newPhoneNumber}";
            }

            return text;
        }

        public string BuildAccountPhoneNumberRemovalLogText(PersonalLog log)
        {
            string text = $"Am șters un număr de telefon de la contul de {log.Data["platform"]}";
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
            string text = $"Am schimbat poza de profil de pe contul de {log.Data["platform"]}";
            string discriminator = GetDiscriminator(log.Data);

            if (!string.IsNullOrWhiteSpace(discriminator))
            {
                text += $" ({discriminator})";
            }

            return text;
        }

        public string BuildAccountRecoveryLogText(PersonalLog log)
        {
            string text = $"Am recuperat contul de {log.Data["platform"]}";
            string discriminator = GetDiscriminator(log.Data);

            if (!string.IsNullOrWhiteSpace(discriminator))
            {
                text += $" ({discriminator})";
            }

            return text;
        }

        public string BuildAccountRecoveryEmailAddressChangeLogText(PersonalLog log)
        {
            string verb = "schimbat";

            if (!log.Data.ContainsKey("old_email_address") && log.Data.ContainsKey("new_email_address"))
            {
                verb = "setat";
            }

            string text = $"Am {verb} adresa de e-mail de recuperare a contului de {log.Data["platform"]}";
            string discriminator = GetDiscriminator(log.Data);

            if (!string.IsNullOrWhiteSpace(discriminator))
            {
                text += $" ({discriminator})";
            }

            if (log.Data.TryGetValue("old_recovery_email_address", out string oldEmailAddress))
            {
                text += $" de la {oldEmailAddress}";
            }

            if (log.Data.TryGetValue("new_recovery_email_address", out string newEmailAddress))
            {
                text += $" la {newEmailAddress}";
            }

            return text;
        }

        public string BuildAccountRegistrationLogText(PersonalLog log)
        {
            string text = $"Am înregistrat contul de {log.Data["platform"]}";
            string discriminator = GetDiscriminator(log.Data);

            if (!string.IsNullOrWhiteSpace(discriminator))
            {
                text += $" ({discriminator})";
            }

            int withCount = 0;

            if (log.Data.TryGetValue("username", out string username))
            {
                text += $" cu numele de utilizator {username}";
                withCount += 1;
            }

            if (log.Data.TryGetValue("phone_number", out string phoneNumber))
            {
                if (withCount > 0)
                {
                    text += ", și";
                }

                text += $" cu numărul de telefon {phoneNumber}";
                withCount += 1;
            }

            if (log.Data.TryGetValue("email_address", out string emailAddress))
            {
                if (withCount > 0)
                {
                    text += ", și";
                }

                text += $" cu adresa de e-mail {emailAddress}";
                withCount += 1;
            }

            if (log.Data.TryGetValue("personal_name", out string personalName))
            {
                if (withCount > 0)
                {
                    text += ", și";
                }

                text += $" cu numele personal {personalName}";
                withCount += 1;
            }

            return text;
        }

        public string BuildAccountRegistrationRequestLogText(PersonalLog log)
        {
            string text = $"Am trimis o solicitare de înregistrare";
            log.Data.TryGetValue("request_id", out string requestId);

            if (!string.IsNullOrWhiteSpace(requestId))
            {
                text += $" cu codul de identificare {requestId}";
            }

            text += $" a contului de {log.Data["platform"]}";
            string discriminator = GetDiscriminator(log.Data);

            if (!string.IsNullOrWhiteSpace(discriminator))
            {
                text += $" ({discriminator})";
            }

            int withCount = 0;

            if (log.Data.TryGetValue("username", out string username))
            {
                text += $" cu numele de utilizator {username}";
                withCount += 1;
            }

            if (log.Data.TryGetValue("phone_number", out string phoneNumber))
            {
                if (withCount > 0)
                {
                    text += ", și";
                }

                text += $" cu numărul de telefon {phoneNumber}";
                withCount += 1;
            }

            if (log.Data.TryGetValue("email_address", out string emailAddress))
            {
                if (withCount > 0)
                {
                    text += ", și";
                }

                text += $" cu adresa de e-mail {emailAddress}";
                withCount += 1;
            }

            if (log.Data.TryGetValue("personal_name", out string personalName))
            {
                if (withCount > 0)
                {
                    text += ", și";
                }

                text += $" cu numele personal {personalName}";
                withCount += 1;
            }

            return text;;
        }

        public string BuildAccountRegistrationRequestFulfillmentLogText(PersonalLog log)
        {
            string text = $"Solicitarea înregistrării contului de {log.Data["platform"]}";
            string discriminator = GetDiscriminator(log.Data);

            if (!string.IsNullOrWhiteSpace(discriminator))
            {
                text += $" ({discriminator})";
            }

            if (log.Data.TryGetValue("request_id", out string requestId))
            {
                text += $" cu codul de identificare {requestId}";
            }

            if (log.Data.TryGetValue("request_date", out string requestDate))
            {
                text += $", trimisă pe {requestDate},";
            }

            text += " a fost îndeplinită";

            return text;
        }

        public string BuildAccountSecurityQuestionsChangeLogText(PersonalLog log)
        {
            string text = $"Am schimbat întrebările de securitate ale contului de {log.Data["platform"]}";
            string discriminator = GetDiscriminator(log.Data);

            if (!string.IsNullOrWhiteSpace(discriminator))
            {
                text += $" ({discriminator})";
            }

            return text;
        }

        public string BuildAccountSubscriptionPurchaseLogText(PersonalLog log)
        {
            string text = $"Am cumpărat un abonament";

            if (log.Data.TryGetValue("subscription_name", out string subscriptionName))
            {
                text += $" {subscriptionName}";
            }

            text += $" pentru contul de {log.Data["platform"]}";
            string discriminator = GetDiscriminator(log.Data);

            if (!string.IsNullOrWhiteSpace(discriminator))
            {
                text += $" ({discriminator})";
            }

            if (log.Data.TryGetValue("price_amount", out string priceAmount))
            {
                text += $" pentru {priceAmount} {log.Data["price_currency"]}";
            }

            return text;
        }

        public string BuildAccountUnlinkingLogText(PersonalLog log)
        {
            string text = $"Am deconectat contul de {log.Data["platform"]}";
            string discriminator = GetDiscriminator(log.Data);

            if (!string.IsNullOrWhiteSpace(discriminator))
            {
                text += $" ({discriminator})";
            }

            text += $" de contul de {log.Data["platform_unlinked"]}";

            if (log.Data.TryGetValue("account_unlinked", out string accountLinked))
            {
                text += $" ({accountLinked})";
            }

            return text;
        }

        public string BuildAccountUsernameChangeLogText(PersonalLog log)
        {
            string verb = "schimbat";

            if (!log.Data.ContainsKey("old_username"))
            {
                verb = "setat";
            }

            string text = $"Am {verb} numele de utilizator al contului de {log.Data["platform"]}";
            string discriminator = GetDiscriminator(log.Data);

            if (!string.IsNullOrWhiteSpace(discriminator))
            {
                text += $" ({discriminator})";
            }

            if (log.Data.TryGetValue("old_username", out string oldUsername))
            {
                text += $" de la {oldUsername}";
            }

            if (log.Data.TryGetValue("new_username", out string newUsername))
            {
                text += $" la {newUsername}";
            }

            return text;
        }

        public string BuildAccountVisibilityMadePrivateLogText(PersonalLog log)
        {
            string text = $"Am făcut privat contul de {log.Data["platform"]}";
            string discriminator = GetDiscriminator(log.Data);

            if (!string.IsNullOrWhiteSpace(discriminator))
            {
                text += $" ({discriminator})";
            }

            return text;
        }

        public string BuildAccountVisibilityMadePublicLogText(PersonalLog log)
        {
            string text = $"Am făcut public contul de {log.Data["platform"]}";
            string discriminator = GetDiscriminator(log.Data);

            if (!string.IsNullOrWhiteSpace(discriminator))
            {
                text += $" ({discriminator})";
            }

            return text;
        }

        public string BuildBloodDonationLogText(PersonalLog log)
        {
            string text = $"Am donat sânge";

            if (log.Data.TryGetValue("donation_centre_name", out string donationCentreName))
            {
                text += $" la {donationCentreName}";
            }

            if (log.Data.TryGetValue("donation_code", out string donationCode))
            {
                text += $". Codul donării a fost: {donationCode}";
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

            return $"Nivelul de glucoză a fost măsurat la {log.Data["glucose_level"]} {unit}";
        }

        public string BuildBloodPressureMeasurementLogText(PersonalLog log)
        {
            log.Data.TryGetValue("unit", out string unit);

            if (string.IsNullOrWhiteSpace(unit))
            {
                unit = "mmHg";
            }

            return $"Tensiunea arterială a fost măsurată la {log.Data["systolic_pressure"]}/{log.Data["diastolic_pressure"]} {unit}";
        }

        public string BuildBodyWaterRateMeasurementLogText(PersonalLog log)
        {
            decimal bodyWaterRate = decimal.Parse(log.Data["body_water_rate"]);

            return $"Nivelul de hidratare corporală a fost măsurat la {bodyWaterRate:F2}%";
        }

        public string BuildBodyWeightMeasurementLogText(PersonalLog log)
        {
            log.Data.TryGetValue("unit", out string unit);

            if (string.IsNullOrWhiteSpace(unit))
            {
                unit = "kg";
            }

            string text = $"Greutatea corporală a fost măsurată la {log.Data["body_weight"]} {unit}";

            if (log.Data.TryGetValue("scale_name", out string scaleName))
            {
                text += $", folosind cântarul {scaleName}";
            }

            return text;
        }

        public string BuildChatGroupCreationLogText(PersonalLog log)
        {
            log.Data.TryGetValue("platform", out string platform);

            if (string.IsNullOrWhiteSpace(platform))
            {
                platform = "chat";
            }

            string text = $"Am creat un grup de {platform}";

            if (!platform.Equals("chat"))
            {
                string discriminator = GetDiscriminator(log.Data);

                if (!string.IsNullOrWhiteSpace(discriminator))
                {
                    text += $" ({discriminator})";
                }
            }

            text += $" '{log.Data["group_name"]}'";

            return text;
        }

        public string BuildChatGroupDeletionLogText(PersonalLog log)
        {
            log.Data.TryGetValue("platform", out string platform);

            if (string.IsNullOrWhiteSpace(platform))
            {
                platform = "chat";
            }

            string text = $"Am șters grupul de {platform}";

            if (!platform.Equals("chat"))
            {
                string discriminator = GetDiscriminator(log.Data);

                if (!string.IsNullOrWhiteSpace(discriminator))
                {
                    text += $" ({discriminator})";
                }
            }

            text += $" '{log.Data["group_name"]}'";

            return text;
        }

        public string BuildChatGroupJoiningLogText(PersonalLog log)
        {
            log.Data.TryGetValue("platform", out string platform);

            if (string.IsNullOrWhiteSpace(platform))
            {
                platform = "chat";
            }

            string text = $"Am intrat în grupul de {platform}";

            if (!platform.Equals("chat"))
            {
                string discriminator = GetDiscriminator(log.Data);

                if (!string.IsNullOrWhiteSpace(discriminator))
                {
                    text += $" ({discriminator})";
                }
            }

            text += $" '{log.Data["group_name"]}'";

            return text;
        }

        public string BuildChatGroupLeavingLogText(PersonalLog log)
        {
            log.Data.TryGetValue("platform", out string platform);

            if (string.IsNullOrWhiteSpace(platform))
            {
                platform = "chat";
            }

            string text = $"Am ieșit din grupul de {platform}";

            if (!platform.Equals("chat"))
            {
                string discriminator = GetDiscriminator(log.Data);

                if (!string.IsNullOrWhiteSpace(discriminator))
                {
                    text += $" ({discriminator})";
                }
            }

            text += $" '{log.Data["group_name"]}'";

            return text;
        }

        public string BuildDatingAppMatchLogText(PersonalLog log)
        {
            string text = $"Am făcut match cu {log.Data["match_name"]} pe {log.Data["platform"]}";
            string discriminator = GetDiscriminator(log.Data);

            if (!string.IsNullOrWhiteSpace(discriminator))
            {
                text += $" ({discriminator})";
            }

            return text;
        }

        public string BuildDeliveryReceivalLogText(PersonalLog log)
        {
            string text = $"Am primit coletul cu {log.Data["package_description"]}";

            if (log.Data.TryGetValue("tracking_number", out string trackingNumber))
            {
                text += $", cu numărul de urmărire {trackingNumber}";
            }

            if (log.Data.TryGetValue("company_name", out string companyName))
            {
                text += $", prin {companyName}";
            }

            return text;
        }

        public string BuildDentalScalingLogText(PersonalLog log)
        {
            string text = $"Am efectuat un detartraj dentar";

            if (log.Data.TryGetValue("clinic_name", out string clinicName))
            {
                text += $" la {clinicName}";
            }

            return text;
        }

        public string BuildDeviceRepairLogText(PersonalLog log)
        {
            string text = $"Am reparat {log.Data["device_type"]}";

            if (log.Data.TryGetValue("device_name", out string deviceName))
            {
                text += $" ({deviceName})";
            }

            if (log.Data.TryGetValue("location", out string location))
            {
                text += $" din {location}";
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

            return $"Nivelul de bilirubină directă a fost măsurat la {log.Data["direct_bilirubin_level"]} {unit}";
        }

        public string BuildEmailExportLogText(PersonalLog log)
        {
            string text = $"Am exportat toate e-mail-urile din contul de {log.Data["platform"]}";

            if (log.Data.TryGetValue("account", out string account))
            {
                text += $" ({account})";
            }

            return text;
        }

        public string BuildEventTicketPurchaseLogText(PersonalLog log)
        {
            string text = $"Am cumpărat bilet";

            if (log.Data.TryGetValue("ticket_type", out string ticketType))
            {
                text += $" {ticketType}";
            }

            text += $"pentru '{log.Data["event_name"]}'";

            if (log.Data.TryGetValue("event_date", out string eventDate))
            {
                text += $" pe {eventDate}";
            }

            if (log.Data.TryGetValue("event_location", out string eventLocation))
            {
                text += $" la {eventLocation}";
            }

            return text;
        }

        public string BuildEyeCheckupLogText(PersonalLog log)
        {
            string text = $"Am efectuat un control oftalmologic";

            if (log.Data.TryGetValue("clinic_name", out string clinicName))
            {
                text += $" la {clinicName}";
            }

            if (log.Data.TryGetValue("optometrist_name", out string optometristName))
            {
                text += $", de către {optometristName}";
            }

            return text;
        }

        public string BuildGameAchievementUnlockLogText(PersonalLog log)
        {
            string achievementType = "achievement-ul";
            string gameName = log.Data["game_name"];
            log.Data.TryGetValue("platform", out string platform);

            if (gameName.Equals("eRepublik"))
            {
                achievementType = "medalia";
            }
            else if (platform is not null)
            {
                if (platform.Equals("Xbox") || platform.Equals("PlayStation"))
                {
                    achievementType = "trofeul";
                }
            }

            string text = $"Am obținut {achievementType} '{log.Data["achievement_name"]}' în jocul {gameName}";

            if (!string.IsNullOrWhiteSpace(platform))
            {
                text += $" pe contul de {platform}";
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
            log.Data.TryGetValue("game_name", out string gameName);
            string text = $"Am public un articol intitulat '{log.Data["article_title"]}' în {gameName}";

            if (gameName?.Equals("eRepublik") == true)
            {
                if (log.Data.TryGetValue("newspaper_name", out string newspaperName))
                {
                    text += $" în ziarul '{newspaperName}'";
                }
            }

            if (log.Data.TryGetValue("platform", out string platform))
            {
                text += $" pe {platform}";
            }

            string discriminator = GetDiscriminator(log.Data);

            if (!string.IsNullOrWhiteSpace(discriminator))
            {
                text += $" ({discriminator})";
            }

            return text;
        }

        public string BuildGameBuildingBoughtLogText(PersonalLog log)
        {
            string text = $"Am cumpărat clădirea {log.Data["building_name"]} în {log.Data["game_name"]}";

            if (log.Data.TryGetValue("platform", out string platform))
            {
                text += $" pe {platform}";
            }

            string discriminator = GetDiscriminator(log.Data);

            if (!string.IsNullOrWhiteSpace(discriminator))
            {
                text += $" ({discriminator})";
            }

            return text;
        }

        public string BuildGameBuildingLevelUpgradeLogText(PersonalLog log)
        {
            string text = $"Am ridicat clădirea {log.Data["building_name"]} la nivelul {log.Data["new_level"]} în {log.Data["game_name"]}";

            if (log.Data.TryGetValue("platform", out string platform))
            {
                text += $" pe {platform}";
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
            string text = $"Am construit {log.Data["construction_name"]} în {log.Data["game_name"]}";

            if (log.Data.TryGetValue("platform", out string platform))
            {
                text += $" pe {platform}";
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
            string text = $"Am început să construiesc {log.Data["construction_name"]} în {log.Data["game_name"]}";

            if (log.Data.TryGetValue("platform", out string platform))
            {
                text += $" pe {platform}";
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
            string text = $"Am terminat de construit {log.Data["construction_name"]} în {log.Data["game_name"]}";

            if (log.Data.TryGetValue("platform", out string platform))
            {
                text += $" pe {platform}";
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
            string text = $"Am avansat la rangul {log.Data["new_rank"]} în {log.Data["game_name"]}";

            if (log.Data.TryGetValue("platform", out string platform))
            {
                text += $" pe {platform}";
            }

            string discriminator = GetDiscriminator(log.Data);

            if (!string.IsNullOrWhiteSpace(discriminator))
            {
                text += $" ({discriminator})";
            }

            return text;
        }

        public string BuildGameStartedPlayingLogText(PersonalLog log)
        {
            string text = $"Am început să joc {log.Data["game_name"]}";

            if (log.Data.TryGetValue("platform", out string platform))
            {
                text += $" pe {platform}";
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
            string text = $"Am avansat la nivelul {log.Data["new_level"]} în {log.Data["game_name"]}";

            if (log.Data.TryGetValue("platform", out string platform))
            {
                text += $" pe {platform}";
            }

            string discriminator = GetDiscriminator(log.Data);

            if (!string.IsNullOrWhiteSpace(discriminator))
            {
                text += $" ({discriminator})";
            }

            return text;
        }

        public string BuildGettingInToBedLogText(PersonalLog log)
            => $"M-am pus în pat";

        public string BuildGettingOutOfBedLogText(PersonalLog log)
            => $"M-am ridicat din pat";

        public string BuildHairCuttingLogText(PersonalLog log)
        {
            string text = $"Am fost la tuns";

            if (log.Data.TryGetValue("salon_name", out string salonName))
            {
                text += $" la {salonName}";
            }

            if (log.Data.TryGetValue("hairdresser_name", out string hairdresserName))
            {
                text += $", la {hairdresserName}";
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

            return $"Nivelul de HDL Colesterol a fost măsurat la {log.Data["hdl_cholesterol_level"]} {unit}";
        }

        public string BuildHeartRateMeasurementLogText(PersonalLog log)
        {
            log.Data.TryGetValue("unit", out string unit);

            if (string.IsNullOrWhiteSpace(unit))
            {
                unit = "bpm";
            }

            string text = $"Ritmul cardiac a fost măsurat la {log.Data["heart_rate"]} {unit}";

            if (log.Data.TryGetValue("device_name", out string deviceName))
            {
                text += $" folosind {deviceName}";
            }

            return text;
        }

        public string BuildIndirectBilirubinMeasurementLogText(PersonalLog log)
        {
            log.Data.TryGetValue("unit", out string unit);

            if (string.IsNullOrWhiteSpace(unit))
            {
                unit = "mg/dL";
            }

            return $"Nivelul de bilirubină indirectă a fost măsurat la {log.Data["indirect_bilirubin_level"]} {unit}";
        }

        public string BuildInternshipApplicationSubmissionLogText(PersonalLog log)
        {
            string internshipType = "internship";

            if (log.Data.TryGetValue("period", out string period))
            {
                internshipType = $"{period} {internshipType}";
            }

            string text = $"Am trimis o aplicare de {internshipType} la {log.Data["company_name"]}";

            if (log.Data.TryGetValue("contact_person_name", out string contactPersonName))
            {
                text += $" către {contactPersonName}";
            }

            if (log.Data.TryGetValue("position_name", out string positionName))
            {
                text += $", pentru o poziție de {positionName}";
            }

            return text;
        }

        public string BuildKinetotherapySessionLogText(PersonalLog log)
        {
            string text = $"Am avut o ședință de kinetoterapie";

            if (log.Data.TryGetValue("clinic_name", out string clinicName))
            {
                text += $" la {clinicName}";
            }

            return text;
        }

        public string BuildLdlCholesterolMeasurementLogText(PersonalLog log)
        {
            log.Data.TryGetValue("unit", out string unit);

            if (string.IsNullOrWhiteSpace(unit))
            {
                unit = "mg/dL";
            }

            string text = $"Nivelul de LDL Colesterol a fost măsurat la {log.Data["ldl_cholesterol_level"]} {unit}";

            return text;
        }

        public string BuildMealVoucherCardCreditationLogText(PersonalLog log)
            => $"Cardul de bonuri de masă a fost creditat cu {log.Data["amount"]} {log.Data["currency"]}";

        public string BuildMovieWatchingLogText(PersonalLog log)
        {
            string text = $"Am vizionat filmul '{log.Data["movie_name"]}'";

            if (log.Data.TryGetValue("platform", out string platform))
            {
                text += $" pe {platform}";

                string discriminator = GetDiscriminator(log.Data);

                if (!string.IsNullOrWhiteSpace(discriminator))
                {
                    text += $" ({discriminator})";
                }
            }
            else if (log.Data.TryGetValue("location", out string location))
            {
                text += $" at {location}";
            }

            return text;
        }

        public string BuildObjectSaleLogText(PersonalLog log)
        {
            string text = $"Am vândut {log.Data["object_name"]}";

            if (log.Data.TryGetValue("platform", out string platform))
            {
                text += $" pe {platform}";
            }

            string discriminator = GetDiscriminator(log.Data);

            if (!string.IsNullOrWhiteSpace(discriminator))
            {
                text += $" ({discriminator})";
            }

            if (log.Data.TryGetValue("price_amount", out string priceAmount))
            {
                text += $" cu {priceAmount} {log.Data["price_currency"]}";
            }

            return text;
        }

        public string BuildOnlineReviewSubmissionLogText(PersonalLog log)
        {
            string text = $"Am trimis un review cu";

            if (log.Data.TryGetValue("stars_count", out string starsCount))
            {
                text += $" {starsCount} ste";

                if (starsCount.Equals("1"))
                {
                    text += "a";
                }
                else
                {
                    text += "le";
                }
            }

            text += $" pe {log.Data["platform"]}";
            string discriminator = GetDiscriminator(log.Data);

            if (!string.IsNullOrWhiteSpace(discriminator))
            {
                text += $" ({discriminator})";
            }

            text += $" pentru {log.Data["subject_name"]}";

            return text;
        }

        public string BuildOnlineStorePurchaseLogText(PersonalLog log)
        {
            string text = $"Am cumpărat {log.Data["product_name"]} pe {log.Data["platform"]}";
            string discriminator = GetDiscriminator(log.Data);

            if (!string.IsNullOrWhiteSpace(discriminator))
            {
                text += $" ({discriminator})";
            }

            if (log.Data.TryGetValue("price_amount", out string priceAmount))
            {
                text += $", cu {priceAmount} {log.Data["price_currency"]}";
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

            string text = $"Greutatea corporală a lui {log.Data["pet_name"]} a fost măsurată la {log.Data["pet_weight"]} {unit}";

            if (log.Data.TryGetValue("scale_name", out string scaleName))
            {
                text += $", pe cântarul {scaleName}";
            }

            return text;
        }

        public string BuildPhysiotherapySessionLogText(PersonalLog log)
        {
            string text = $"Am avut o ședință de fizioterapie";

            if (log.Data.TryGetValue("clinic_name", out string clinicName))
            {
                text += $" la {clinicName}";
            }

            return text;
        }

        public string BuildPsychotherapySessionLogText(PersonalLog log)
        {
            string text = $"Am avut o ședință de psihoterapie";

            if (log.Data.TryGetValue("clinic_name", out string clinicName))
            {
                text += $" la {clinicName}";
            }

            if (log.Data.TryGetValue("therapist_name", out string therapistName))
            {
                text += $", cu {therapistName}";
            }

            return text;
        }

        public string BuildSeriesEpisodeWatchingLogText(PersonalLog log)
        {
            string text = $"Am vizionat episodul #{log.Data["episode_number"]}";

            if (log.Data.TryGetValue("episode_name", out string episodeName))
            {
                text += $" '{episodeName}'";
            }

            if (log.Data.TryGetValue("season_number", out string seasonNumber))
            {
                text += $" din sezonul {seasonNumber}";
            }

            if (log.Data.TryGetValue("series_name", out string seriesName))
            {
                text += $" din '{seriesName}'";
            }

            if (log.Data.TryGetValue("platform", out string platform))
            {
                text += $" pe {platform}";

                string discriminator = GetDiscriminator(log.Data);

                if (!string.IsNullOrWhiteSpace(discriminator))
                {
                    text += $" ({discriminator})";
                }

            }
            else if (log.Data.TryGetValue("location", out string location))
            {
                text += $" la {location}";
            }

            return text;
        }

        public string BuildShowerTakingLogText(PersonalLog log)
        {
            string text = $"Am făcut duș";

            if (log.Data.TryGetValue("duration_minutes", out string durationMinutes))
            {
                text += $" timp de {durationMinutes} minute";
            }

            return text;
        }

        public string BuildStepCountMeasurementLogText(PersonalLog log)
        {
            string text = $"Am umblat {log.Data["step_count"]} de pași";

            if (log.Data.TryGetValue("device_name", out string deviceName))
            {
                text += $", conform măsurătorilor făcute de {deviceName}";
            }

            return text;
        }

        public string BuildSwimmingActivityLogText(PersonalLog log)
        {
            string text = $"Am fost la înnot";

            if (log.Data.TryGetValue("location", out string location))
            {
                text += $" la {location}";
            }

            return text;
        }

        public string BuildTeethBrushingLogText(PersonalLog log)
        {
            string text = $"M-am spălat pe dinți";

            if (log.Data.TryGetValue("duration_minutes", out string durationMinutes))
            {
                text += $" timp de {durationMinutes} minute";
            }

            return text;
        }

        public string BuildTollPaymentLogText(PersonalLog log)
        {
            string text = $"Am plătit taxa de drum";

            if (log.Data.TryGetValue("provider_name", out string providerName))
            {
                text += $" către {providerName}";
            }

            if (log.Data.TryGetValue("toll_location", out string tollLocation))
            {
                text += $" pentru {tollLocation}";
            }

            if (log.Data.TryGetValue("vehicle_registration_number", out string vehicleRegistrationNumber))
            {
                text += $" pentru vehiculul cu numărul de înmatriculare {vehicleRegistrationNumber}";
            }

            if (log.Data.TryGetValue("cost_amount", out string costAmount))
            {
                text += $", în valoare de {costAmount} {log.Data["cost_currency"]}";
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

            return $"Nivelul de bilirubină totală a fost măsurat la {log.Data["total_bilirubin_level"]} {unit}";
        }

        public string BuildTotalCholesterolMeasurementLogText(PersonalLog log)
        {
            string unit = "mg/dL";

            if (log.Data.TryGetValue("unit", out string unitValue))
            {
                unit = unitValue;
            }

            return $"Nivelul de colesterol total a fost măsurat la {log.Data["total_cholesterol_level"]} {unit}";
        }

        public string BuildUtilityBillPaymentLogText(PersonalLog log)
        {
            log.Data.TryGetValue("utility_type", out string utilityType);

            if (string.IsNullOrWhiteSpace(utilityType))
            {
                utilityType = "utility";
            }
            else if (utilityType.Equals("electricity"))
            {
                utilityType = "curent";
            }
            else if (utilityType.Equals("water"))
            {
                utilityType = "apă";
            }
            else if (utilityType.Equals("gas"))
            {
                utilityType = "gaz";
            }

            string text = $"Am plătit factura de {utilityType} la {log.Data["provider_name"]}";

            if (log.Data.TryGetValue("supply_point_number", out string supplyPointNumber))
            {
                text += $" pentru locuința cu numărul locului de consum {supplyPointNumber}";
            }

            if (log.Data.TryGetValue("cost_amount", out string costAmount))
            {
                text += $", în valoare de {costAmount} {log.Data["cost_currency"]}";
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
            else if (utilityType.Equals("electricity"))
            {
                utilityType = "curent";
            }
            else if (utilityType.Equals("water"))
            {
                utilityType = "apă";
            }
            else if (utilityType.Equals("gas"))
            {
                utilityType = "gaz";
            }

            string text = $"Am citit indexul contorului de {utilityType}";

            if (log.Data.TryGetValue("supply_point_number", out string supplyPointNumber))
            {
                text += $", pentru locuința cu numărul locului de consum {supplyPointNumber}";
            }
            else if (log.Data.TryGetValue("location", out string location))
            {
                text += $" din {location}";
            }

            if (log.Data.TryGetValue("index_value", out string indexValue))
            {
                text += $", obținând valoarea {indexValue}";
            }

            return text;
        }

        public string BuildVideoUploadLogText(PersonalLog log)
        {
            string text = $"Am publicat un video";

            if (log.Data.TryGetValue("video_url", out string videoId))
            {
                text += $" ({videoId})";
            }

            text += $" cu titlul '{log.Data["video_title"]}' pe {log.Data["platform"]}";
            string discriminator = GetDiscriminator(log.Data);
            if (!string.IsNullOrWhiteSpace(discriminator))
            {
                text += $" ({discriminator})";
            }

            if (log.Data.TryGetValue("uploaded_file_name", out string uploadedFileName))
            {
                text += $", din fișierul '{uploadedFileName}'";
            }

            return text;
        }

        public string BuildWakingUpLogText(PersonalLog log)
            => "M-am trezit";

        public string BuildWeddingAttendanceLogText(PersonalLog log)
        {
            string text = $"Am participat la nunta lui";

            log.Data.TryGetValue("bride_name", out string brideName);
            log.Data.TryGetValue("groom_name", out string groomName);

            if (!string.IsNullOrWhiteSpace(brideName) && !string.IsNullOrWhiteSpace(groomName))
            {
                text += $" {groomName} și {brideName}";
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
                text += $" în {location}";
            }

            if (log.Data.TryGetValue("venue_name", out string venueName))
            {
                text += $" la {venueName}";
            }

            return text;
        }

        public string BuildWorkFromTheOfficeLogText(PersonalLog log)
        {
            string text = $"Am lucrat de la birou";

            if (log.Data.TryGetValue("office_name", out string officeName))
            {
                text += $", din {officeName}";
            }

            return text;
        }
    }
}
