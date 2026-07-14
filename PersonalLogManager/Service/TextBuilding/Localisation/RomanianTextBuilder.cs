using System.Collections.Generic;

using NuciText.Obfuscation;

using PersonalLogManager.Service.Models;

namespace PersonalLogManager.Service.TextBuilding.Localisation
{
    public class RomanianTextBuilder(INuciTextObfuscator obfuscator)
        : PersonalLogTextBuilderBase(obfuscator), IPersonalLogTextBuilder
    {
        protected override string LanguageCode => "ro";

        public string BuildAccessoryCleaningLogText(PersonalLog log)
            => $"Mi-am curățat {GetAccessoryType(log.Data, true)} prin {GetCleaningMethod(log.Data)}" +
                GetLocation(log.Data);

        public string BuildAccountActivationLogText(PersonalLog log)
            => $"Am activat contul de {GetPlatform(log.Data)}";

        public string BuildAccountBanningLogText(PersonalLog log)
        {
            string text = $"Contul de {GetPlatform(log.Data)} a fost banat";

            string banReason = GetDataValue(log.Data, "ban_reason");

            if (!string.IsNullOrWhiteSpace(banReason))
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

            string text = $"Am {verb} adresa de e-mail de contact a contului de {GetPlatform(log.Data)}";

            string oldContactEmailAddress = GetDataValue(log.Data, "old_contact_email_address");

            if (!string.IsNullOrWhiteSpace(oldContactEmailAddress))
            {
                text += $" de la {oldContactEmailAddress}";
            }

            string newContactEmailAddress = GetDataValue(log.Data, "new_contact_email_address");

            if (!string.IsNullOrWhiteSpace(newContactEmailAddress))
            {
                text += $" la {newContactEmailAddress}";
            }

            return text;
        }

        public string BuildAccountDataExportLogText(PersonalLog log)
            => $"Am exportat datele contului de {GetPlatform(log.Data)}";

        public string BuildAccountDataExportRequestLogText(PersonalLog log)
        {
            string text = $"Am trimis o solicitare a unui export al datelor contului de {GetPlatform(log.Data)}";

            string requestId = GetDataValue(log.Data, "request_id");

            if (!string.IsNullOrWhiteSpace(requestId))
            {
                text += $" cu codul de identificare {requestId}";
            }

            string requestMethod = GetMappedDataValue(
                log.Data,
                "request_method",
                new()
                {
                    { "AccountSettings", "pagina de setări a contului" },
                    { "ContactForm", "formularul de contact" },
                    { "EMail", "e-mail" },
                    { "SupportTicket", "un tichet de suport" }
                });

            if (!string.IsNullOrWhiteSpace(requestMethod))
            {
                text += $", prin {requestMethod}";
            }

            return text;
        }

        public string BuildAccountDataExportRequestFulfillmentLogText(PersonalLog log)
        {
            string text = $"Solicitarea exportului de date ale contului de {GetPlatform(log.Data)}";

            string requestId = GetDataValue(log.Data, "request_id");

            if (!string.IsNullOrWhiteSpace(requestId))
            {
                text += $" cu codul de identificare {requestId}";
            }

            string requestDate = GetDataValue(log.Data, "request_date");

            if (!string.IsNullOrWhiteSpace(requestDate))
            {
                text += $", trimisă pe {requestDate},";
            }

            return $"{text} a fost îndeplinită";
        }

        public string BuildAccountDataExportSaveLogText(PersonalLog log)
        {
            string text = $"Am salvat exportul datelor contului de {GetPlatform(log.Data)}";

            string requestDate = GetDataValue(log.Data, "request_date");
            string requestId = GetDataValue(log.Data, "request_id");

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
            => $"Am ofuscat datele contului de {GetPlatform(log.Data)}";

        public string BuildAccountDeactivationLogText(PersonalLog log)
            => $"Am dezactivat contul de {GetPlatform(log.Data)}";

        public string BuildAccountDeletionLogText(PersonalLog log)
            => $"Am șters contul de {GetPlatform(log.Data)}";

        public string BuildAccountDeletionRequestLogText(PersonalLog log)
        {
            string text = $"Am trimis o solicitare de ștergere a contului de {GetPlatform(log.Data)}";

            string requestId = GetDataValue(log.Data, "request_id");

            if (!string.IsNullOrWhiteSpace(requestId))
            {
                text += $" cu codul de identificare {requestId}";
            }

            string requestMethod = GetMappedDataValue(
                log.Data,
                "request_method",
                new()
                {
                    { "AccountSettings", "pagina de setări a contului" },
                    { "ContactForm", "formularul de contact" },
                    { "EMail", "e-mail" },
                    { "SupportTicket", "un tichet de suport" }
                });

            if (!string.IsNullOrWhiteSpace(requestMethod))
            {
                text += $", prin {requestMethod}";
            }

            return text;
        }

        public string BuildAccountDeletionRequestCancellationLogText(PersonalLog log)
        {
            string text = $"Am anulat solicitarea ștergerii contului de {GetPlatform(log.Data)}";

            string requestId = GetDataValue(log.Data, "request_id");

            if (!string.IsNullOrWhiteSpace(requestId))
            {
                text += $" cu codul de identificare {requestId}";
            }

            string requestDate = GetDataValue(log.Data, "request_date");

            if (!string.IsNullOrWhiteSpace(requestDate))
            {
                text += $", trimisă pe {requestDate}";
            }

            return text;
        }

        public string BuildAccountDeletionRequestFulfillmentLogText(PersonalLog log)
        {
            string text = $"Solicitarea de ștergere a contului de {GetPlatform(log.Data)}";

            string requestId = GetDataValue(log.Data, "request_id");

            if (!string.IsNullOrWhiteSpace(requestId))
            {
                text += $" cu codul de identificare {requestId}";
            }

            string requestDate = GetDataValue(log.Data, "request_date");

            if (!string.IsNullOrWhiteSpace(requestDate))
            {
                text += $", trimisă pe {requestDate},";
            }

            text += " a fost îndeplinită";

            return text;
        }

        public string BuildAccountDeletionRequestRejectionLogText(PersonalLog log)
        {
            string text = $"Solicitarea de ștergere a contului de {GetPlatform(log.Data)}";

            string requestId = GetDataValue(log.Data, "request_id");

            if (!string.IsNullOrWhiteSpace(requestId))
            {
                text += $" cu codul de identificare {requestId}";
            }

            string requestDate = GetDataValue(log.Data, "request_date");

            if (!string.IsNullOrWhiteSpace(requestDate))
            {
                text += $", trimisă pe {requestDate},";
            }

            text += " a fost respinsă";

            return text;
        }

        public string BuildAccountDeletionValidationLogText(PersonalLog log)
        {
            string text = $"Am verificat că a fost șters contul de {GetPlatform(log.Data)}";

            string requestDate = GetDataValue(log.Data, "request_date");

            if (!string.IsNullOrWhiteSpace(requestDate))
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

            string text = $"Am {verb} adresa de e-mail a contului de {GetPlatform(log.Data)}";

            string oldEmailAddress = GetDataValue(log.Data, "old_email_address");

            if (!string.IsNullOrWhiteSpace(oldEmailAddress))
            {
                text += $" din {oldEmailAddress}";
            }

            string newEmailAddress = GetDataValue(log.Data, "new_email_address");

            if (!string.IsNullOrWhiteSpace(newEmailAddress))
            {
                text += $" în {newEmailAddress}";
            }

            return text;
        }

        public string BuildAccountEmailAddressChangeRequestLogText(PersonalLog log)
        {
            string text = $"Am trimis o solicitare de schimbare a adresei de e-mail a contului de {GetPlatform(log.Data)}";

            string oldEmailAddress = GetDataValue(log.Data, "old_email_address");

            if (!string.IsNullOrWhiteSpace(oldEmailAddress))
            {
                text += $" din {oldEmailAddress}";
            }

            string newEmailAddress = GetDataValue(log.Data, "new_email_address");

            if (!string.IsNullOrWhiteSpace(newEmailAddress))
            {
                text += $" în {newEmailAddress}";
            }

            string requestId = GetDataValue(log.Data, "request_id");

            if (!string.IsNullOrWhiteSpace(requestId))
            {
                text += $", cu codul de identificare {requestId}";
            }

            string requestMethod = GetMappedDataValue(
                log.Data,
                "request_method",
                new()
                {
                    { "AccountSettings", "pagina de setări a contului" },
                    { "ContactForm", "formularul de contact" },
                    { "EMail", "e-mail" },
                    { "SupportTicket", "un tichet de suport" }
                });

            if (!string.IsNullOrWhiteSpace(requestMethod))
            {
                text += $", prin {requestMethod}";
            }

            return text;
        }

        public string BuildAccountEmailAddressChangeRequestFulfillmentLogText(PersonalLog log)
        {
            string text = $"Solicitarea de schimbare a adresei de e-mail a contului de {GetPlatform(log.Data)}";

            string oldEmailAddress = GetDataValue(log.Data, "old_email_address");

            if (!string.IsNullOrWhiteSpace(oldEmailAddress))
            {
                text += $" de la {oldEmailAddress}";
            }

            string newEmailAddress = GetDataValue(log.Data, "new_email_address");

            if (!string.IsNullOrWhiteSpace(newEmailAddress))
            {
                text += $" la {newEmailAddress}";
            }

            string requestId = GetDataValue(log.Data, "request_id");

            if (!string.IsNullOrWhiteSpace(requestId))
            {
                text += $", cu codul de identificare {requestId}";
            }

            string requestDate = GetDataValue(log.Data, "request_date");

            if (!string.IsNullOrWhiteSpace(requestDate))
            {
                text += $", trimisă pe {requestDate},";
            }

            text += " a fost îndeplinită";

            return text;
        }

        public string BuildAccountEmailAddressConfirmationLogText(PersonalLog log)
        {
            string text = $"Am confirmat adresa de e-mail";

            string emailAddress = GetDataValue(log.Data, "email_address");

            if (!string.IsNullOrWhiteSpace(emailAddress))
            {
                text += $" ({emailAddress})";
            }

            return text + $" a contului de {GetPlatform(log.Data)}";
        }

        public string BuildAccountFeatureEnablementLogText(PersonalLog log)
            => $"Am activat funcția {log.Data["feature_name"]} pe contul de {GetPlatform(log.Data)}";

        public string BuildAccountFeatureDisablementLogText(PersonalLog log)
            => $"Am dezactivat funcția de {log.Data["feature_name"]} pe contul de {GetPlatform(log.Data)}";

        public string BuildAccountFriendshipRequestReceivalLogText(PersonalLog log)
        {
            string text = $"Am primit o solicitare de prietenie pe contul de {GetPlatform(log.Data)}";

            string fromAccount = GetDataValue(log.Data, "from_account");

            if (!string.IsNullOrWhiteSpace(fromAccount))
            {
                text += $" de la {fromAccount}";
            }

            return text;
        }

        public string BuildAccountIdentityVerificationLogText(PersonalLog log)
            => $"Am verificat identitatea pentru contul de {GetPlatform(log.Data)}";

        public string BuildAccountLinkingLogText(PersonalLog log)
        {
            string text = $"Am conectat contul de {GetPlatform(log.Data)}";

            text += $" cu";

            string platformLinked = GetDataValue(log.Data, "platform_linked");

            if (!string.IsNullOrWhiteSpace(platformLinked))
            {
                string accountLinked = GetDataValue(log.Data, "account_linked");

                if (!string.IsNullOrWhiteSpace(accountLinked))
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

        public string BuildAccountLoginLogText(PersonalLog log)
        {
            string text = $"M-am logat în contul de {GetPlatform(log.Data)}";

            string ipAddress = GetDataValue(log.Data, "ip_address");

            if (!string.IsNullOrWhiteSpace(ipAddress))
            {
                text += $" de la adresa IP {ipAddress}";
            }

            return text;
        }

        public string BuildAccountMessagesErasureLogText(PersonalLog log)
            => $"Am șters toate mesajele trimise de pe contul de {GetPlatform(log.Data)}";

        public string BuildAccountPasswordChangeLogText(PersonalLog log)
            => $"Am schimbat parola contului de {GetPlatform(log.Data)}";

        public string BuildAccountPersonalNameChangeLogText(PersonalLog log)
        {
            string text = $"Am schimbat numele personal al contului de {GetPlatform(log.Data)}";

            string oldPersonalName = GetDataValue(log.Data, "old_personal_name");

            if (!string.IsNullOrWhiteSpace(oldPersonalName))
            {
                text += $" de la {oldPersonalName}";
            }

            string newPersonalName = GetDataValue(log.Data, "new_personal_name");

            if (!string.IsNullOrWhiteSpace(newPersonalName))
            {
                text += $" la {newPersonalName}";
            }

            return text;
        }

        public string BuildAccountPhoneNumberAdditionLogText(PersonalLog log)
        {
            string text = $"Am adăugat un număr de telefon la contului de {GetPlatform(log.Data)}";

            string newPhoneNumber = GetDataValue(log.Data, "phone_number");

            if (!string.IsNullOrWhiteSpace(newPhoneNumber))
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

            string text = $"Am {verb} numărul de telefon al contului de {GetPlatform(log.Data)}";

            string oldPhoneNumber = GetDataValue(log.Data, "old_phone_number");

            if (!string.IsNullOrWhiteSpace(oldPhoneNumber))
            {
                text += $" de la {oldPhoneNumber}";
            }

            string newPhoneNumber = GetDataValue(log.Data, "new_phone_number");

            if (!string.IsNullOrWhiteSpace(newPhoneNumber))
            {
                text += $" la {newPhoneNumber}";
            }

            return text;
        }

        public string BuildAccountPhoneNumberConfirmationLogText(PersonalLog log)
        {
            string text = $"Am confirmat numărul de telefon";

            string phoneNumber = GetDataValue(log.Data, "phone_number");

            if (!string.IsNullOrWhiteSpace(phoneNumber))
            {
                text += $" ({phoneNumber})";
            }

            return text + $" al contului de {GetPlatform(log.Data)}";
        }

        public string BuildAccountPhoneNumberRemovalLogText(PersonalLog log)
        {
            string text = $"Am șters un număr de telefon din contul de {GetPlatform(log.Data)}";

            string newPhoneNumber = GetDataValue(log.Data, "phone_number");

            if (!string.IsNullOrWhiteSpace(newPhoneNumber))
            {
                text += $": {newPhoneNumber}";
            }

            return text;
        }

        public string BuildAccountProfilePictureChangeLogText(PersonalLog log)
            => $"Am schimbat poza de profil a contului de {GetPlatform(log.Data)}";

        public string BuildAccountRecoveryLogText(PersonalLog log)
            => $"Am recuperat contul de {GetPlatform(log.Data)}";

        public string BuildAccountRecoveryEmailAddressChangeLogText(PersonalLog log)
        {
            string verb = "schimbat";

            if (!log.Data.ContainsKey("old_email_address") && log.Data.ContainsKey("new_email_address"))
            {
                verb = "setat";
            }

            string text = $"Am {verb} adresa de e-mail de recuperare a contului de {GetPlatform(log.Data)}";

            string oldEmailAddress = GetDataValue(log.Data, "old_recovery_email_address");

            if (!string.IsNullOrWhiteSpace(oldEmailAddress))
            {
                text += $" de la {oldEmailAddress}";
            }

            string newEmailAddress = GetDataValue(log.Data, "new_recovery_email_address");

            if (!string.IsNullOrWhiteSpace(newEmailAddress))
            {
                text += $" la {newEmailAddress}";
            }

            return text;
        }

        public string BuildAccountRecoveryRequestLogText(PersonalLog log)
        {
            string text = $"Am trimis o solicitare de recuperare a contului de {GetPlatform(log.Data)}";

            string requestId = GetDataValue(log.Data, "request_id");

            if (!string.IsNullOrWhiteSpace(requestId))
            {
                text += $" cu codul de identificare {requestId}";
            }

            string requestMethod = GetMappedDataValue(
                log.Data,
                "request_method",
                new()
                {
                    { "AccountSettings", "pagina de setări a contului" },
                    { "ContactForm", "formularul de contact" },
                    { "EMail", "e-mail" },
                    { "SupportTicket", "un tichet de suport" }
                });

            if (!string.IsNullOrWhiteSpace(requestMethod))
            {
                text += $", prin {requestMethod}";
            }

            return text;
        }

        public string BuildAccountRecoveryRequestFulfillmentLogText(PersonalLog log)
        {
            string text = $"Solicitarea de recuperare a contului de {GetPlatform(log.Data)}";

            string requestId = GetDataValue(log.Data, "request_id");

            if (!string.IsNullOrWhiteSpace(requestId))
            {
                text += $" cu codul de identificare {requestId}";
            }

            string requestDate = GetDataValue(log.Data, "request_date");

            if (!string.IsNullOrWhiteSpace(requestDate))
            {
                text += $", trimisă pe {requestDate},";
            }

            return $"{text} a fost îndeplinită";
        }

        public string BuildAccountRegistrationLogText(PersonalLog log)
        {
            string text = $"Am înregistrat contul de {GetPlatform(log.Data)}";

            int withCount = 0;

            string username = GetDataValue(log.Data, "username");

            if (!string.IsNullOrWhiteSpace(username))
            {
                text += $" cu numele de utilizator {username}";
                withCount += 1;
            }

            string phoneNumber = GetDataValue(log.Data, "phone_number");

            if (!string.IsNullOrWhiteSpace(phoneNumber))
            {
                if (withCount > 0)
                {
                    text += ", și";
                }

                text += $" cu numărul de telefon {phoneNumber}";
                withCount += 1;
            }

            string emailAddress = GetDataValue(log.Data, "email_address");

            if (!string.IsNullOrWhiteSpace(emailAddress))
            {
                if (withCount > 0)
                {
                    text += ", și";
                }

                text += $" cu adresa de e-mail {emailAddress}";
                withCount += 1;
            }

            string personalName = GetDataValue(log.Data, "personal_name");

            if (!string.IsNullOrWhiteSpace(personalName))
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
            string requestId = GetDataValue(log.Data, "request_id");

            if (!string.IsNullOrWhiteSpace(requestId))
            {
                text += $" cu codul de identificare {requestId}";
            }

            text += $" a contului de {GetPlatform(log.Data)}";

            int withCount = 0;

            string username = GetDataValue(log.Data, "username");

            if (!string.IsNullOrWhiteSpace(username))
            {
                text += $" cu numele de utilizator {username}";
                withCount += 1;
            }

            string phoneNumber = GetDataValue(log.Data, "phone_number");

            if (!string.IsNullOrWhiteSpace(phoneNumber))
            {
                if (withCount > 0)
                {
                    text += ", și";
                }

                text += $" cu numărul de telefon {phoneNumber}";
                withCount += 1;
            }

            string emailAddress = GetDataValue(log.Data, "email_address");

            if (!string.IsNullOrWhiteSpace(emailAddress))
            {
                if (withCount > 0)
                {
                    text += ", și";
                }

                text += $" cu adresa de e-mail {emailAddress}";
                withCount += 1;
            }

            string personalName = GetDataValue(log.Data, "personal_name");

            if (!string.IsNullOrWhiteSpace(personalName))
            {
                if (withCount > 0)
                {
                    text += ", și";
                }

                text += $" cu numele personal {personalName}";
                withCount += 1;
            }

            return text; ;
        }

        public string BuildAccountRegistrationRequestFulfillmentLogText(PersonalLog log)
        {
            string text = $"Solicitarea înregistrării contului de {GetPlatform(log.Data)}";

            string requestId = GetDataValue(log.Data, "request_id");

            if (!string.IsNullOrWhiteSpace(requestId))
            {
                text += $" cu codul de identificare {requestId}";
            }

            string requestDate = GetDataValue(log.Data, "request_date");

            if (!string.IsNullOrWhiteSpace(requestDate))
            {
                text += $", trimisă pe {requestDate},";
            }

            text += " a fost îndeplinită";

            return text;
        }

        public string BuildAccountSecurityQuestionsChangeLogText(PersonalLog log)
            => $"Am schimbat întrebările de securitate ale contului de {GetPlatform(log.Data)}";

        public string BuildAccountSubscriptionPurchaseLogText(PersonalLog log)
        {
            string text = $"Am cumpărat un abonament";

            string subscriptionName = GetDataValue(log.Data, "subscription_name");

            if (!string.IsNullOrWhiteSpace(subscriptionName))
            {
                text += $" {subscriptionName}";
            }

            text += $" pentru contul de {GetPlatform(log.Data)}";

            if (log.Data.ContainsKey("price_amount"))
            {
                text += $" pentru {GetBalance(log.Data)}";
            }

            return text;
        }

        public string BuildAccountUnlinkingLogText(PersonalLog log)
        {
            string text = $"Am deconectat contul de {GetPlatform(log.Data)}";

            text += $" de contul de {log.Data["platform_unlinked"]}";

            string accountLinked = GetDataValue(log.Data, "account_unlinked");

            if (!string.IsNullOrWhiteSpace(accountLinked))
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

            string text = $"Am {verb} numele de utilizator al contului de {GetPlatform(log.Data)}";

            string oldUsername = GetDataValue(log.Data, "old_username");

            if (!string.IsNullOrWhiteSpace(oldUsername))
            {
                text += $" de la {oldUsername}";
            }

            string newUsername = GetDataValue(log.Data, "new_username");

            if (!string.IsNullOrWhiteSpace(newUsername))
            {
                text += $" la {newUsername}";
            }

            return text;
        }

        public string BuildAccountUsernameChangeRequestLogText(PersonalLog log)
        {
            string text = $"Am trimis o solicitare de schimbare a numelui de utilizator al contului de {GetPlatform(log.Data)}";

            string oldUsername = GetDataValue(log.Data, "old_username");

            if (!string.IsNullOrWhiteSpace(oldUsername))
            {
                text += $" de la {oldUsername}";
            }

            string newUsername = GetDataValue(log.Data, "new_username");

            if (!string.IsNullOrWhiteSpace(newUsername))
            {
                text += $" la {newUsername}";
            }

            string requestId = GetDataValue(log.Data, "request_id");

            if (!string.IsNullOrWhiteSpace(requestId))
            {
                text += $", cu codul de identificare {requestId}";
            }

            string requestMethod = GetMappedDataValue(
                log.Data,
                "request_method",
                new()
                {
                    { "AccountSettings", "pagina de setări a contului" },
                    { "ContactForm", "formularul de contact" },
                    { "EMail", "e-mail" },
                    { "SupportTicket", "un tichet de suport" }
                });

            if (!string.IsNullOrWhiteSpace(requestMethod))
            {
                text += $", prin {requestMethod}";
            }

            return text;
        }

        public string BuildAccountUsernameChangeRequestFulfillmentLogText(PersonalLog log)
        {
            string text = $"Solicitarea de schimbare a numelui de utilizator al contului de {GetPlatform(log.Data)}";

            string oldUsername = GetDataValue(log.Data, "old_username");

            if (!string.IsNullOrWhiteSpace(oldUsername))
            {
                text += $" de la {oldUsername}";
            }

            string newUsername = GetDataValue(log.Data, "new_username");

            if (!string.IsNullOrWhiteSpace(newUsername))
            {
                text += $" la {newUsername}";
            }

            string requestId = GetDataValue(log.Data, "request_id");

            if (!string.IsNullOrWhiteSpace(requestId))
            {
                text += $", cu codul de identificare {requestId}";
            }

            string requestDate = GetDataValue(log.Data, "request_date");

            if (!string.IsNullOrWhiteSpace(requestDate))
            {
                text += $", trimisă pe {requestDate},";
            }

            return $"{text} a fost îndeplinită";
        }

        public string BuildAccountVisibilityMadePrivateLogText(PersonalLog log)
            => $"Am făcut privat contul de {GetPlatform(log.Data)}";

        public string BuildAccountVisibilityMadePublicLogText(PersonalLog log)
            => $"Am făcut public contul de {GetPlatform(log.Data)}";

        public string BuildAlkalinePhosphataseMeasurementLogText(PersonalLog log)
            => $"Nivelul de fosfatază alcalină a fost măsurat la {GetDecimalValue(log.Data, "alkaline_phosphatase_level")} {GetDataValue(log.Data, "unit", "U/L")}" +
                GetLocation(log.Data);

        public string BuildApplicationInstallationLogText(PersonalLog log)
            => $"Am instalat aplicația {GetDataValue(log.Data, "application_name")} pe {GetDevice(log.Data)}";

        public string BuildApplicationUninstallationLogText(PersonalLog log)
            => $"Am dezinstalat aplicația {GetDataValue(log.Data, "application_name")} de pe {GetDevice(log.Data)}";

        public string BuildBedLinenChangingLogText(PersonalLog log)
            => "Am schimbat lenjeria de pat" + GetLocation(log.Data);

        public string BuildBedMakingLogText(PersonalLog log)
            => "Am făcut patul" + GetLocation(log.Data);

        public string BuildBloodDonationLogText(PersonalLog log)
        {
            string text = $"Am donat sânge" + GetLocation(log.Data);

            string donationCode = GetDataValue(log.Data, "donation_code");

            if (!string.IsNullOrWhiteSpace(donationCode))
            {
                text += $". Codul donării a fost: {donationCode}";
            }

            return text;
        }

        public string BuildBloodGlucoseMeasurementLogText(PersonalLog log)
        {
            string text =
                "Nivelul de glucoză a fost măsurat la" +
                $" {log.Data["glucose_level"]} {GetDataValue(log.Data, "unit", "mg/dL")}" +
                GetLocation(log.Data);

            string device = GetDevice(log.Data);

            if (!string.IsNullOrWhiteSpace(device) &&
                !string.Equals(device, MissingValue))
            {
                text += $", utilizând {device}";
            }

            return text;
        }

        public string BuildBloodPressureMeasurementLogText(PersonalLog log)
        {
            string text = $"Tensiunea arterială a fost măsurată la {log.Data["systolic_pressure"]}/{log.Data["diastolic_pressure"]} {GetDataValue(log.Data, "unit", "mmHg")}" +
                GetLocation(log.Data);

            string device = GetDevice(log.Data);

            if (!string.IsNullOrWhiteSpace(device) &&
                !string.Equals(device, MissingValue))
            {
                text += $", utilizând {device}";
            }

            return text;
        }

        public string BuildBodyWaterRateMeasurementLogText(PersonalLog log)
        {
            string text = $"Nivelul de hidratare corporală a fost măsurat la {GetDecimalValue(log.Data, "body_water_rate")}%" +
                GetLocation(log.Data);

            string device = GetDevice(log.Data);

            if (!string.IsNullOrWhiteSpace(device) &&
                !string.Equals(device, MissingValue))
            {
                text += $", utilizând {device}";
            }

            return text;
        }

        public string BuildBodyWeightMeasurementLogText(PersonalLog log)
        {
            string text =
                $"Greutatea corporală a fost măsurată la {GetDataValue(log.Data, "body_weight")} {GetDataValue(log.Data, "unit", "kg")}" +
                GetLocation(log.Data);

            string device = GetDevice(log.Data);

            if (!string.IsNullOrWhiteSpace(device) &&
                !string.Equals(device, MissingValue))
            {
                text += $", utilizând {device}";
            }

            return text;
        }

        public string BuildBookBeginningLogText(PersonalLog log)
        {
            string verb = "citesc";
            string bookType = GetMappedDataValue(
                log.Data,
                "book_type",
                new()
                {
                    { "Book", "cartea" },
                    { "ComicBook", "benzile desenate" },
                    { "Audiobook", "cartea audio" },
                },
                "cartea");

            if (bookType.Equals("cartea audio"))
            {
                verb = "ascult";
            }

            string text = $"Am început să {verb} {bookType} '{log.Data["book_title"]}'";

            string bookSeriesName = GetDataValue(log.Data, "book_series_name");

            if (!string.IsNullOrWhiteSpace(bookSeriesName))
            {
                text += $" din seria '{bookSeriesName}'";
            }

            return text;
        }

        public string BuildBookChapterBeginningLogText(PersonalLog log)
        {
            string verb = "citesc";
            string bookType = GetMappedDataValue(
                log.Data,
                "book_type",
                new()
                {
                    { "Book", "cartea" },
                    { "ComicBook", "benzile desenate" },
                    { "Audiobook", "cartea audio" },
                },
                "cartea");

            if (bookType.Equals("cartea audio"))
            {
                verb = "ascult";
            }

            string text = $"Am început să {verb} capitolul {log.Data["chapter_number"]} din {bookType} '{log.Data["book_title"]}'";

            string bookSeriesName = GetDataValue(log.Data, "book_series_name");

            if (!string.IsNullOrWhiteSpace(bookSeriesName))
            {
                text += $" din seria '{bookSeriesName}'";
            }

            return text;
        }

        public string BuildBookChapterCompletionLogText(PersonalLog log)
        {
            string verb = "citit";
            string bookType = GetMappedDataValue(
                log.Data,
                "book_type",
                new()
                {
                    { "Book", "cartea" },
                    { "ComicBook", "benzile desenate" },
                    { "Audiobook", "cartea audio" },
                },
                "cartea");

            if (bookType.Equals("cartea audio"))
            {
                verb = "ascultat";
            }

            string text = $"Am terminat de {verb} capitolul {log.Data["chapter_number"]} din {bookType} '{log.Data["book_title"]}'";

            string bookSeriesName = GetDataValue(log.Data, "book_series_name");

            if (!string.IsNullOrWhiteSpace(bookSeriesName))
            {
                text += $" din seria '{bookSeriesName}'";
            }

            return text;
        }

        public string BuildBookCompletionLogText(PersonalLog log)
        {
            string verb = "citit";
            string bookType = GetMappedDataValue(
                log.Data,
                "book_type",
                new()
                {
                    { "Book", "cartea" },
                    { "ComicBook", "benzile desenate" },
                    { "Audiobook", "cartea audio" },
                },
                "cartea");

            if (bookType.Equals("cartea audio"))
            {
                verb = "ascultat";
            }

            string text = $"Am terminat de {verb} {bookType} '{log.Data["book_title"]}'";

            string bookSeriesName = GetDataValue(log.Data, "book_series_name");

            if (!string.IsNullOrWhiteSpace(bookSeriesName))
            {
                text += $" din seria '{bookSeriesName}'";
            }

            return text;
        }

        public string BuildBookResumingLogText(PersonalLog log)
        {
            string verb = "citesc";
            string bookType = GetMappedDataValue(
                log.Data,
                "book_type",
                new()
                {
                    { "Book", "cartea" },
                    { "ComicBook", "benzile desenate" },
                    { "Audiobook", "cartea audio" },
                },
                "cartea");

            if (bookType.Equals("cartea audio"))
            {
                verb = "ascult";
            }

            string text = $"Am reluat să {verb} {bookType} '{log.Data["book_title"]}'";

            string bookSeriesName = GetDataValue(log.Data, "book_series_name");

            if (!string.IsNullOrWhiteSpace(bookSeriesName))
            {
                text += $" din seria '{bookSeriesName}'";
            }

            return text;
        }

        public string BuildBookStoppingLogText(PersonalLog log)
        {
            string verb = "citi";
            string bookType = GetMappedDataValue(
                log.Data,
                "book_type",
                new()
                {
                    { "Book", "cartea" },
                    { "ComicBook", "benzile desenate" },
                    { "Audiobook", "cartea audio" },
                },
                "cartea");

            if (bookType.Equals("cartea audio"))
            {
                verb = "asculta";
            }

            string text = $"M-am oprit din a {verb} {bookType} '{log.Data["book_title"]}'";

            string bookSeriesName = GetDataValue(log.Data, "book_series_name");

            if (!string.IsNullOrWhiteSpace(bookSeriesName))
            {
                text += $" din seria '{bookSeriesName}'";
            }

            return text;
        }

        public string BuildBotPrizeWinningLogText(PersonalLog log)
        {
            string text = $"Am câștigat un premiu cu un bot";

            string platform = GetPlatform(log.Data);

            if (!string.IsNullOrWhiteSpace(platform))
            {
                text += $" de {platform}";
            }

            return $"{text}: {log.Data["prize_description"]}";
        }

        public string BuildBotsTotalBalanceMeasurementLogText(PersonalLog log)
        {
            string text = $"Soldul total al boților";

            string platform = GetPlatform(log.Data);

            if (!string.IsNullOrWhiteSpace(platform))
            {
                text += $" de {platform}";
            }

            return $"{text} a fost măsurat la {GetBalance(log.Data)}";
        }

        public string BuildCalciumLevelMeasurementLogText(PersonalLog log)
        {
            string text =
            $"Nivelul de calciu a fost măsurat la {GetDecimalValue(log.Data, "calcium_level")} {GetDataValue(log.Data, "unit", "mg/dL")}" +
                GetLocation(log.Data);

            string device = GetDevice(log.Data);

            if (!string.IsNullOrWhiteSpace(device) &&
                !string.Equals(device, MissingValue))
            {
                text += $", utilizând {device}";
            }

            return text;
        }

        public string BuildCertificationObtainmentLogText(PersonalLog log)
        {
            string text = $"Am obținut certificarea {log.Data["certification_name"]}";

            string certificationAuthority = GetDataValue(log.Data, "certification_authority");

            if (!string.IsNullOrWhiteSpace(certificationAuthority))
            {
                text += $" de la {certificationAuthority}";
            }

            return text;
        }

        public string BuildChatGroupCreationLogText(PersonalLog log)
        {
            string text = $"Am creat un grup de chat";

            string platform = GetPlatform(log.Data);

            if (!string.IsNullOrWhiteSpace(platform))
            {
                text += $" pe {platform}";
            }

            return $"{text} numit '{log.Data["group_name"]}'";
        }

        public string BuildChatGroupDeletionLogText(PersonalLog log)
        {
            string text = $"Am șters grupul de chat";

            string platform = GetPlatform(log.Data);

            if (!string.IsNullOrWhiteSpace(platform))
            {
                text += $" de pe {platform}";
            }

            return $"{text} numit '{log.Data["group_name"]}'";
        }

        public string BuildChatGroupJoiningLogText(PersonalLog log)
        {
            string text = $"Am intrat în grupul de chat";

            string platform = GetPlatform(log.Data);

            if (!string.IsNullOrWhiteSpace(platform))
            {
                text += $" de pe {platform}";
            }

            return $"{text} numit '{log.Data["group_name"]}'";
        }

        public string BuildChatGroupLeavingLogText(PersonalLog log)
        {
            string text = $"Am ieșit din grupul de chat";

            string platform = GetPlatform(log.Data);

            if (!string.IsNullOrWhiteSpace(platform))
            {
                text += $" de pe {platform}";
            }

            return $"{text} numit '{log.Data["group_name"]}'";
        }

        public string BuildCustomGptCreationLogText(PersonalLog log)
        {
            string text = $"Am creat un GPT personalizat";

            string gptName = GetDataValue(log.Data, "gpt_name");

            if (!string.IsNullOrWhiteSpace(gptName))
            {
                text += $" numit '{gptName}'";
            }

            string platform = GetPlatform(log.Data);

            if (!string.IsNullOrWhiteSpace(platform))
            {
                text += $" pe {platform}";
            }

            return text;
        }

        public string BuildDatingAppMatchLogText(PersonalLog log)
            => $"Am făcut match cu {log.Data["match_name"]} pe {GetPlatform(log.Data)}";

        public string BuildDeliveryReceivalLogText(PersonalLog log)
        {
            string text = $"Am primit coletul cu {log.Data["package_description"]}";

            string trackingNumber = GetDataValue(log.Data, "tracking_number");

            if (!string.IsNullOrWhiteSpace(trackingNumber))
            {
                text += $", cu numărul de urmărire {trackingNumber}";
            }

            string companyName = GetDataValue(log.Data, "company_name");

            if (!string.IsNullOrWhiteSpace(companyName))
            {
                text += $", prin {companyName}";
            }

            return text;
        }

        public string BuildDentalAppointmentLogText(PersonalLog log)
        {
            string text = $"Am avut o programare la stomatolog" + GetLocation(log.Data);

            string dentistName = GetByPerson(log.Data);

            if (!string.IsNullOrWhiteSpace(dentistName) &&
                !string.Equals(dentistName, MissingValue))
            {
                text += $", cu {dentistName}";
            }

            return text;
        }

        public string BuildDentalScalingLogText(PersonalLog log)
        {
            string text = $"Am efectuat un detartraj dentar" + GetLocation(log.Data);

            string dentistName = GetByPerson(log.Data);

            if (!string.IsNullOrWhiteSpace(dentistName) &&
                !string.Equals(dentistName, MissingValue))
            {
                text += $", cu {dentistName}";
            }

            return text;
        }

        public string BuildDeviceBatteryHealthLogText(PersonalLog log)
            => $"Sănătatea bateriei din {GetDevice(log.Data)} a fost {log.Data["battery_health_percentage"]}%";

        public string BuildDeviceBatteryLevelLogText(PersonalLog log)
            => $"Nivelul bateriei din {GetDevice(log.Data)} a fost măsurat la {log.Data["battery_level_percentage"]}%";

        public string BuildDeviceBreakingLogText(PersonalLog log)
        {
            string deviceType = GetDeviceType(log.Data);
            string text = $"S-a";

            if (deviceType.EndsWith('e'))
            {
                text += 'u';
            }

            text += $" stricat {deviceType} {log.Data["device_name"]}" + GetLocation(log.Data);

            string deviceOwnerName = GetDataValue(log.Data, "device_owner_name");

            if (!string.IsNullOrWhiteSpace(deviceOwnerName))
            {
                text += ",";

                if (deviceType.EndsWith('e'))
                {
                    text += " ale";
                }
                else if (deviceType.EndsWith('a'))
                {
                    text += " a";
                }
                else
                {
                    text += " al";
                }

                text += $" lui {deviceOwnerName}";
            }

            return text;
        }

        public string BuildDeviceChargingLogText(PersonalLog log)
            => $"Am pus la încărcat {GetDevice(log.Data)}" + GetLocation(log.Data);

        public string BuildDeviceContainerEmptyingLogText(PersonalLog log)
            => $"Am golit rezervorul de la {GetDevice(log.Data)}" + GetLocation(log.Data);

        public string BuildDeviceExternalCleaningLogText(PersonalLog log)
        {
            string text = $"Am curățat extern {GetDevice(log.Data)}";

            string cleaningMethod = GetCleaningMethod(log.Data);

            if (!string.IsNullOrWhiteSpace(cleaningMethod) &&
                !string.Equals(cleaningMethod, MissingValue))
            {
                text += $" prin {cleaningMethod}";
            }

            return text + GetLocation(log.Data);
        }

        public string BuildDeviceInternalCleaningLogText(PersonalLog log)
        {
            string text = $"Am curățat intern {GetDevice(log.Data)}";

            string cleaningMethod = GetCleaningMethod(log.Data);

            if (!string.IsNullOrWhiteSpace(cleaningMethod) &&
                !string.Equals(cleaningMethod, MissingValue))
            {
                text += $" prin {cleaningMethod}";
            }

            return text + GetLocation(log.Data);
        }

        public string BuildDeviceRepairLogText(PersonalLog log)
        {
            string deviceType = GetDeviceType(log.Data);
            string text = $"Am";

            text += $" reparat {deviceType} {log.Data["device_name"]}" + GetLocation(log.Data);

            string deviceOwnerName = GetDataValue(log.Data, "device_owner_name");

            if (!string.IsNullOrWhiteSpace(deviceOwnerName))
            {
                text += ",";

                if (deviceType.EndsWith('e'))
                {
                    text += " ale";
                }
                else if (deviceType.EndsWith('a'))
                {
                    text += " a";
                }
                else
                {
                    text += " al";
                }

                text += $" lui {deviceOwnerName}";
            }

            return text + GetLocation(log.Data);
        }

        public string BuildDeviceScreentimeMeasurementLogText(PersonalLog log)
        {
            string text = $"Timpul petrecut astăzi pe {GetDevice(log.Data)} a fost măsurat la";

            string screentimeHours = GetDataValue(log.Data, "screentime_hours");

            if (!string.IsNullOrWhiteSpace(screentimeHours))
            {
                text += $" {screentimeHours} or";

                if (screentimeHours.Equals("1"))
                {
                    text += "ă";
                }
                else
                {
                    text += "e";
                }
            }

            string screentimeMinutes = GetDataValue(log.Data, "screentime_minutes");

            if (!string.IsNullOrWhiteSpace(screentimeMinutes))
            {
                if (log.Data.ContainsKey("screentime_hours"))
                {
                    text += $" și";
                }

                text += $" {screentimeMinutes} minut";

                if (!screentimeMinutes.Equals("1"))
                {
                    text += "e";
                }
            }

            return text;
        }

        public string BuildDirectBilirubinMeasurementLogText(PersonalLog log)
            => $"Nivelul de bilirubină directă a fost măsurat la {log.Data["direct_bilirubin_level"]} {GetDataValue(log.Data, "unit", "mg/dL")}";

        public string BuildDonationLogText(PersonalLog log)
            => $"Am donat {GetBalance(log.Data)} către {GetDataValue(log.Data, "recipient")}";

        public string BuildEarwaxCleaningLogText(PersonalLog log)
        {
            string text = "Mi-am curățat ceara din urechi";

            string cleaningMethod = GetCleaningMethod(log.Data);

            if (!string.IsNullOrWhiteSpace(cleaningMethod) &&
                !string.Equals(cleaningMethod, MissingValue))
            {
                text += $" prin {cleaningMethod}";
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
                    { "AverageGrade", "media" },
                    { "Grade", "nota" },
                    { "TestGrade", "nota la test" },
                    { "ThesisGrade", "nota la teză" },
                    { "Qualifier", "calificativul" }
                },
                "nota");

            string text = $"Am obținut {gradeType} {log.Data["grade_value"]} la materia '{log.Data["subject_name"]}'";

            string subjectCode = GetDataValue(log.Data, "subject_code");

            if (!string.IsNullOrWhiteSpace(subjectCode))
            {
                text += $" ({subjectCode})";
            }

            string courseName = GetDataValue(log.Data, "course_name");

            if (!string.IsNullOrWhiteSpace(courseName))
            {
                text += $", în cadrul cursului {courseName}";
            }

            string institutionName = GetDataValue(log.Data, "institution_name");

            if (!string.IsNullOrWhiteSpace(institutionName))
            {
                text += $", la";

                string institutionDepartmentName = GetDataValue(log.Data, "institution_department_name");

                if (!string.IsNullOrWhiteSpace(institutionDepartmentName))
                {
                    text += $" {institutionDepartmentName}";

                    string institutionDepartmentSpecialisation = GetDataValue(log.Data, "institution_department_specialisation");

                    if (!string.IsNullOrWhiteSpace(institutionDepartmentSpecialisation))
                    {
                        text += $", specializarea {institutionDepartmentSpecialisation},";
                    }

                    text += $" de la";
                }

                text += $" {institutionName}";
            }

            string educationalCycleYear = GetDataValue(log.Data, "educational_cycle_year");

            if (!string.IsNullOrWhiteSpace(educationalCycleYear))
            {
                text += $", din anul {educationalCycleYear}";
            }
            else
            {
                string educationalCycleGrade = GetDataValue(log.Data, "educational_cycle_grade");

                if (!string.IsNullOrWhiteSpace(educationalCycleGrade))
                {
                    text += $", din clasa a {educationalCycleGrade}-a";
                }
            }

            string educationalCycleSemester = GetDataValue(log.Data, "educational_cycle_semester");

            if (!string.IsNullOrWhiteSpace(educationalCycleSemester))
            {
                text += $", din semestrul {educationalCycleSemester}";
            }

            return text;
        }

        public string BuildEmailExportLogText(PersonalLog log)
            => $"Am exportat toate e-mail-urile din contul de {GetPlatform(log.Data)}";

        public string BuildEmailAliasCreationLogText(PersonalLog log)
            => $"Am creat aliasul de e-mail '{log.Data["email_alias"]}' în {GetPlatform(log.Data)}";

        public string BuildEmailAliasDeletionLogText(PersonalLog log)
            => $"Am șters aliasul de e-mail '{log.Data["email_alias"]}' din {GetPlatform(log.Data)}";

        public string BuildEventTicketPurchaseLogText(PersonalLog log)
        {
            string text = $"Am cumpărat bilet";

            string ticketType = GetDataValue(log.Data, "ticket_type");

            if (!string.IsNullOrWhiteSpace(ticketType))
            {
                text += $" {ticketType}";
            }

            text += $"pentru '{log.Data["event_name"]}'";

            string eventDate = GetDataValue(log.Data, "event_date");

            if (!string.IsNullOrWhiteSpace(eventDate))
            {
                text += $" pe {eventDate}";
            }

            return text + GetLocation(log.Data);
        }

        public string BuildEyeCheckupLogText(PersonalLog log)
        {
            string text = $"Am efectuat un control oftalmologic" + GetLocation(log.Data);

            string optometristName = GetByPerson(log.Data);

            if (!string.IsNullOrWhiteSpace(optometristName) &&
                !string.Equals(optometristName, MissingValue))
            {
                text += $", cu {optometristName}";
            }

            return text;
        }

        public string BuildFireDrillLogText(PersonalLog log)
            => $"Am participat la un exercițiu de evacuare în caz de incendiu" +
                GetLocation(log.Data);

        public string BuildFurnitureCleaningLogText(PersonalLog log)
        {
            string text = $"Am curățat {GetFurnitureType(log.Data)}";

            string cleaningMethod = GetCleaningMethod(log.Data);

            if (!string.IsNullOrWhiteSpace(cleaningMethod) &&
                !string.Equals(cleaningMethod, MissingValue))
            {
                text += $", prin {cleaningMethod}";
            }

            return text + GetLocation(log.Data);
        }

        public string BuildGameAchievementUnlockLogText(PersonalLog log)
        {
            string achievementType = "achievement-ul";
            string gameName = log.Data["game_name"];
            string platform = GetDataValue(log.Data, "platform");

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
            string gameName = GetDataValue(log.Data, "game_name");
            string text = $"Am publicat un articol intitulat '{log.Data["article_title"]}' în {gameName}";

            if (string.Equals(gameName, "eRepublik"))
            {
                string newspaperName = GetDataValue(log.Data, "newspaper_name");

                if (!string.IsNullOrWhiteSpace(newspaperName))
                {
                    text += $" în ziarul '{newspaperName}'";
                }
            }

            string platform = GetPlatform(log.Data);

            if (!string.IsNullOrWhiteSpace(platform))
            {
                text += $" pe {platform}";
            }

            return text;
        }

        public string BuildGameBuildingBoughtLogText(PersonalLog log)
        {
            string text = $"Am cumpărat clădirea {log.Data["building_name"]} în {log.Data["game_name"]}";

            string platform = GetPlatform(log.Data);

            if (!string.IsNullOrWhiteSpace(platform))
            {
                text += $" pe {platform}";
            }

            return text;
        }

        public string BuildGameBuildingLevelUpgradeLogText(PersonalLog log)
        {
            string text = $"Am ridicat clădirea {log.Data["building_name"]} la nivelul {log.Data["new_level"]} în {log.Data["game_name"]}";

            string platform = GetPlatform(log.Data);

            if (!string.IsNullOrWhiteSpace(platform))
            {
                text += $" pe {platform}";
            }

            return text;
        }

        public string BuildGameConstructionLogText(PersonalLog log)
        {
            string text = $"Am construit {log.Data["construction_name"]} în {log.Data["game_name"]}";

            string platform = GetPlatform(log.Data);

            if (!string.IsNullOrWhiteSpace(platform))
            {
                text += $" pe {platform}";
            }

            return text;
        }

        public string BuildGameConstructionBeginningLogText(PersonalLog log)
        {
            string text = $"Am început să construiesc {log.Data["construction_name"]} în {log.Data["game_name"]}";

            string platform = GetPlatform(log.Data);

            if (!string.IsNullOrWhiteSpace(platform))
            {
                text += $" pe {platform}";
            }

            return text;
        }

        public string BuildGameConstructionCompletionLogText(PersonalLog log)
        {
            string text = $"Am terminat de construit {log.Data["construction_name"]} în {log.Data["game_name"]}";

            string platform = GetPlatform(log.Data);

            if (!string.IsNullOrWhiteSpace(platform))
            {
                text += $" pe {platform}";
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
                    { "Clan", "clanul" },
                    { "MilitaryUnit", "unitatea militară" },
                    { "PoliticalParty", "partidul politic" }
                },
                "ghilda"
            );

            string text = $"Am intrat în {guildType} '{log.Data["guild_name"]}' în {log.Data["game_name"]}";

            string platform = GetPlatform(log.Data);

            if (!string.IsNullOrWhiteSpace(platform))
            {
                text += $" pe {platform}";
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
                    { "Clan", "clanul" },
                    { "MilitaryUnit", "unitatea militară" },
                    { "PolititicalParty", "partidul politic" }
                },
                "ghilda"
            );

            return $"Am ieșit din {guildType} '{log.Data["party_name"]}' în {log.Data["game_name"]} pe {GetPlatform(log.Data)}";
        }

        public string BuildGameModPublishingLogText(PersonalLog log)
            => $"Am publicat mod-ul '{log.Data["mod_name"]}' pentru jocul {log.Data["game_name"]} pe {GetPlatform(log.Data)}";

        public string BuildGameOfficeTermBeginningLogText(PersonalLog log)
        {
            string text = $"Am început un mandat de {log.Data["office_name"]}";

            string officeLocation = GetDataValue(log.Data, "office_location");

            if (!string.IsNullOrWhiteSpace(officeLocation))
            {
                text += $" în {officeLocation}";
            }

            string factionName = GetDataValue(log.Data, "faction_name");

            if (!string.IsNullOrWhiteSpace(factionName))
            {
                text += $" din partea {factionName},";
            }

            text += $" în {log.Data["game_name"]}";

            string platform = GetPlatform(log.Data);

            if (!string.IsNullOrWhiteSpace(platform))
            {
                text += $" pe {platform}";
            }

            return text;
        }

        public string BuildGameOfficeTermEndingLogText(PersonalLog log)
        {
            string text = $"Am încheiat un mandat de {log.Data["office_name"]}";

            string officeLocation = GetDataValue(log.Data, "office_location");

            if (!string.IsNullOrWhiteSpace(officeLocation))
            {
                text += $" în {officeLocation}";
            }

            string factionName = GetDataValue(log.Data, "faction_name");

            if (!string.IsNullOrWhiteSpace(factionName))
            {
                text += $" din partea {factionName},";
            }

            text += $" în {log.Data["game_name"]}";

            string platform = GetPlatform(log.Data);

            if (!string.IsNullOrWhiteSpace(platform))
            {
                text += $" pe {platform}";
            }

            return text;
        }

        public string BuildGameRankUpLogText(PersonalLog log)
        {
            string text = $"Am avansat la rangul {log.Data["new_rank"]} în {log.Data["game_name"]}";

            string platform = GetPlatform(log.Data);

            if (!string.IsNullOrWhiteSpace(platform))
            {
                text += $" pe {platform}";
            }

            return text;
        }

        public string BuildGameStartedPlayingLogText(PersonalLog log)
        {
            string text = $"Am început să joc {log.Data["game_name"]}";

            string platform = GetPlatform(log.Data);

            if (!string.IsNullOrWhiteSpace(platform))
            {
                text += $" pe {platform}";
            }

            return text;
        }

        public string BuildGameLevelUpLogText(PersonalLog log)
        {
            string text = $"Am avansat la nivelul {log.Data["new_level"]} în {log.Data["game_name"]}";

            string platform = GetPlatform(log.Data);

            if (!string.IsNullOrWhiteSpace(platform))
            {
                text += $" pe {platform}";
            }

            return text;
        }

        public string BuildGarbageDisposalLogText(PersonalLog log)
            => $"Am aruncat deșeurile" + GetLocation(log.Data);

        public string BuildGettingInToBedLogText(PersonalLog log)
        {
            string text = "M-am pus în pat" + GetLocation(log.Data);

            string side = GetSide(log.Data);

            if (!string.IsNullOrWhiteSpace(side) &&
                !string.Equals(side, MissingValue))
            {
                text += $", pe partea {side}";
            }

            return text;
        }

        public string BuildGettingOutOfBedLogText(PersonalLog log)
        {
            string text = "M-am ridicat din pat" + GetLocation(log.Data);

            string side = GetSide(log.Data);

            if (!string.IsNullOrWhiteSpace(side) &&
                !string.Equals(side, MissingValue))
            {
                text += $", pe partea {side}";
            }

            return text;
        }

        public string BuildGiftReceivalLogText(PersonalLog log)
        {
            string text = $"Am primit un cadou";
            string giftOccasion = GetMappedDataValue(
                log.Data,
                "gift_occasion",
                new()
                {
                    { "Birthday", "ziua mea de naștere" },
                    { "Christmas", "Crăciun" },
                    { "Easter", "Paște" },
                    { "NameDay", "onomastică" },
                    { "RelationshipAnniversary", "aniversare a relației" },
                    { "ValentinesDay", "Ziua Îndrăgostiților" }
                });

            if (!string.IsNullOrWhiteSpace(giftOccasion))
            {
                text += $" de {giftOccasion}";
            }

            string giverName = GetByPerson(log.Data);

            if (!string.IsNullOrWhiteSpace(giverName) &&
                !string.Equals(giverName, MissingValue))
            {
                text += $" de la {giverName}";
            }

            string giftContent = GetDataValue(log.Data, "gift_content");

            if (!string.IsNullOrWhiteSpace(giftContent))
            {
                text += $": {giftContent}";
            }

            return text;
        }

        public string BuildGitContributionsMeasurementLogText(PersonalLog log)
            => $"Am avut {log.Data["contributions_count"]} de contribuții pe {GetPlatform(log.Data)}";

        public string BuildGitReleaseLogText(PersonalLog log)
            => $"Am lansat versiunea '{GetDataValue(log.Data, "version")}' pentru repozitoriul '{GetDataValue(log.Data, "repository")}' pe {GetPlatform(log.Data)}";

        public string BuildGitRepositoryArchivalLogText(PersonalLog log)
            => $"Am arhivat repozitoriul '{GetDataValue(log.Data, "repository")}' de pe {GetPlatform(log.Data)}";

        public string BuildGitRepositoryDeletionLogText(PersonalLog log)
            => $"Am șters repozitoriul '{GetDataValue(log.Data, "repository")}' de pe {GetPlatform(log.Data)}";

        public string BuildGitRepositoryCreationLogText(PersonalLog log)
            => $"Am creat repozitoriul '{GetDataValue(log.Data, "repository")}' pe {GetPlatform(log.Data)}";

        public string BuildGitRepositoryRenameLogText(PersonalLog log)
            => $"Am redenumit repozitoriul '{GetDataValue(log.Data, "old_repository")}' de pe {GetPlatform(log.Data)} în '{GetDataValue(log.Data, "new_repository")}'";

        public string BuildGoingToSleepLogText(PersonalLog log)
        {
            string text = "M-am culcat" + GetLocation(log.Data);

            string side = GetSide(log.Data);

            if (!string.IsNullOrWhiteSpace(side) &&
                !string.Equals(side, MissingValue))
            {
                text += $", pe partea {side}";
            }

            return text;
        }

        public string BuildGoingToTheChurchLogText(PersonalLog log)
            => "Am fost la biserică" + GetLocation(log.Data);

        public string BuildGoingToTheToiletLogText(PersonalLog log)
            => "Am mers la toaletă" + GetLocation(log.Data);

        public string BuildGraduationCeremonyAttendanceLogText(PersonalLog log)
        {
            string text =
                $"Am participat la ceremonia de absolvire a lui " +
                GetLocalisedValue(log.Data, "graduate_name") +
                GetLocation(log.Data);

            string degreeLevel = GetMappedDataValue(
                log.Data,
                "degree_level",
                new()
                {
                    { "Bachelor", "licență" },
                    { "Master", "masterat" },
                    { "Doctorate", "doctorat" }
                });

            if (!string.IsNullOrWhiteSpace(degreeLevel))
            {
                text += $", pentru finalizarea nivelului {degreeLevel}";

                string institutionName = GetDataValue(log.Data, "institution_name");

                if (!string.IsNullOrWhiteSpace(institutionName))
                {
                    text += $" la {institutionName}";

                }
            }

            return text;
        }

        public string BuildGraduationCeremonyParticipationLogText(PersonalLog log)
        {
            string text = $"Am participat la ceremonia mea de absolvire";

            string degreeLevel = GetMappedDataValue(
                log.Data,
                "degree_level",
                new()
                {
                    { "Bachelor", "licență" },
                    { "Master", "masterat" },
                    { "Doctorate", "doctorat" }
                });

            if (!string.IsNullOrWhiteSpace(degreeLevel))
            {
                text += $" a nivelului {degreeLevel}";

                string institutionName = GetDataValue(log.Data, "institution_name");

                if (!string.IsNullOrWhiteSpace(institutionName))
                {
                    text += $" la {institutionName}";

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
                text += $"Mi-a fost";
            }
            else
            {
                text += $"Mi-am";
            }

            text += $" tuns {GetHairType(log.Data)}";
            text += GetLocation(log.Data);

            if (hasHairdresser)
            {
                text += $", de către {hairdresserName}";
            }

            return text;
        }

        public string BuildHairTrimmingLogText(PersonalLog log)
            => $"Mi-am scurtat {GetHairType(log.Data)}" + GetLocation(log.Data);

        public string BuildHdlCholesterolMeasurementLogText(PersonalLog log)
            => $"Nivelul de HDL Colesterol a fost măsurat la {log.Data["hdl_cholesterol_level"]} {GetDataValue(log.Data, "unit", "mg/dL")}";

        public string BuildHeartRateMeasurementLogText(PersonalLog log)
        {
            string unit = GetDataValue(log.Data, "unit", "bpm");
            string text = $"Ritmul cardiac a fost măsurat la {log.Data["heart_rate"]} {unit}";

            string device = GetDevice(log.Data);

            if (!string.IsNullOrWhiteSpace(device) &&
                !string.Equals(device, MissingValue))
            {
                text += $", utilizând {device}";
            }

            return text;
        }

        public string BuildIndirectBilirubinMeasurementLogText(PersonalLog log)
            => $"Nivelul de bilirubină indirectă a fost măsurat la {log.Data["indirect_bilirubin_level"]} {GetDataValue(log.Data, "unit", "mg/dL")}";

        public string BuildInternshipApplicationSubmissionLogText(PersonalLog log)
        {
            string internshipType = "internship";

            string period = GetDataValue(log.Data, "period");

            if (!string.IsNullOrWhiteSpace(period))
            {
                internshipType = $"{period} {internshipType}";
            }

            string text = $"Am trimis o aplicare de {internshipType} la {log.Data["company_name"]}";

            string contactPersonName = GetDataValue(log.Data, "contact_person_name");

            if (!string.IsNullOrWhiteSpace(contactPersonName))
            {
                text += $" către {contactPersonName}";
            }

            string positionName = GetDataValue(log.Data, "position_name");

            if (!string.IsNullOrWhiteSpace(positionName))
            {
                text += $", pentru o poziție de {positionName}";
            }

            return text;
        }

        public string BuildKinetotherapySessionLogText(PersonalLog log)
        {
            string text = $"Am avut o ședință de kinetoterapie" + GetLocation(log.Data);

            string therapistName = GetByPerson(log.Data);

            if (!string.IsNullOrWhiteSpace(therapistName) &&
                !string.Equals(therapistName, MissingValue))
            {
                text += $", cu {therapistName}";
            }

            return text;
        }

        public string BuildLdlCholesterolMeasurementLogText(PersonalLog log)
            => $"Nivelul de LDL Colesterol a fost măsurat la {log.Data["ldl_cholesterol_level"]} {GetDataValue(log.Data, "unit", "mg/dL")}";

        public string BuildMagnesiumLevelMeasurementLogText(PersonalLog log)
            => $"Nivelul de magneziu a fost măsurat la {log.Data["magnesium_level"]} {GetDataValue(log.Data, "unit", "mg/dL")}";

        public string BuildMealVoucherCardCreditationLogText(PersonalLog log)
            => $"Cardul de bonuri de masă a fost creditat cu {GetBalance(log.Data)}";

        public string BuildMedicationIntakeLogText(PersonalLog log)
        {
            string medicationType;
            string text = $"Am luat";

            if (IsDataValuePlural(log.Data, "medication_name"))
            {
                medicationType = GetMedicationType(log.Data, true);

                if (medicationType.EndsWith('i'))
                {
                    text += " următorii";
                }
                else
                {
                    text += " următoarele";
                }

                text += $" {medicationType}";
            }
            else
            {
                medicationType = GetMedicationType(log.Data, false);

                text += $" următorul {medicationType}";
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
                    { "Alliance", "alianță" },
                    { "DiplomaticRelations", "relații diplomatice" },
                    { "NonAggressionPact", "pact de neagresiune" },
                    { "TradeAgreement", "acord comercial" }
                },
                "relație externă");

            return $"Am trimis o solicitare de {relationTypeWord} către micronațiunea {log.Data["target_micronation_name"]} din partea micronațiunii {log.Data["source_micronation_name"]}";
        }

        public string BuildMicronationExternalRelationsRequestReceivalLogText(PersonalLog log)
        {
            string relationTypeWord = GetMappedDataValue(
                log.Data,
                "relation_type",
                new()
                {
                    { "Alliance", "alianță" },
                    { "DiplomaticRelations", "relații diplomatice" },
                    { "NonAggressionPact", "pact de neagresiune" },
                    { "TradeAgreement", "acord comercial" }
                },
                "relație externă");

            return $"Am primit o solicitare de {relationTypeWord} din partea micronațiunii {log.Data["source_micronation_name"]} către micronațiunea {log.Data["target_micronation_name"]}";
        }

        public string BuildMicronationExternalRelationsRequestRejectionLogText(PersonalLog log)
        {
            string relationTypeWord = GetMappedDataValue(
                log.Data,
                "relation_type",
                new()
                {
                    { "Alliance", "alianță" },
                    { "DiplomaticRelations", "relații diplomatice" },
                    { "NonAggressionPact", "pact de neagresiune" },
                    { "TradeAgreement", "acord comercial" }
                },
                "relație externă");

            string text = $"Solicitarea de {relationTypeWord} din partea micronațiunii {log.Data["source_micronation_name"]} către {log.Data["target_micronation_name"]}";

            string requestDate = GetDataValue(log.Data, "request_date");

            if (!string.IsNullOrWhiteSpace(requestDate))
            {
                text += $", trimisă la {requestDate},";
            }

            return $"{text} a fost respinsă";
        }

        public string BuildMicronationExternalRelationsEstablishmentLogText(PersonalLog log)
        {
            string relationshipTypeWord = GetMappedDataValue(
                log.Data,
                "relation_type",
                new()
                {
                    { "Alliance", "o alianța" },
                    { "DiplomaticRelations", "relații diplomatice" },
                    { "NonAggressionPact", "un pact de neagresiune" },
                    { "TradeAgreement", "un acord comercial" }
                },
                "relație externă");

            string text = $"Am stabilit o {relationshipTypeWord} între micronațiunile {log.Data["source_micronation_name"]} și {log.Data["target_micronation_name"]}";

            string requestDate = GetDataValue(log.Data, "request_date");

            if (!string.IsNullOrWhiteSpace(requestDate))
            {
                text += $", în urma solicitării din {requestDate}";
            }

            return text;
        }

        public string BuildMicronationFoundingLogText(PersonalLog log)
            => $"Am fondat micronațiunea {GetDataValue(log.Data, "name")}";

        public string BuildMicronationLegalActIssuanceLogText(PersonalLog log)
        {
            string legalActTypeWord = GetMappedDataValue(
                log.Data,
                "legal_act_type",
                new()
                {
                    { "NucalDecree", "decretul nucal" },
                    { "PalatinalDecree", "decretul palatinal" },
                    { "PrefecturalDecree", "decretul prefectural" },
                    { "VoivodalDecree", "decretul voievodal" }
                },
                "actul legal");

            string text = $"Am emis {legalActTypeWord} '{log.Data["legal_act_name"]}'";

            if (log.Data.ContainsKey("administrative_unit_type"))
            {
                string administrativeUnitTypeWord = GetMappedDataValue(
                    log.Data,
                    "administrative_unit_type",
                    new()
                    {
                        { "Castle", "cetatea" },
                        { "City", "orașul" },
                        { "Town", "orășelul" },
                        { "Village", "satul" },
                        { "Land", "ținutul" },
                        { "County", "județul" },
                        { "District", "districtul" },
                        { "Zhupanate", "jupânatul" },
                        { "Voivodeship", "voievodatul" },
                        { "Prefecture", "prefectura" }
                    });

                text += $" în {administrativeUnitTypeWord} {log.Data["administrative_unit_name"]} din";
            }
            else
            {
                text += " în";
            }

            text += $" micronațiunea {log.Data["micronation_name"]}";

            return text;
        }

        public string BuildMicronationNameChangeLogText(PersonalLog log)
            => $"Am schimbat numele micronațiunii {GetDataValue(log.Data, "old_name")} în {GetDataValue(log.Data, "new_name")}";

        public string BuildMicronationSettlementFoundingLogText(PersonalLog log)
        {
            string settlementType = GetMappedDataValue(
                log.Data,
                "settlement_type",
                new()
                {
                    { "Castle", "cetatea" },
                    { "City", "orașul" },
                    { "Town", "orășelul" },
                    { "Village", "satul" }
                },
                "așezarea");

            string text = $"Am fondat {settlementType} {log.Data["settlement_name"]}";

            string administrativeUnitName = GetDataValue(log.Data, "administrative_unit_name");
            if (!string.IsNullOrWhiteSpace(administrativeUnitName))
            {
                string administrativeUnitType = GetMappedDataValue(
                    log.Data,
                    "administrative_unit_type",
                    new()
                    {
                        { "Land", "ținutul" },
                        { "County", "județul" },
                        { "District", "districtul" },
                        { "Zhupanate", "jupânatul" },
                        { "Voivodeship", "voievodatul" },
                        { "Prefecture", "prefectura" }
                    });

                text += $" în {administrativeUnitType} {administrativeUnitName},";
            }

            text += $" în micronațiunea {log.Data["micronation_name"]}";

            return text;
        }

        public string BuildMicronationSettlementRankDowngradeLogText(PersonalLog log)
        {
            string oldRank = GetMappedDataValue(
                log.Data,
                "old_rank",
                new()
                {
                    { "Castle", "cetatea" },
                    { "City", "orașul" },
                    { "Town", "orășelul" },
                    { "Village", "satul" }
                },
                "așezarea");

            string newRank = GetMappedDataValue(
                log.Data,
                "old_rank",
                new()
                {
                    { "Castle", "cetate" },
                    { "City", "oraș" },
                    { "Town", "orășel" },
                    { "Village", "sat" }
                });

            return $"Am retrogradat {oldRank} {log.Data["settlement_name"]} din micronațiunea {log.Data["micronation_name"]} la rangul de {newRank}";
        }

        public string BuildMicronationSettlementRankUpgradeLogText(PersonalLog log)
        {
            string oldRank = GetMappedDataValue(
                log.Data,
                "old_rank",
                new()
                {
                    { "Castle", "cetatea" },
                    { "City", "orașul" },
                    { "Town", "orășelul" },
                    { "Village", "satul" }
                },
                "așezarea");

            string newRank = GetMappedDataValue(
                log.Data,
                "old_rank",
                new()
                {
                    { "Castle", "cetate" },
                    { "City", "oraș" },
                    { "Town", "orășel" },
                    { "Village", "sat" }
                });

            return $"Am promovat {oldRank} {log.Data["settlement_name"]} din micronațiunea {log.Data["micronation_name"]} la rangul de {newRank}";
        }

        public string BuildMovieBeginningLogText(PersonalLog log)
        {
            string text = $"Am început să vizionez filmul {log.Data["movie_name"]}";

            string platform = GetPlatform(log.Data);

            if (!string.IsNullOrWhiteSpace(platform))
            {
                text += $" pe {platform}";
            }

            return text + GetLocation(log.Data);
        }

        public string BuildMovieCompletionLogText(PersonalLog log)
        {
            string text = $"Am terminat de vizionat filmul {log.Data["movie_name"]}";

            string platform = GetPlatform(log.Data);

            if (!string.IsNullOrWhiteSpace(platform))
            {
                text += $" pe {platform}";
            }

            return text + GetLocation(log.Data);
        }

        public string BuildMovieWatchingLogText(PersonalLog log)
        {
            string text = $"Am vizionat filmul '{log.Data["movie_name"]}'";

            string platform = GetPlatform(log.Data);

            if (!string.IsNullOrWhiteSpace(platform))
            {
                text += $" pe {platform}";
            }

            return text + GetLocation(log.Data);
        }

        public string BuildNailCuttingLogText(PersonalLog log)
            => $"Mi-am tăiat {GetNailsType(log.Data)}" + GetLocation(log.Data);

        public string BuildObjectSaleLogText(PersonalLog log)
        {
            string text = $"Am vândut {log.Data["object_name"]}";

            string platform = GetPlatform(log.Data);

            if (!string.IsNullOrWhiteSpace(platform))
            {
                text += $" pe {platform}";
            }

            if (log.Data.ContainsKey("price_amount"))
            {
                text += $" cu {GetBalance(log.Data)}";
            }

            return text;
        }

        public string BuildOnlineReviewSubmissionLogText(PersonalLog log)
        {
            string text = $"Am trimis un review cu";

            string starsCount = GetDataValue(log.Data, "stars_count");

            if (!string.IsNullOrWhiteSpace(starsCount))
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

            return text + $" pe {GetPlatform(log.Data)} pentru {log.Data["subject_name"]}";
        }

        public string BuildOnlineStorePurchaseLogText(PersonalLog log)
        {
            string text = $"Am cumpărat {log.Data["product_name"]} pe {GetPlatform(log.Data)}";

            if (log.Data.ContainsKey("price_amount"))
            {
                text += $", cu {GetBalance(log.Data)}";
            }

            return text;
        }

        public string BuildPetAdoptionLogText(PersonalLog log)
            => $"Am adoptat {GetPetType(log.Data, true)} {GetPet(log.Data)}" +
                GetLocation(log.Data);

        public string BuildPetBathingLogText(PersonalLog log)
            => $"I-am făcut baie lui {GetPet(log.Data)}" +
                GetLocation(log.Data);

        public string BuildPetBrushingLogText(PersonalLog log)
            => $"I-am periat blana lui {GetPet(log.Data)}" +
                GetLocation(log.Data);

        public string BuildPetLitterCleaningLogText(PersonalLog log)
            => $"Am curățat litiera de {GetPetType(log.Data, false, true)}" +
                GetLocation(log.Data);

        public string BuildPetLitterEmptyingLogText(PersonalLog log)
            => $"Am golit litiera de {GetPetType(log.Data, false, true)}" +
                GetLocation(log.Data);

        public string BuildPetLitterRefillLogText(PersonalLog log)
            => $"Am reumplut litiera de {GetPetType(log.Data, false, true)}" +
                GetLocation(log.Data);

        public string BuildPetMedicationAdministrationLogText(PersonalLog log)
        {
            string medicationType;
            string text = $"I-am administrat";

            if (IsDataValuePlural(log.Data, "medication_name"))
            {
                medicationType = GetMedicationType(log.Data, true);

                if (medicationType.EndsWith('i'))
                {
                    text += " următorii";
                }
                else
                {
                    text += " următoarele";
                }

                text += $" {medicationType}";
            }
            else
            {
                medicationType = GetMedicationType(log.Data, false);

                text += $" următorul {medicationType}";
            }

            return $"{text} lui {GetPet(log.Data)}: {GetLocalisedValue(log.Data, "medication_name")}";
        }

        public string BuildPetNailsTrimmingLogText(PersonalLog log)
            => $"I-am tăiat ghearele lui {GetPet(log.Data)}" +
                GetLocation(log.Data);

        public string BuildPetWeightMeasurementLogText(PersonalLog log)
        {
            string unit = GetDataValue(log.Data, "unit", "kg");
            string text = $"Greutatea corporală a lui {GetPet(log.Data)} a fost măsurată la {log.Data["pet_weight"]} {unit}";

            string scaleName = GetDataValue(log.Data, "scale_name");

            if (!string.IsNullOrWhiteSpace(scaleName))
            {
                text += $", pe cântarul {scaleName}";
            }

            return text + GetLocation(log.Data);
        }

        public string BuildPhysiotherapySessionLogText(PersonalLog log)
        {
            string text = $"Am avut o ședință de fizioterapie" + GetLocation(log.Data);

            string therapistName = GetByPerson(log.Data);

            if (!string.IsNullOrWhiteSpace(therapistName) &&
                !string.Equals(therapistName, MissingValue))
            {
                text += $", cu {therapistName}";
            }

            return text;
        }

        public string BuildPlantWateringLogText(PersonalLog log)
            => $"Am udat {GetPlantType(log.Data, true, true)}" +
                GetLocation(log.Data);

        public string BuildProductKeyActivationLogText(PersonalLog log)
        {
            string text = "Am activat cheia de produs";

            if (log.Data.ContainsKey("product_key"))
            {
                 text += $" '{GetDataValue(log.Data, "product_key")}'";
            }

            return $"{text} pentru {log.Data["product_name"]} pe {GetPlatform(log.Data)}";
        }

        public string BuildPsychotherapySessionLogText(PersonalLog log)
        {
            string text = $"Am avut o ședință de psihoterapie" + GetLocation(log.Data);

            string therapistName = GetByPerson(log.Data);

            if (!string.IsNullOrWhiteSpace(therapistName) &&
                !string.Equals(therapistName, MissingValue))
            {
                text += $", cu {therapistName}";
            }

            return text;
        }

        public string BuildPublicIpAddressMeasurementLogText(PersonalLog log)
            => $"Adresa IP publică a fost {log.Data["ip_address"]}" + GetLocation(log.Data);

        public string BuildRestaurantVisitLogText(PersonalLog log)
            => $"Am fost la restaurant" + GetLocation(log.Data);

        public string BuildSaunaSessionLogText(PersonalLog log)
        {
            string text = $"Am fost la saună";

            string durationMinutes = GetDataValue(log.Data, "duration_minutes");

            if (!string.IsNullOrWhiteSpace(durationMinutes))
            {
                text += $" timp de {durationMinutes} minut";

                if (durationMinutes != "1")
                {
                    text += "e";
                }
            }

            return text + GetLocation(log.Data);
        }

        public string BuildSeriesBeginningLogText(PersonalLog log)
        {
            string text = $"Am început să vizionez serialul '{log.Data["series_name"]}'";

            string platform = GetPlatform(log.Data);

            if (!string.IsNullOrWhiteSpace(platform))
            {
                text += $" pe {platform}";
            }

            return text + GetLocation(log.Data);
        }

        public string BuildSeriesCompletionLogText(PersonalLog log)
        {
            string text = $"Am terminat de vizionat serialul '{log.Data["series_name"]}'";

            string platform = GetPlatform(log.Data);

            if (!string.IsNullOrWhiteSpace(platform))
            {
                text += $" pe {platform}";
            }

            return text + GetLocation(log.Data);
        }

        public string BuildSeriesEpisodeBeginningLogText(PersonalLog log)
        {
            string text = $"Am început să vizionez episodul {log.Data["episode_number"]}";

            string episodeName = GetDataValue(log.Data, "episode_name");

            if (!string.IsNullOrWhiteSpace(episodeName))
            {
                text += $" '{episodeName}'";
            }

            string seasonNumber = GetDataValue(log.Data, "season_number");

            if (!string.IsNullOrWhiteSpace(seasonNumber))
            {
                text += $" din sezonul {seasonNumber}";
            }

            string seriesName = GetDataValue(log.Data, "series_name");

            if (!string.IsNullOrWhiteSpace(seriesName))
            {
                text += $" din '{seriesName}'";
            }

            string platform = GetPlatform(log.Data);

            if (!string.IsNullOrWhiteSpace(platform))
            {
                text += $" pe {platform}";
            }

            return text + GetLocation(log.Data);
        }

        public string BuildSeriesEpisodeCompletionLogText(PersonalLog log)
        {
            string text = $"Am terminat de vizionat episodul {log.Data["episode_number"]}";

            string episodeName = GetDataValue(log.Data, "episode_name");

            if (!string.IsNullOrWhiteSpace(episodeName))
            {
                text += $" '{episodeName}'";
            }

            string seasonNumber = GetDataValue(log.Data, "season_number");

            if (!string.IsNullOrWhiteSpace(seasonNumber))
            {
                text += $" din sezonul {seasonNumber}";
            }

            string seriesName = GetDataValue(log.Data, "series_name");

            if (!string.IsNullOrWhiteSpace(seriesName))
            {
                text += $" din '{seriesName}'";
            }

            string platform = GetPlatform(log.Data);

            if (!string.IsNullOrWhiteSpace(platform))
            {
                text += $" pe {platform}";
            }

            return text + GetLocation(log.Data);
        }

        public string BuildSeriesEpisodeWatchingLogText(PersonalLog log)
        {
            string text = $"Am vizionat episodul {log.Data["episode_number"]}";

            string episodeName = GetDataValue(log.Data, "episode_name");

            if (!string.IsNullOrWhiteSpace(episodeName))
            {
                text += $" '{episodeName}'";
            }

            string seasonNumber = GetDataValue(log.Data, "season_number");

            if (!string.IsNullOrWhiteSpace(seasonNumber))
            {
                text += $" din sezonul {seasonNumber}";
            }

            string seriesName = GetDataValue(log.Data, "series_name");

            if (!string.IsNullOrWhiteSpace(seriesName))
            {
                text += $" din '{seriesName}'";
            }

            string platform = GetPlatform(log.Data);

            if (!string.IsNullOrWhiteSpace(platform))
            {
                text += $" pe {platform}";
            }

            return text + GetLocation(log.Data);
        }

        public string BuildSeriesSeasonBeginningLogText(PersonalLog log)
        {
            string text = $"Am început să vizionez sezonul {log.Data["season_number"]}";

            string seriesName = GetDataValue(log.Data, "series_name");

            if (!string.IsNullOrWhiteSpace(seriesName))
            {
                text += $" din '{seriesName}'";
            }

            string platform = GetPlatform(log.Data);

            if (!string.IsNullOrWhiteSpace(platform))
            {
                text += $" pe {platform}";
            }

            return text + GetLocation(log.Data);
        }

        public string BuildSeriesSeasonCompletionLogText(PersonalLog log)
        {
            string text = $"Am terminat sezonul {log.Data["season_number"]}";

            string seriesName = GetDataValue(log.Data, "series_name");

            if (!string.IsNullOrWhiteSpace(seriesName))
            {
                text += $" din '{seriesName}'";
            }

            string platform = GetPlatform(log.Data);

            if (!string.IsNullOrWhiteSpace(platform))
            {
                text += $" pe {platform}";
            }

            return text + GetLocation(log.Data);
        }

        public string BuildShavingLogText(PersonalLog log)
        {
            string text = string.Empty;

            if (log.Data.ContainsKey("stylist_name"))
            {
                text = "Mi-a fost rasă";
            }
            else
            {
                text = "Mi-am ras";
            }

            text += $" {GetHairType(log.Data)}";

            string stylistName = GetDataValue(log.Data, "stylist_name");

            if (!string.IsNullOrWhiteSpace(stylistName))
            {
                text += $" de către {stylistName}";
            }

            return text + GetLocation(log.Data);
        }

        public string BuildShowerBeginningLogText(PersonalLog log)
            => "Am început să fac duș" + GetLocation(log.Data);

        public string BuildShowerCompletionLogText(PersonalLog log)
            => "Am terminat de făcut duș" + GetLocation(log.Data);

        public string BuildShowerTakingLogText(PersonalLog log)
        {
            string text = $"Am făcut duș";

            string durationMinutes = GetDataValue(log.Data, "duration_minutes");

            if (!string.IsNullOrWhiteSpace(durationMinutes))
            {
                text += $" timp de {durationMinutes} minute";
            }

            return text + GetLocation(log.Data);
        }

        public string BuildStepCountMeasurementLogText(PersonalLog log)
        {
            string text = $"Am umblat {log.Data["step_count"]} de pași";

            string distanceMetres = GetDataValue(log.Data, "distance_metres");

            if (!string.IsNullOrWhiteSpace(distanceMetres))
            {
                text += $", pe o distanță de {distanceMetres} de metri";
            }

            string caloriesBurned = GetDataValue(log.Data, "calories_burned");

            if (!string.IsNullOrWhiteSpace(caloriesBurned))
            {
                text += $", arzând {caloriesBurned} de kilocalorii";
            }

            string device = GetDevice(log.Data);

            if (!string.IsNullOrWhiteSpace(device) &&
                !string.Equals(device, MissingValue))
            {
                text += $", conform măsurătorilor făcute de {device}";
            }

            return text;
        }

        public string BuildSwimmingActivityLogText(PersonalLog log)
            => $"Am fost la înnot" + GetLocation(log.Data);

        public string BuildTeethBrushingLogText(PersonalLog log)
        {
            string text = $"M-am spălat pe dinți";

            string durationMinutes = GetDataValue(log.Data, "duration_minutes");

            if (!string.IsNullOrWhiteSpace(durationMinutes))
            {
                text += $" timp de {durationMinutes} minut";

                if (durationMinutes != "1")
                {
                    text += "e";
                }
            }

            return text + GetLocation(log.Data);
        }

        public string BuildTheatricalPlayAttendanceLogText(PersonalLog log)
            => $"Am fost la piesa de teatru '{log.Data["play_name"]}'" + GetLocation(log.Data);

        public string BuildTollPaymentLogText(PersonalLog log)
        {
            string text = $"Am plătit taxa de drum";

            string providerName = GetDataValue(log.Data, "provider_name");

            if (!string.IsNullOrWhiteSpace(providerName))
            {
                text += $" către {providerName}";
            }

            string tollLocation = GetDataValue(log.Data, "toll_location");

            if (!string.IsNullOrWhiteSpace(tollLocation))
            {
                text += $" pentru {tollLocation}";
            }

            string vehicleRegistrationNumber = GetDataValue(log.Data, "vehicle_registration_number");

            if (!string.IsNullOrWhiteSpace(vehicleRegistrationNumber))
            {
                text += $" pentru vehiculul cu numărul de înmatriculare {vehicleRegistrationNumber}";
            }

            string costAmount = GetDataValue(log.Data, "cost_amount");

            if (!string.IsNullOrWhiteSpace(costAmount))
            {
                text += $", în valoare de {GetBalance(log.Data)}";
            }

            return text;
        }

        public string BuildTotalBilirubinMeasurementLogText(PersonalLog log)
            => $"Nivelul de bilirubină totală a fost măsurat la {log.Data["total_bilirubin_level"]} {GetDataValue(log.Data, "unit", "mg/dL")}" +
                GetLocation(log.Data);

        public string BuildTotalCholesterolMeasurementLogText(PersonalLog log)
        {
            string unit = "mg/dL";

            string unitValue = GetDataValue(log.Data, "unit");

            if (!string.IsNullOrWhiteSpace(unitValue))
            {
                unit = unitValue;
            }

            return $"Nivelul de colesterol total a fost măsurat la {log.Data["total_cholesterol_level"]} {unit}" +
                GetLocation(log.Data);
        }

        public string BuildTreePlantingLogText(PersonalLog log)
        {
            string text = $"Am plantat";

            string treesCount = GetDataValue(log.Data, "trees_count", "1");
            string treeSpecies;

            if (treesCount.Equals("1"))
            {
                text += " un";

                treeSpecies = GetMappedDataValue(
                    log.Data,
                    "tree_species",
                    new Dictionary<string, string>
                    {
                        { "Oak", "stejar" },
                        { "Pine", "brad" },
                        { "Maple", "arțar" },
                        { "Birch", "mesteacăn" },
                        { "Cherry", "cireș" },
                        { "Apple", "măr" },
                        { "Walnut", "nuc" },
                        { "Willow", "salcie" }
                    },
                    "copac");
            }
            else
            {
                text += $" {treesCount}";

                treeSpecies = GetMappedDataValue(
                    log.Data,
                    "tree_species",
                    new Dictionary<string, string>
                    {
                        { "Oak", "stejari" },
                        { "Pine", "brazi" },
                        { "Maple", "arțari" },
                        { "Birch", "mesteacăni" },
                        { "Cherry", "cireși" },
                        { "Apple", "meri" },
                        { "Walnut", "nuci" },
                        { "Willow", "salcii" }
                    },
                    "copaci");
            }

            return $"{text} {treeSpecies}" + GetLocation(log.Data);
        }

        public string BuildUtilityBillPaymentLogText(PersonalLog log)
        {
            string utilityType = GetMappedDataValue(
                log.Data,
                "utility_type",
                new Dictionary<string, string>
                {
                    { "Electricity", "curent" },
                    { "Gas", "gaz" },
                    { "InternetAndTV", "internet și cablu TV" },
                    { "Water", "apă" }
                },
                "utilitate"
            );

            string text = $"Am plătit factura de {utilityType} la {log.Data["provider_name"]}";

            string supplyPointNumber = GetDataValue(log.Data, "supply_point_number");

            if (!string.IsNullOrWhiteSpace(supplyPointNumber))
            {
                text += $" pentru locuința cu numărul locului de consum {supplyPointNumber}";
            }

            text += GetLocation(log.Data);

            if (log.Data.ContainsKey("cost_amount"))
            {
                text += $", în valoare de {GetBalance(log.Data)}";
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
                    { "Electricity", "curent" },
                    { "Gas", "gaz" },
                    { "InternetAndTV", "internet și cablu TV" },
                    { "Water", "apă" }
                },
                "utilitate"
            );

            string text = $"Am citit indexul contorului de {utilityType}";

            string supplyPointNumber = GetDataValue(log.Data, "supply_point_number");

            if (!string.IsNullOrWhiteSpace(supplyPointNumber))
            {
                text += $", pentru locuința cu numărul locului de consum {supplyPointNumber}";
            }

            text += GetLocation(log.Data);

            string indexValue = GetDataValue(log.Data, "index_value");

            if (!string.IsNullOrWhiteSpace(indexValue))
            {
                text += $", obținând valoarea {indexValue}";
            }

            return text;
        }

