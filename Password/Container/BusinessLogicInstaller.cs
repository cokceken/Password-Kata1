using Castle.MicroKernel.Registration;
using Castle.MicroKernel.SubSystems.Configuration;
using Castle.Windsor;
using Password.Application;
using Password.Infrastructure.Contract;
using Password.Infrastructure.Services;
using Password.Infrastructure.Services.Data;
using Password.Infrastructure.Services.Dummy;
using Password.Repository.Contract;
using Password.Repository.Repository;

namespace Password.UI.Container
{
    public class BusinessLogicInstaller : IWindsorInstaller
    {
        public void Install(IWindsorContainer container, IConfigurationStore store)
        {
            container.Register(Component.For<IPasswordTokenRepository>().ImplementedBy<PasswordTokenRepository>());
            container.Register(Component.For<IUserCredentialRepository>().ImplementedBy<UserCredentialRepository>());
            container.Register(Component.For<IEmailService>().ImplementedBy<DummyEmailService>());
            container.Register(Component.For<IHashService>().ImplementedBy<DummyHashService>());
            container.Register(Component.For<IPasswordHashService>().ImplementedBy<PasswordHashService>());
            container.Register(Component.For<ITokenCreator>().ImplementedBy<TokenCreator>());
            container.Register(Component.For<ITokenDataService>().ImplementedBy<TokenDataService>());
            container.Register(Component.For<IUserCredentialDataService>().ImplementedBy<UserCredentialDataService>());
            container.Register(Component.For<IPasswordService>().ImplementedBy<PasswordService>());
        }
    }
}