using System.Web.Mvc;
using System.Web.Optimization;
using System.Web.Routing;
using Password.Domain.Contract;
using Password.UI.Container;
using Password.UI.Exception;

namespace Password.UI
{
    public class MvcApplication : System.Web.HttpApplication
    {
        protected void Application_Start()
        {
            AreaRegistration.RegisterAllAreas();
            FilterConfig.RegisterGlobalFilters(GlobalFilters.Filters);
            RouteConfig.RegisterRoutes(RouteTable.Routes);
            BundleConfig.RegisterBundles(BundleTable.Bundles);

            //HibernatingRhinos.Profiler.Appender.NHibernate.NHibernateProfiler.Initialize();

            IocContainer.Setup();
            GlobalFilters.Filters.Add(new ExceptionHandler(IocContainer.Get().Resolve<ILogger>()));
        }
    }
}