        public string BuildVacuumCleaningLogText(PersonalLog log)
            => $"Am aspirat" + GetLocation(log.Data);

        public string BuildVehicleFluidChangingLogText(PersonalLog log)
        {
            string text = $"A fost înlocuit complet {GetFluidType(log.Data, true)} din";

            if (IsDataValuePresent(log.Data, "vehicle_model") ||
                IsDataValuePresent(log.Data, "vehicle_name") ||
                IsDataValuePresent(log.Data, "vehicle_registration_number"))
            {
                text += $" {GetVehicleType(log.Data, true)}";
            }
            else
            {
                text += $" {GetVehicleType(log.Data, false)}";
            }

            string vehicleName = GetDataValue(log.Data, "vehicle_name");

            if (!string.IsNullOrWhiteSpace(vehicleName))
            {
                text += $" '{vehicleName}'";
            }

            string vehicleModel = GetDataValue(log.Data, "vehicle_model");

            if (!string.IsNullOrWhiteSpace(vehicleModel))
            {
                text += $" {vehicleModel}";
            }

            string vehicleRegistrationNumber = GetDataValue(log.Data, "vehicle_registration_number");

            if (!string.IsNullOrWhiteSpace(vehicleRegistrationNumber))
            {
                text += $" cu numărul de înmatriculare '{vehicleRegistrationNumber}'";
            }

            text += GetLocation(log.Data);

            string mechanicName = GetByPerson(log.Data);

            if (!string.IsNullOrWhiteSpace(mechanicName) &&
                !string.Equals(mechanicName, MissingValue))
            {
                text += $", de către {mechanicName}";
            }

            return text;
        }

