using FluentNHibernate.Mapping;
using Password.Domain.Model;

namespace Password.Repository.Map
{
    public class PasswordTokenMap : ClassMap<Token>
    {
        public PasswordTokenMap()
        {
            Id(x => x.Id);

            References(x => x.User).Column("UserCredential_id");
            Map(x => x.ExpireDateTime);
            Map(x => x.Value);

            Table("Token");
        }
    }
}
