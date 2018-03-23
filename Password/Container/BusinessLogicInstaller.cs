using Castle.MicroKernel.Registration;
using Castle.MicroKernel.SubSystems.Configuration;
using Castle.Windsor;
using Password.Domain.Contract;
using Password.Domain.Contract.AuthenticationContract;
using Password.Domain.Contract.RepositoryContract;
using Password.Domain.Contract.TokenContract;
using Password.Domain.Contract.UserContract;
using Password.Domain.Service;
using Password.Infrastructure.Services;
using Password.Infrastructure.Services.Data;
using Password.Infrastructure.Services.Dummy;
using Password.Repository.Repository;

namespace Password.UI.Container
{
    public class BusinessLogicInstaller : IWindsorInstaller
    {
        public void Install(IWindsorContainer container, IConfigurationStore store)
        {
            InstallDomain(container, store);
            InstallInfrastructure(container, store);
            InstallRepository(container, store);
        }

        private void InstallDomain(IWindsorContainer container, IConfigurationStore store)
        {
            container.Register(Component.For<IUserService>().ImplementedBy<UserService>());
            container.Register(Component.For<ITokenService>().ImplementedBy<TokenService>());
            container.Register(Component.For<IAuthenticationService>().ImplementedBy<AuthenticationService>());
        }

        private void InstallInfrastructure(IWindsorContainer container, IConfigurationStore store)
        {
            container.Register(Component.For<IEmailService>().ImplementedBy<DummyEmailService>().LifeStyle.Singleton);
            container.Register(Component.For<IHashService>().ImplementedBy<HashService>().LifeStyle.Singleton);
            container.Register(Component.For<ITokenGenerator>().ImplementedBy<TokenGenerator>().LifeStyle.Singleton);
            container.Register(Component.For<ITokenDataService>().ImplementedBy<TokenDataService>());
            container.Register(Component.For<IUserDataService>().ImplementedBy<UserDataService>());
            container.Register(Component.For<ILogger>().ImplementedBy<Log4Net>().LifeStyle.Singleton);
        }

        private void InstallRepository(IWindsorContainer container, IConfigurationStore store)
        {
            container.Register(Component.For<ITokenRepository>().ImplementedBy<TokenRepository>());
            container.Register(Component.For<IUserRepository>().ImplementedBy<UserRepository>());
        }
    }
}