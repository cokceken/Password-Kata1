using FluentNHibernate.Cfg;
using FluentNHibernate.Cfg.Db;
using NHibernate;
using NHibernate.Tool.hbm2ddl;
using Password.Repository.Map;

namespace Password.Repository.NHibernate
{
    public class NHibernateHelper
    {
        public static ISession OpenSession()
        {
            var sessionFactory = Fluently.Configure()
                .Database(MsSqlConfiguration.MsSql2012
                    .ConnectionString(
                        "Data Source=(localdb)\\MSSQLLocalDB;Initial Catalog=password;Integrated Security=True")
                    .ShowSql())
                .Mappings(m => m.FluentMappings.AddFromAssemblyOf<UserCredentialMap>())
                .ExposeConfiguration(x => new SchemaUpdate(x).Execute(false, true))
                .BuildSessionFactory();

            return sessionFactory.OpenSession();
        }
    }
}