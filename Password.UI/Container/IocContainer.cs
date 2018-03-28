using System;
using System.Web.Mvc;
using Castle.MicroKernel.Registration;
using Castle.Windsor;
using Castle.Windsor.Installer;

namespace Password.UI.Container
{
    public static class IocContainer
    {
        private static IWindsorContainer _container;

        public static void Setup()
        {
            _container =
                new WindsorContainer().Install(
                    FromAssembly.InDirectory(new AssemblyFilter(AppDomain.CurrentDomain.RelativeSearchPath)));

            var controllerFactory = new WindsorControllerFactory(_container.Kernel);
            ControllerBuilder.Current.SetControllerFactory(controllerFactory);
        }

        public static IWindsorContainer Get() => _container;
    }
}