        public string BuildVehicleFluidRefillingLogText(PersonalLog log)
        {
            string text = $"A fost completat {GetFluidType(log.Data, true)} în";

            if (IsDataValuePresent(log.Data, "vehicle_model") ||
                IsDataValuePresent(log.Data, "vehicle_name") ||
                IsDataValuePresent(log.Data, "vehicle_registration_number"))
            {
                text += $" {GetVehicleType(log.Data, true)}";
            }
            else
            {
                text += $" {GetVehicleType(log.Data, false)}";
            }

            string vehicleName = GetDataValue(log.Data, "vehicle_name");

            if (!string.IsNullOrWhiteSpace(vehicleName))
            {
                text += $" '{vehicleName}'";
            }

            string vehicleModel = GetDataValue(log.Data, "vehicle_model");

            if (!string.IsNullOrWhiteSpace(vehicleModel))
            {
                text += $" {vehicleModel}";
            }

            string vehicleRegistrationNumber = GetDataValue(log.Data, "vehicle_registration_number");

            if (!string.IsNullOrWhiteSpace(vehicleRegistrationNumber))
            {
                text += $" cu numărul de înmatriculare '{vehicleRegistrationNumber}'";
            }

            text += GetLocation(log.Data);

            string mechanicName = GetByPerson(log.Data);

            if (!string.IsNullOrWhiteSpace(mechanicName) &&
                !string.Equals(mechanicName, MissingValue))
            {
                text += $", de către {mechanicName}";
            }

            return text;
        }

