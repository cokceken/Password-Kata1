using FluentNHibernate.Mapping;
using Password.Domain.Model;

namespace Password.Repository.Map
{
    public class UserMap : ClassMap<User>
    {
        public UserMap()
        {
            Id(x => x.Id);

            Map(x => x.Email);
            Map(x => x.Password);
            Map(x => x.Username);
            Map(x => x.PasswordSalt);

            Table("[User]");
        }
    }
}
