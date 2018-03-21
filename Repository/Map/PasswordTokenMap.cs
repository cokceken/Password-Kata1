using FluentNHibernate.Mapping;
using Password.Domain.Model;

namespace Password.Repository.Map
{
    public class PasswordTokenMap : ClassMap<PasswordToken>
    {
        public PasswordTokenMap()
        {
            Id(x => x.Id);

            References(x => x.UserCredential).Column("UserCredential_id");
            Map(x => x.ExpireDateTime);
            Map(x => x.Token);

            Table("PasswordToken");
        }
    }
}
