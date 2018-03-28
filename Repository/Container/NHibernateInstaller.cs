using System.Linq;
using System.Reflection;
using Castle.Core;
using Castle.MicroKernel;
using Castle.MicroKernel.Registration;
using Castle.MicroKernel.SubSystems.Configuration;
using Castle.Windsor;
using NHibernate;
using Password.Repository.Repository;
using Password.Repository.UnitOfWork;

namespace Password.Repository.Container
{
    public class NHibernateInstaller : IWindsorInstaller
    {
        public void Install(IWindsorContainer container, IConfigurationStore store)
        {
            container.Kernel.ComponentRegistered += KernelComponentRegistered;

            container.Register(Component.For<ISessionFactory>()
                .UsingFactoryMethod(NHibernateSessionFactory.GetSessionFactory)
                .LifeStyle.Singleton);

            container.Register(Component.For<NHibernateUnitOfWorkInterceptor>().LifeStyle.Transient);

            container.Register(Classes.FromAssembly(Assembly.GetAssembly(typeof(UserRepository)))
                .InSameNamespaceAs<UserRepository>()
                .WithService.DefaultInterfaces()
                .LifestylePerWebRequest());
        }

        private void KernelComponentRegistered(string key, IHandler handler)
        {
            if (UnitOfWorkHelper.IsRepositoryClass(handler.ComponentModel.Implementation))
                handler.ComponentModel.Interceptors.Add(
                    new InterceptorReference(typeof(NHibernateUnitOfWorkInterceptor)));

            if (handler.ComponentModel.Implementation.GetMethods().Any(UnitOfWorkHelper.HasUnitOfWorkAttribute))
                handler.ComponentModel.Interceptors.Add(
                    new InterceptorReference(typeof(NHibernateUnitOfWorkInterceptor)));
        }
    }
}