using FluentNHibernate.Mapping;
using Password.Domain.Model;

namespace Password.Repository.Map
{
    public class TokenMap : ClassMap<Token>
    {
        public TokenMap()
        {
            Id(x => x.Id);

            References(x => x.User).Column("User_id");
            Map(x => x.ExpireDateTime);
            Map(x => x.Value);

            Table("Token");
        }
    }
}
