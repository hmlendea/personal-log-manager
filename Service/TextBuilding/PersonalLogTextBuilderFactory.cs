using PersonalLogManager.Service.Models;

namespace PersonalLogManager.Service.TextBuilding
{
    public class PersonalLogTextBuilderFactory(IPersonalLogTextBuilder personalLogTextBuilder) : IPersonalLogTextBuilderFactory
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

        string BuildLogTextByTemplate(PersonalLog log)
        {
            if (log.Template.Equals(PersonalLogTemplate.AccountActivation))
            {
                return personalLogTextBuilder.BuildAccountActivationLogText(log);
            }
            else if (log.Template.Equals(PersonalLogTemplate.AccountBanning))
            {
                return personalLogTextBuilder.BuildAccountBanningLogText(log);
            }
            else if (log.Template.Equals(PersonalLogTemplate.AccountContactEmailAddressChange))
            {
                return personalLogTextBuilder.BuildAccountContactEmailAddressChangeLogText(log);
            }
            else if (log.Template.Equals(PersonalLogTemplate.AccountDataExport))
            {
                return personalLogTextBuilder.BuildAccountDataExportLogText(log);
            }
            else if (log.Template.Equals(PersonalLogTemplate.AccountDataExportRequest))
            {
                return personalLogTextBuilder.BuildAccountDataExportRequestLogText(log);
            }
            else if (log.Template.Equals(PersonalLogTemplate.AccountDataExportRequestFulfillment))
            {
                return personalLogTextBuilder.BuildAccountDataExportRequestFulfillmentLogText(log);
            }
            else if (log.Template.Equals(PersonalLogTemplate.AccountDataExportSave))
            {
                return personalLogTextBuilder.BuildAccountDataExportSaveLogText(log);
            }
            else if (log.Template.Equals(PersonalLogTemplate.AccountDataObfuscation))
            {
                return personalLogTextBuilder.BuildAccountDataObfuscationLogText(log);
            }
            else if (log.Template.Equals(PersonalLogTemplate.AccountDeactivation))
            {
                return personalLogTextBuilder.BuildAccountDeactivationLogText(log);
            }
            else if (log.Template.Equals(PersonalLogTemplate.AccountDeletion))
            {
                return personalLogTextBuilder.BuildAccountDeletionLogText(log);
            }
            else if (log.Template.Equals(PersonalLogTemplate.AccountDeletionRequest))
            {
                return personalLogTextBuilder.BuildAccountDeletionRequestLogText(log);
            }
            else if (log.Template.Equals(PersonalLogTemplate.AccountDeletionRequestCancellation))
            {
                return personalLogTextBuilder.BuildAccountDeletionRequestCancellationLogText(log);
            }
            else if (log.Template.Equals(PersonalLogTemplate.AccountDeletionRequestFulfillment))
            {
                return personalLogTextBuilder.BuildAccountDeletionRequestFulfillmentLogText(log);
            }
            else if (log.Template.Equals(PersonalLogTemplate.AccountDeletionValidation))
            {
                return personalLogTextBuilder.BuildAccountDeletionValidationLogText(log);
            }
            else if (log.Template.Equals(PersonalLogTemplate.AccountEmailAddressChange))
            {
                return personalLogTextBuilder.BuildAccountEmailAddressChangeLogText(log);
            }
            else if (log.Template.Equals(PersonalLogTemplate.AccountEmailAddressChangeRequest))
            {
                return personalLogTextBuilder.BuildAccountEmailAddressChangeRequestLogText(log);
            }
            else if (log.Template.Equals(PersonalLogTemplate.AccountEmailAddressChangeRequestFulfillment))
            {
                return personalLogTextBuilder.BuildAccountEmailAddressChangeRequestFulfillmentLogText(log);
            }
            else if (log.Template.Equals(PersonalLogTemplate.AccountEmailAddressConfirmation))
            {
                return personalLogTextBuilder.BuildAccountEmailAddressConfirmationLogText(log);
            }
            else if (log.Template.Equals(PersonalLogTemplate.AccountFeatureEnablement))
            {
                return personalLogTextBuilder.BuildAccountFeatureEnablementLogText(log);
            }
            else if (log.Template.Equals(PersonalLogTemplate.AccountFeatureDisablement))
            {
                return personalLogTextBuilder.BuildAccountFeatureDisablementLogText(log);
            }
            else if (log.Template.Equals(PersonalLogTemplate.AccountFriendshipRequestReceival))
            {
                return personalLogTextBuilder.BuildAccountFriendshipRequestReceivalLogText(log);
            }
            else if (log.Template.Equals(PersonalLogTemplate.AccountIdentityVerification))
            {
                return personalLogTextBuilder.BuildAccountIdentityVerificationLogText(log);
            }
            else if (log.Template.Equals(PersonalLogTemplate.AccountLinking))
            {
                return personalLogTextBuilder.BuildAccountLinkingLogText(log);
            }
            else if (log.Template.Equals(PersonalLogTemplate.AccountMessagesErasure))
            {
                return personalLogTextBuilder.BuildAccountMessagesErasureLogText(log);
            }
            else if (log.Template.Equals(PersonalLogTemplate.AccountPasswordChange))
            {
                return personalLogTextBuilder.BuildAccountPasswordChangeLogText(log);
            }
            else if (log.Template.Equals(PersonalLogTemplate.AccountPersonalNameChange))
            {
                return personalLogTextBuilder.BuildAccountPersonalNameChangeLogText(log);
            }
            else if (log.Template.Equals(PersonalLogTemplate.AccountPhoneNumberAddition))
            {
                return personalLogTextBuilder.BuildAccountPhoneNumberAdditionLogText(log);
            }
            else if (log.Template.Equals(PersonalLogTemplate.AccountPhoneNumberChange))
            {
                return personalLogTextBuilder.BuildAccountPhoneNumberChangeLogText(log);
            }
            else if (log.Template.Equals(PersonalLogTemplate.AccountPhoneNumberRemoval))
            {
                return personalLogTextBuilder.BuildAccountPhoneNumberRemovalLogText(log);
            }
            else if (log.Template.Equals(PersonalLogTemplate.AccountProfilePictureChange))
            {
                return personalLogTextBuilder.BuildAccountProfilePictureChangeLogText(log);
            }
            else if (log.Template.Equals(PersonalLogTemplate.AccountRecovery))
            {
                return personalLogTextBuilder.BuildAccountRecoveryLogText(log);
            }
            else if (log.Template.Equals(PersonalLogTemplate.AccountRecoveryEmailAddressChange))
            {
                return personalLogTextBuilder.BuildAccountRecoveryEmailAddressChangeLogText(log);
            }
            else if (log.Template.Equals(PersonalLogTemplate.AccountRegistration))
            {
                return personalLogTextBuilder.BuildAccountRegistrationLogText(log);
            }
            else if (log.Template.Equals(PersonalLogTemplate.AccountRegistrationRequest))
            {
                return personalLogTextBuilder.BuildAccountRegistrationRequestLogText(log);
            }
            else if (log.Template.Equals(PersonalLogTemplate.AccountRegistrationRequestFulfillment))
            {
                return personalLogTextBuilder.BuildAccountRegistrationRequestFulfillmentLogText(log);
            }
            else if (log.Template.Equals(PersonalLogTemplate.AccountSubscriptionPurchase))
            {
                return personalLogTextBuilder.BuildAccountSubscriptionPurchaseLogText(log);
            }
            else if (log.Template.Equals(PersonalLogTemplate.AccountUnlinking))
            {
                return personalLogTextBuilder.BuildAccountUnlinkingLogText(log);
            }
            else if (log.Template.Equals(PersonalLogTemplate.AccountUsernameChange))
            {
                return personalLogTextBuilder.BuildAccountUsernameChangeLogText(log);
            }
            else if (log.Template.Equals(PersonalLogTemplate.AccountVisibilityMadePrivate))
            {
                return personalLogTextBuilder.BuildAccountVisibilityMadePrivateLogText(log);
            }
            else if (log.Template.Equals(PersonalLogTemplate.AccountVisibilityMadePublic))
            {
                return personalLogTextBuilder.BuildAccountVisibilityMadePublicLogText(log);
            }
            else if (log.Template.Equals(PersonalLogTemplate.BloodDonation))
            {
                return personalLogTextBuilder.BuildBloodDonationLogText(log);
            }
            else if (log.Template.Equals(PersonalLogTemplate.BloodGlucoseMeasurement))
            {
                return personalLogTextBuilder.BuildBloodGlucoseMeasurementLogText(log);
            }
            else if (log.Template.Equals(PersonalLogTemplate.BodyWaterRateMeasurement))
            {
                return personalLogTextBuilder.BuildBodyWaterRateMeasurementLogText(log);
            }
            else if (log.Template.Equals(PersonalLogTemplate.BodyWeightMeasurement))
            {
                return personalLogTextBuilder.BuildBodyWeightMeasurementLogText(log);
            }
            else if (log.Template.Equals(PersonalLogTemplate.ChatGroupCreation))
            {
                return personalLogTextBuilder.BuildChatGroupCreationLogText(log);
            }
            else if (log.Template.Equals(PersonalLogTemplate.ChatGroupDeletion))
            {
                return personalLogTextBuilder.BuildChatGroupDeletionLogText(log);
            }
            else if (log.Template.Equals(PersonalLogTemplate.ChatGroupJoining))
            {
                return personalLogTextBuilder.BuildChatGroupJoiningLogText(log);
            }
            else if (log.Template.Equals(PersonalLogTemplate.ChatGroupLeaving))
            {
                return personalLogTextBuilder.BuildChatGroupLeavingLogText(log);
            }
            else if (log.Template.Equals(PersonalLogTemplate.DatingAppMatch))
            {
                return personalLogTextBuilder.BuildDatingAppMatchLogText(log);
            }
            else if (log.Template.Equals(PersonalLogTemplate.DeliveryReceival))
            {
                return personalLogTextBuilder.BuildDeliveryReceivalLogText(log);
            }
            else if (log.Template.Equals(PersonalLogTemplate.DentalScaling))
            {
                return personalLogTextBuilder.BuildDentalScalingLogText(log);
            }
            else if (log.Template.Equals(PersonalLogTemplate.EmailExport))
            {
                return personalLogTextBuilder.BuildEmailExportLogText(log);
            }
            else if (log.Template.Equals(PersonalLogTemplate.EyeCheckup))
            {
                return personalLogTextBuilder.BuildEyeCheckupLogText(log);
            }
            else if (log.Template.Equals(PersonalLogTemplate.GettingOutOfBed))
            {
                return personalLogTextBuilder.BuildGettingOutOfBedLogText(log);
            }
            else if (log.Template.Equals(PersonalLogTemplate.HairCutting))
            {
                return personalLogTextBuilder.BuildHairCuttingLogText(log);
            }
            else if (log.Template.Equals(PersonalLogTemplate.InternshipApplicationSubmission))
            {
                return personalLogTextBuilder.BuildInternshipApplicationSubmissionLogText(log);
            }
            else if (log.Template.Equals(PersonalLogTemplate.MealVoucherCardCreditation))
            {
                return personalLogTextBuilder.BuildMealVoucherCardCreditationLogText(log);
            }
            else if (log.Template.Equals(PersonalLogTemplate.ObjectSale))
            {
                return personalLogTextBuilder.BuildObjectSaleLogText(log);
            }
            else if (log.Template.Equals(PersonalLogTemplate.OnlineReviewSubmission))
            {
                return personalLogTextBuilder.BuildOnlineReviewSubmissionLogText(log);
            }
            else if (log.Template.Equals(PersonalLogTemplate.OnlineStorePurchase))
            {
                return personalLogTextBuilder.BuildOnlineStorePurchaseLogText(log);
            }
            else if (log.Template.Equals(PersonalLogTemplate.SwimmingActivity))
            {
                return personalLogTextBuilder.BuildSwimmingActivityLogText(log);
            }
            else if (log.Template.Equals(PersonalLogTemplate.TeethBrushing))
            {
                return personalLogTextBuilder.BuildTeethBrushingLogText(log);
            }
            else if (log.Template.Equals(PersonalLogTemplate.TollPayment))
            {
                return personalLogTextBuilder.BuildTollPaymentLogText(log);
            }
            else if (log.Template.Equals(PersonalLogTemplate.UtilityBillPayment))
            {
                return personalLogTextBuilder.BuildUtilityBillPaymentLogText(log);
            }
            else if (log.Template.Equals(PersonalLogTemplate.UtilityIndexMeasurement))
            {
                return personalLogTextBuilder.BuildUtilityIndexMeasurementLogText(log);
            }
            else if (log.Template.Equals(PersonalLogTemplate.VideoUpload))
            {
                return personalLogTextBuilder.BuildVideoUploadLogText(log);
            }
            else if (log.Template.Equals(PersonalLogTemplate.WakingUp))
            {
                return personalLogTextBuilder.BuildWakingUpLogText(log);
            }
            else if (log.Template.Equals(PersonalLogTemplate.WorkFromTheOffice))
            {
                return personalLogTextBuilder.BuildWorkFromTheOfficeLogText(log);
            }

            return log.Data["text"];
        }
    }
}