        public string BuildVehicleMileageMeasurementLogText(PersonalLog log)
        {
            string text = "Distanța totală parcursă de";
            string vehicleType;

            if (IsDataValuePresent(log.Data, "vehicle_model") ||
                IsDataValuePresent(log.Data, "vehicle_name") ||
                IsDataValuePresent(log.Data, "vehicle_registration_number"))
            {
                vehicleType = GetVehicleType(log.Data, true);
            }
            else
            {
                vehicleType = GetVehicleType(log.Data, false);
            }

            text += $" {vehicleType}";

            string vehicleName = GetDataValue(log.Data, "vehicle_name");

            if (!string.IsNullOrWhiteSpace(vehicleName))
            {
                text += $" '{vehicleName}'";
            }

            string vehicleModel = GetDataValue(log.Data, "vehicle_model");

            if (!string.IsNullOrWhiteSpace(vehicleModel))
            {
                text += $" {vehicleModel}";
            }

            string vehicleRegistrationNumber = GetDataValue(log.Data, "vehicle_registration_number");

            if (!string.IsNullOrWhiteSpace(vehicleRegistrationNumber))
            {
                text += $" cu numărul de înmatriculare '{vehicleRegistrationNumber}'";
            }

            return $"{text} a fost măsurată la {GetDataValue(log.Data, "distance")} {GetDataValue(log.Data, "unit", "km")}";
        }

