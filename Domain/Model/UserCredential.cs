using System.Collections.Generic;

namespace Password.Domain.Model
{
    public class UserCredential : BaseModel<int>
    {
        public virtual string Username { get; set; }
        public virtual string Password { get; set; }
        public virtual string Email { get; set; }
        public virtual string PasswordSalt { get; set; }
    }
}