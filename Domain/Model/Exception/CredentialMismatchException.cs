using System.Runtime.Serialization;

namespace Password.Domain.Model.Exception
{
    public class CredentialMismatchException : System.Exception , IUserFriendlyException
    {
        public CredentialMismatchException()
        {
        }

        public CredentialMismatchException(string message) : base(message)
        {
        }

        public CredentialMismatchException(string message, System.Exception innerException) : base(message, innerException)
        {
        }

        protected CredentialMismatchException(SerializationInfo info, StreamingContext context) : base(info, context)
        {
        }
    }
}