        public string BuildVideoUploadLogText(PersonalLog log)
        {
            string text = $"Am publicat un video";

            string videoId = GetDataValue(log.Data, "video_url");

            if (!string.IsNullOrWhiteSpace(videoId))
            {
                text += $" ({videoId})";
            }

            text += $" cu titlul '{log.Data["video_title"]}' pe {GetPlatform(log.Data)}";

            string uploadedFileName = GetDataValue(log.Data, "uploaded_file_name");

            if (!string.IsNullOrWhiteSpace(uploadedFileName))
            {
                text += $", din fișierul '{uploadedFileName}'";
            }

            return text;
        }

        public string BuildVideoWatchingLogText(PersonalLog log)
        {
            string text = $"Am vizionat video-ul '{log.Data["video_title"]}'";

            string videoId = GetDataValue(log.Data, "video_id");

            if (!string.IsNullOrWhiteSpace(videoId))
            {
                text += $" ({videoId})";
            }

            string channelName = GetDataValue(log.Data, "channel_name");

            if (!string.IsNullOrWhiteSpace(channelName))
            {
                text += $" de pe canalul '{channelName}'";
            }

            string platform = GetPlatform(log.Data);

            if (!string.IsNullOrWhiteSpace(platform))
            {
                text += $" de pe {platform}";
            }

            return text + GetLocation(log.Data);
        }

