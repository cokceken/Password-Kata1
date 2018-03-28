using System.Web.Mvc;
using Password.Domain.Contract;
using Password.UI.Container;
using Password.UI.Handler;

namespace Password.UI
{
    public class FilterConfig
    {
        public static void RegisterGlobalFilters(GlobalFilterCollection filters)
        {
            filters.Add(new HandleErrorAttribute());
            filters.Add(new ExceptionHandler(IocContainer.Get().Resolve<ILogger>()));
        }
    }
}
