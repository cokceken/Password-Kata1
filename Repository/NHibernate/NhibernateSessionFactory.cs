using FluentNHibernate.Cfg;
using FluentNHibernate.Cfg.Db;
using NHibernate;
using NHibernate.Tool.hbm2ddl;
using Password.Repository.Map;

namespace Password.Repository.NHibernate
{
    public class NhibernateSessionFactory
    {
        private ISessionFactory _sessionFactory;

        private const string ConnectionString =
            "Data Source=(localdb)\\MSSQLLocalDB;Initial Catalog=password;Integrated Security=True;Connect Timeout=30;Encrypt=False;TrustServerCertificate=True;ApplicationIntent=ReadWrite;MultiSubnetFailover=False";

        public ISessionFactory SessionFactory => _sessionFactory ?? (_sessionFactory = CreateSessionFactory());

        public ISessionFactory CreateSessionFactory()
        {
            var sessionFactory = Fluently.Configure()
                .Database(MsSqlConfiguration.MsSql2012
                    .ConnectionString(ConnectionString)
                    .ShowSql())
                .Mappings(m => m.FluentMappings.AddFromAssemblyOf<UserCredentialMap>())
                .ExposeConfiguration(x => new SchemaUpdate(x).Execute(false, true))
                .BuildSessionFactory();

            return sessionFactory;
        }
    }
}