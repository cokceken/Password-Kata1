using System.Reflection;
using Castle.MicroKernel.Registration;
using Castle.MicroKernel.SubSystems.Configuration;
using Castle.Windsor;
using Password.Domain.Contract;
using Password.Domain.Contract.TokenContract;
using Password.Domain.Contract.UserContract;
using Password.Domain.Service;
using Password.Infrastructure.Services;
using Password.Infrastructure.Services.Data;
using Password.Infrastructure.Services.Dummy;

namespace Password.UI.Container
{
    public class BusinessLogicInstaller : IWindsorInstaller
    {
        public void Install(IWindsorContainer container, IConfigurationStore store)
        {
            InstallDomain(container, store);
            InstallInfrastructure(container, store);
        }

        public void InstallDomain(IWindsorContainer container, IConfigurationStore store)
        {
            container.Register(Classes.FromAssembly(Assembly.GetAssembly(typeof(UserService)))
                .InSameNamespaceAs<UserService>()
                .WithService.DefaultInterfaces()
                .LifestylePerWebRequest());
        }

        public void InstallInfrastructure(IWindsorContainer container, IConfigurationStore store)
        {
            container.Register(Component.For<IEmailService>().ImplementedBy<DummyEmailService>().LifeStyle.Singleton);
            container.Register(Component.For<IHashService>().ImplementedBy<HashService>().LifeStyle.Singleton);
            container.Register(Component.For<ITokenDataService>().ImplementedBy<TokenDataService>());
            container.Register(Component.For<IUserDataService>().ImplementedBy<UserDataService>());
            container.Register(Component.For<ILogger>().ImplementedBy<Log4Net>().LifeStyle.Singleton);
        }
    }
}