using FluentNHibernate.Cfg;
using FluentNHibernate.Cfg.Db;
using NHibernate;
using NHibernate.Tool.hbm2ddl;
using Password.Repository.Map;

namespace Password.Repository.UnitOfWork
{
    public class NHibernateSessionFactory
    {
        private const string ConnectionString =
            "Data Source=(localdb)\\MSSQLLocalDB;Initial Catalog=password;Integrated Security=True";

        public static ISession OpenSession()
        {
            var sessionFactory = Fluently.Configure()
                .Database(MsSqlConfiguration.MsSql2012
                    .ConnectionString(ConnectionString)
                    .ShowSql())
                .Mappings(m => m.FluentMappings.AddFromAssemblyOf<UserMap>())
                .ExposeConfiguration(x => new SchemaUpdate(x).Execute(false, true))
                .BuildSessionFactory();

            return sessionFactory.OpenSession();
        }

        public static ISessionFactory GetSessionFactory()
        {
            return Fluently.Configure()
                .Database(MsSqlConfiguration.MsSql2012
                    .ConnectionString(ConnectionString)
                    .ShowSql())
                .Mappings(m => m.FluentMappings.AddFromAssemblyOf<UserMap>())
                .ExposeConfiguration(x => new SchemaUpdate(x).Execute(false, true))
                .BuildSessionFactory();
        }
    }
}