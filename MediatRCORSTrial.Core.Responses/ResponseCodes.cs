using System;
using System.Collections.Generic;
using System.Text;

namespace MediatRCORSTrial.Core.Responses
{
    public static class ResponseCodes
    {
        public const string Success = "RC0000";
        public const string NotNullable = "RC0001";
        public const string Failed = "RC0002";
        public const string NotFound = "RC0003";
        public const string Unauthorized = "RC0004";
        public const string BadRequest = "RC0005";
        public const string UserAlreadyExistButNotConfirmed = "RC10001";
        public const string ConfirmedButDetailsWereNotFilled = "RC10002";
        public const string UserAlreadyExist = "RC10003";
        public const string NeedToConfirmAccount = "RC10004";
        public const string LockedAccount = "RC10005";
        public const string UseRemoved = "RC10006";
        public const string AccountLockedFor30Min = "RC10007";
        public const string SignInFailed = "RC10008";
        public const string SMSCodeNotMatched = "RC10009";
        public const string UserNotFound = "RC10010";
        public const string PasswordNotMatched = "RC10011";
        public const string InvalidToken = "RC10012";
        public const string ExpireToken = "RC10013";
        public const string InvalidRowGUID = "RC10014";
        public const string InvalidPasswordRequirements = "RC10015";
        public const string MernisGetTCKNInfoError = "RC10016";
        public const string MernisGetYKNInfoError = "RC10017";
        public const string HasAlreadyToken = "RC10018";
        public const string MernisPersonNotFound = "RC10019";
        public const string MernisVknNotFound = "RC10020";
        public static string MernisNameOrLastNameInError = "RC10021";
        public const string WalletRegisterFail = "RC20011";
        public const string WalletAccountExtractFail = "RC20012";
        public const string WalletCardListFail = "RC20013";
        public const string WalletDeleteCardFail = "RC20014";
        public const string WalletCheckBalanceFail = "RC20015";
        public const string WalletExtractFail = "RC20016";
        public const string WalletCardAddFail = "RC20017";
        public const string WalletTransactionPaymentFail = "RC20018";
        public const string AccountNotFound = "RC20019";
        public const string WalletCardPersonalizationFail = "RC20020";
        public const string WalletCardTopUpFail = "RC20021";
        public const string ParentNetworkDetailIsNotExist = "RC20022";
        public const string WalletQrGenerateError = "RC20023";
        public const string WalletQrTransferError = "RC20024";
        public const string WalletUploadDocument = "RC20025";
        public const string WalletTopUpError = "RC20026";
        public const string TopUpErrorInsertRecord = "RC20027";
        public const string TopUpErrorUpdateRecord = "RC20028";
        public const string MerchantDetailNotFound = "RC20029";
        public const string MerchantFilesNotFound = "RC20030";
        public const string UnauthorizedUser = "RC20031";
        public const string UserRegistrationIsWaitingOnEWallet = "RC20032";
        public const string UserRegistrationDidntExistOnEWallet = "RC20033";
        public const string UserRegistrationDeniedByEWallet = "RC20034";
        public const string WalletRefundError = "RC20035";
        public const string KYCServiceErrorByWallet = "RC20036";
        public const string KYCAnonymousByWallet = "RC20037";
        public const string KYCWaitingByWallet = "RC20038";
        public const string KYCSuccessByWallet = "RC20039";
        public const string KYCDeniedByWallet = "RC20040";

        public const string QrGenerateError = "RC30001";
        public const string QrNotFound = "RC30002";
        public const string QrValidateError = "RC30003";
        public const string QrMoneyTransferNotConfirmed = "RC30004";
        public const string QrAlreadyUsed = "RC30005";
        public const string WrongUserForQrMoneyTransfer = "RC30006";
        public const string PayerAndReceiverCannotBeSame = "RC30007";
    }
}
