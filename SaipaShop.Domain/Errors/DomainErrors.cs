using SaipaShop.Domain.ResultPattern;

namespace SaipaShop.Domain.Errors;

public static class DomainErrors
{
    public static class Errors
    {
        public static readonly Error UserNotFoundError = new Error(ErrorType.Failure, "Check Username and Password");
        public static readonly Error MailEmptyError = new Error(ErrorType.Validation, "Mail Is Empty");
        public static readonly Error MailValidationError = new Error(ErrorType.Validation, "Write Valid Email");
        public static readonly Error UserNotAccepted = new Error(ErrorType.Validation, "UserNotAccepted");
        public static readonly Error NoResultFound = new Error(ErrorType.Failure, "NoResultFound");
        public static readonly Error AddedFailed = new Error(ErrorType.Failure, "AddedFailed");

    }
}