        public string BuildWakingUpLogText(PersonalLog log)
        {
            string text = "M-am trezit" + GetLocation(log.Data);

            string side = GetSide(log.Data);

            if (!string.IsNullOrWhiteSpace(side) &&
                !string.Equals(side, MissingValue))
            {
                text += $", pe partea {side}";
            }

            return text;
        }

        public string BuildWaterDrinkingLogText(PersonalLog log)
            => $"Am băut apă" + GetLocation(log.Data);

        public string BuildWeddingAttendanceLogText(PersonalLog log)
        {
            string text = $"Am participat la nunta lui";

            string brideName = GetDataValue(log.Data, "bride_name");
            string groomName = GetDataValue(log.Data, "groom_name");

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

            return text + GetLocation(log.Data);
        }

        public string BuildWindowClosingLogText(PersonalLog log)
            => $"Am închis fereastra" + GetLocation(log.Data);

        public string BuildWindowOpeningLogText(PersonalLog log)
            => $"Am deschis fereastra" + GetLocation(log.Data);

        public string BuildWorkFromTheOfficeLogText(PersonalLog log)
            => $"Am lucrat de la birou" + GetLocation(log.Data);

        public string BuildWorkMandatoryCourseBeginningLogText(PersonalLog log)
        {
            string text = $"Am început cursul obligatoriu '{log.Data["course_name"]}'";

            string employerName = GetDataValue(log.Data, "employer_name");

            if (!string.IsNullOrWhiteSpace(employerName))
            {
                text += $" de la {employerName}";
            }

            return text;
        }

