using Reviews.CommandApi.Shared.Extensions;

namespace Reviews.CommandApi.Shared.Exceptions
{
    public class NotOpenTransactionException : Exception
    {
        private const string ErrorMessage =
            "There is no transaction opended to {0}";

        public NotOpenTransactionException() { }

        public NotOpenTransactionException(string operation)
            : base(ErrorMessage.Format(operation)) { }
    }
}
