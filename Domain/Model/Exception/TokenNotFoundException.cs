using System.Runtime.Serialization;

namespace Password.Domain.Model.Exception
{
    public class TokenNotFoundException : System.Exception
    {
        public TokenNotFoundException()
        {
        }

        public TokenNotFoundException(string message, System.Exception innerException) : base(message, innerException)
        {
        }

        protected TokenNotFoundException(SerializationInfo info, StreamingContext context) : base(info, context)
        {
        }

        public TokenNotFoundException(string message) : base(message)
        {
        }
    }
}