        public string BuildWorkMandatoryCourseCompletionLogText(PersonalLog log)
        {
            string text = $"Am finalizat cursul obligatoriu '{log.Data["course_name"]}'";

            string employerName = GetDataValue(log.Data, "employer_name");

            if (!string.IsNullOrWhiteSpace(employerName))
            {
                text += $" de la {employerName}";
            }

            string scorePercentage = GetDataValue(log.Data, "score_obtained");

            if (!string.IsNullOrWhiteSpace(scorePercentage))
            {
                text += $", obținând un scor de {scorePercentage}%";
            }

            return text;
        }

        public string BuildWorkOnCallAlertReceivalLogText(PersonalLog log)
        {
            string text = $"A venit o alertă în timpul turei mele de gardă pentru {GetDataValue(log.Data, "employer_name")}";

            string platform = GetPlatform(log.Data);

            if (!string.IsNullOrWhiteSpace(platform))
            {
                text += $" pe {platform}";
            }

            string alertSubject = GetDataValue(log.Data, "alert_subject");

            if (!string.IsNullOrWhiteSpace(alertSubject))
            {
                text += $": {alertSubject}";
            }

            return text;
        }

        public string BuildWorkOnCallShiftBeginningLogText(PersonalLog log)
            => $"Tura mea de gardă pentru {GetDataValue(log.Data, "employer_name")} a început";

        public string BuildWorkOnCallShiftEndingLogText(PersonalLog log)
            => $"Tura mea de gardă pentru {GetDataValue(log.Data, "employer_name")} s-a terminat";

