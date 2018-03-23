using FluentNHibernate.Mapping;
using Password.Domain.Model;

namespace Password.Repository.Map
{
    public class UserCredentialMap : ClassMap<User>
    {
        public UserCredentialMap()
        {
            Id(x => x.Id);

            Map(x => x.Email);
            Map(x => x.Password);
            Map(x => x.Username);
            Map(x => x.PasswordSalt);

            Table("User");
        }
    }
}
