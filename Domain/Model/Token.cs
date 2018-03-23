using System;

namespace Password.Domain.Model
{
    public class Token : BaseModel<int>
    {
        public virtual string Value { get; set; }
        public virtual DateTime ExpireDateTime { get; set; }
        public virtual User User { get; set; }
        public override string ToString()
        {
            return $"token={Value}&userId={User.Id}";
        }
    }
}