        public string BuildWorkTimesheetSubmissionLogText(PersonalLog log)
        {
            string text = $"Am trimis pontajul pentru {GetDataValue(log.Data, "employer_name")}";

            string weekNumber = GetDataValue(log.Data, "week_number");

            if (!string.IsNullOrWhiteSpace(weekNumber))
            {
                text += $", pentru săptămâna #{weekNumber} a anului ";

                string year = GetDataValue(log.Data, "year");

                if (!string.IsNullOrWhiteSpace(year))
                {
                    text += year;
                }
                else
                {
                    text += "curent";
                }
            }

            return text;
        }

        protected override string GetAccessoryType(
            Dictionary<string, string> data,
            bool useDefinitiveForm)
        {
            if (useDefinitiveForm)
            {
                return GetMappedDataValue(data, "accessory_type", new()
                {
                    { "Glasses", "ochelarii" }
                },
                "accesoriul");
            }

            return GetMappedDataValue(data, "accessory_type", new()
            {
                { "Glasses", "ochelari" }
            },
            "accesoriu");
        }

        protected override string GetByPerson(Dictionary<string, string> data)
        {
            if (data.ContainsKey("by"))
            {
                return GetLocalisedValue(data, "by");
            }
            else if (data.ContainsKey("from"))
            {
                return GetLocalisedValue(data, "from");
            }
            else if (data.ContainsKey("dentist_name"))
            {
                return GetLocalisedValue(data, "dentist_name");
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
                { "AirBlower", "utilizarea suflantei de aer" },
                { "Brushing", "periere" },
                { "CottonBuds", "utilizarea bețișoarelor de urechi" },
                { "Dusting", "ștergerea prafului" },
                { "LintRemover", "utilizarea aparatului de scos scame" },
                { "LintRoller", "utilizarea rolei de scame" },
                { "SpiralEarCleaner", "utilizarea dispozitivului de curățare a urechilor în formă de spirală" },
                { "Vacuuming", "aspirare" },
                { "Washing", "spălare" },
                { "Wiping", "ștergere" },
            });

        protected override string GetDevice(Dictionary<string, string> data)
        {
            string text = string.Empty;

            if (data.ContainsKey("device_type"))
            {
                text += GetDeviceType(data);
            }

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
                if (!string.IsNullOrWhiteSpace(text))
                {
                    text += " ";
                }

                text += deviceName;
            }

            return text;
        }

        protected override string GetDeviceType(Dictionary<string, string> data)
            => GetMappedDataValue(data, "device_type", new()
                {
                    { "AirBlower", "suflanta de aer" },
                    { "BloodGlucoseMeter", "glucometrul" },
                    { "BodyScale", "cântarul de corp" },
                    { "BodyTrimmer", "trimmer-ul corporal" },
                    { "Computer", "calculatorul" },
                    { "Console", "consola" },
                    { "Dehumidifier", "dezumidificatorul" },
                    { "DesktopComputer", "desktop computer" },
                    { "FitnessTracker", "brățara de fitness" },
                    { "HairTrimmer", "aparatul de tuns părul" },
                    { "Headphones", "căștile" },
                    { "HeadTorch", "lanterna frontală" },
                    { "HeartRateMonitor", "monitorul de ritm cardiac" },
                    { "Laptop", "laptop-ul" },
                    { "LaserPetToy", "laserul pentru animale de companie" },
                    { "LintRemover", "aparatul de scos scame" },
                    { "Phone", "telefonul" },
                    { "PrecisionTrimmer", "trimmer-ul mic" },
                    { "Scale", "cântarul" },
                    { "Scooter", "trotineta" },
                    { "Tablet", "tableta" },
                    { "Toothbrush", "periuța de dinți" },
                    { "VacuumCleaner", "aspiratorul" },
                    { "Watch", "ceasul" },
                    { "WaterFlosser", "irigatorul bucal" },
                    { "WirelessSpeaker", "boxa fără fir" },
                },
                data["device_type"].ToLower()
            );

        protected override string GetFluidType(Dictionary<string, string> data, bool useDefinitiveForm)
        {
            if (useDefinitiveForm)
            {
                return GetMappedDataValue(data, "fluid_type", new()
                {
                    { "Coolant", "lichidul de răcire" },
                    { "MotorOil", "uleiul de motor" },
                    { "WindscreenWasherFluid", "lichidul de parbriz" }
                },
                "lichidul");
            }

            return GetMappedDataValue(data, "fluid_type", new()
            {
                { "Coolant", "lichid de răcire" },
                { "MotorOil", "ulei de motor" },
                { "WindscreenWasherFluid", "lichid de parbriz" }
            },
            "lichid");
        }

        protected override string GetFurnitureType(Dictionary<string, string> data)
            => GetMappedDataValue(data, "furniture_type", new()
            {
                { "Armchair", "fotoliul" },
                { "BathroomSink", "lavoarul" },
                { "CatTree", "pomul pentru pisici" },
                { "Chair", "scaunul" },
                { "Chairs", "scaunele" },
                { "CoffeeTable", "masa de cafea" },
                { "Couch", "canapeaua" },
                { "Countertop", "blatul" },
                { "Desk", "biroul" },
                { "Doorstop", "opritorul de ușă" },
                { "KitchenSink", "chiuveta" },
                { "Radiator", "radiatorul" },
                { "Sink", "chiuveta" },
                { "Table", "masa" },
                { "Toilet", "toaleta" },
                { "TvStand", "comoda pentru televizor" },
                { "Washbasin", "lavoarul" },
                { "WindowSill", "pervazul" }
            },
            "mobila");

        protected override string GetHairType(Dictionary<string, string> data)
            => GetMappedDataValue(data, "hair_type", new()
            {
                { "Beard", "barba" },
                { "ChestHair", "părul de pe piept" },
                { "EyebrowHair", "părul de pe sprâncene" },
                { "FacialHair", "părul facial" },
                { "FootHair", "părul de pe labele picioarelor" },
                { "GenitalHair", "părul pubian" },
                { "HairTrimmer", "trimmer-ul de păr" },
                { "HeadHair", "părul de pe cap" },
                { "LegHair", "părul de pe picioare" },
                { "Mustache", "mustața" },
                { "NoseHair", "părul din nas" },
                { "Sideburns", "părul de pe tâmple" },
                { "UnderarmHair", "părul de la subraț" },
                { "Unibrow", "monosprânceana" }
            },
            "părul");

        protected override string GetLocation(Dictionary<string, string> data)
        {
            if (data is null)
            {
                return string.Empty;
            }

            string text = string.Empty;
            string room = string.Empty;

            if (data.ContainsKey("room"))
            {
                room = GetRoom(data);
            }

            if (!string.IsNullOrWhiteSpace(room))
            {
                text += $", în {room}";
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

                text += $" la {buildingName}";
            }

            string location = string.Empty;

            if (data.ContainsKey("location"))
            {
                location = GetDataValue(data, "location");
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
                    text += " la";
                }
                else
                {
                    text += " din";
                }

                text += $" {location}";
            }

            if (!string.IsNullOrWhiteSpace(text) &&
                data.ContainsKey("floor_index"))
            {
                text += $", de la etajul {GetDataValue(data, "floor_index")}";
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
                text += $", împreună cu {with}";
            }

            return text;
        }

        protected override string GetNailsType(Dictionary<string, string> data)
            => GetMappedDataValue(data, "nails_type", new()
            {
                { "FingerNails", "unghiile de la degete" },
                { "ToeNails", "unghiile de la picioare" }
            }, "unghiile");

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
                        { "Antiacid", "antiacide" },
                        { "Antibiotic", "antibiotice" },
                        { "Antifungal", "antifungice" },
                        { "Antiinflammatory", "antiinflamatoare" },
                        { "Antiparasitic", "antiparazitice" },
                        { "Antiseptic", "antiseptice" },
                        { "Anxiolytic", "anxiolitice" },
                        { "Corticosteroid", "corticosteroizi" },
                        { "Enzymatic", "enzimatice" },
                        { "Gastroprotective", "gastroprotective" },
                        { "Painkiller", "antinevralgice" },
                        { "Probiotic", "probiotice" },
                        { "Supplement", "suplimente" },
                        { "Vaccine", "vaccinuri" },
                    },
                    "medicamente");
            }

            return GetMappedDataValue(
                data,
                "medication_type",
                new()
                {
                    { "Antiacid", "antiacid" },
                    { "Antibiotic", "antibiotic" },
                    { "Antifungal", "antifungic" },
                    { "Antiinflammatory", "antiinflamator" },
                    { "Antiparasitic", "antiparazitic" },
                    { "Antiseptic", "antiseptic" },
                    { "Anxiolytic", "anxiolitic" },
                    { "Corticosteroid", "corticosteroid" },
                    { "Enzymatic", "enzimatic" },
                    { "Gastroprotective", "gastroprotectiv" },
                    { "Painkiller", "antinevralgic" },
                    { "Probiotic", "probiotic" },
                    { "Supplement", "supliment" },
                    { "Vaccine", "vaccin" },
                },
                "medicament");
        }

        protected override string GetPet(Dictionary<string, string> data)
            => GetLocalisedValue(data, "pet_name");

        protected override string GetPetType(
            Dictionary<string, string> data,
            bool useDefinitiveForm,
            bool usePluralForm)
        {
            string key = "pet_type";

            if (useDefinitiveForm)
            {
                if (usePluralForm)
                {
                    return GetMappedDataValue(data, key, new()
                    {
                        { "Cat", "pisicile" },
                        { "Dog", "câinii" },
                        { "Rabbit", "iepurii" },
                        { "Ferret", "dihorii" },
                        { "GuineaPig", "porcușorii de Guineea" }
                    }, "animalele de companie");
                }
                else
                {
                    return GetMappedDataValue(data, key, new()
                    {
                        { "Cat", "pisica" },
                        { "Dog", "câinele" },
                        { "Rabbit", "iepurele" },
                        { "Ferret", "dihorul" },
                        { "GuineaPig", "porcușorul de Guineea" }
                    }, "animalul de companie");
                }
            }
            else
            {
                if (usePluralForm)
                {
                    return GetMappedDataValue(data, key, new()
                    {
                        { "Cat", "pisici" },
                        { "Dog", "câini" },
                        { "Rabbit", "iepuri" },
                        { "Ferret", "dihori" },
                        { "GuineaPig", "porcușori de Guineea" }
                    }, "animale de companie");
                }
                else
                {
                    return GetMappedDataValue(data, key, new()
                    {
                        { "Cat", "pisică" },
                        { "Dog", "câine" },
                        { "Rabbit", "iepure" },
                        { "Ferret", "dihor" },
                        { "GuineaPig", "porcușor de Guineea" }
                    }, "animal de companie");
                }
            }
        }

        protected override string GetPlantType(
            Dictionary<string, string> data,
            bool useDefinitiveForm,
            bool usePluralForm)
        {
            if (usePluralForm)
            {
                string plantType = GetMappedDataValue(
                    data,
                    "plant_type",
                    new()
                    {
                        { "Flower", "flori" },
                        { "Succulent", "suculente" },
                    },
                    "plante");

                if (useDefinitiveForm)
                {
                    return $"{plantType}le";
                }

                return plantType;
            }

            if (useDefinitiveForm)
            {
                return GetMappedDataValue(
                    data,
                    "plant_type",
                    new()
                    {
                        { "Flower", "floarea" },
                        { "Succulent", "suculenta" },
                    },
                    "planta");
            }

            return GetMappedDataValue(
                data,
                "plant_type",
                new()
                {
                    { "Flower", "floare" },
                    { "Succulent", "suculentă" },
                },
                "plantă");
        }

        protected override string GetRoom(Dictionary<string, string> data)
            => GetMappedDataValue(data, "room", new()
                {
                    { "AccessibleBathroom", "baia pentru persoanele cu dezabilități" },
                    { "Attic", "pod" },
                    { "BackBalcony", "balconul din spate" },
                    { "BackPorch", "veranda din spate" },
                    { "Balcony", "balcon" },
                    { "Bathroom", "baie" },
                    { "Bedroom", "dormitor" },
                    { "DressingRoom", "dressing" },
                    { "FemaleBathroom", "baia pentru femei" },
                    { "FrontBalcony", "balconul din față" },
                    { "FrontPorch", "veranda din față" },
                    { "Hallway", "hol" },
                    { "Kitchen", "bucătărie" },
                    { "LargerBathroom", "baia mare" },
                    { "LargerBedroom", "dormitorul mare" },
                    { "LivingRoom", "sufragerie" },
                    { "LowerBathroom", "baia de jos" },
                    { "LowerBedroom", "dormitorul de jos" },
                    { "LowerHallway", "holul de jos" },
                    { "MaleBathroom", "baia pentru bărbați" },
                    { "Office", "birou" },
                    { "Pantry", "cămară" },
                    { "Porch", "verandă" },
                    { "SmallerBathroom", "baia mică" },
                    { "SmallerBedroom", "dormitorul mic" },
                    { "Stairway", "scară" },
                    { "UpperBathroom", "baia de sus" },
                    { "UpperBedroom", "dormitorul de sus" },
                    { "UpperHallway", "holul de sus" }
                },
                data["room"].ToLower()
            );

        protected override string GetSide(Dictionary<string, string> data)
            => GetMappedDataValue(data, "side", new()
                {
                    { "central", "centrală" },
                    { "right", "dreaptă" },
                    { "left", "stângă" }
                }, MissingValue
            );

        protected override string GetVehicleType(Dictionary<string, string> data, bool useDefinitiveForm)
        {
            if (useDefinitiveForm)
            {
                return GetMappedDataValue(data, "vehicle_type", new()
                {
                    { "Bicycle", "bicicleta" },
                    { "Car", "mașina" },
                    { "ElectricScooter", "trotineta eletrică" }
                },
                "vehiculul");
            }

            return GetMappedDataValue(data, "vehicle_type", new()
            {
                { "Bicycle", "bicicletă" },
                { "Car", "mașină" },
                { "ElectricScooter", "trotinetă electrică" }
            },
            "vehicul");
        }
    }
}