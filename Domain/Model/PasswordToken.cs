using System;

namespace Password.Domain.Model
{
    public class PasswordToken : BaseModel<int>
    {
        public virtual string Token { get; set; }
        public virtual DateTime ExpireDateTime { get; set; }
        public virtual UserCredential UserCredential { get; set; }
        public override string ToString()
        {
            return $"token={Token}&userId={UserCredential.Id}";
        }
    }
}
