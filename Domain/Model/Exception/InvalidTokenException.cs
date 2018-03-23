using System.Runtime.Serialization;

namespace Password.Domain.Model.Exception
{
    public class InvalidTokenException : System.Exception
    {
        public InvalidTokenException()
        {
        }

        public InvalidTokenException(string message) : base(message)
        {
        }

        public InvalidTokenException(string message, System.Exception innerException) : base(message, innerException)
        {
        }

        protected InvalidTokenException(SerializationInfo info, StreamingContext context) : base(info, context)
        {
        }
    }
}