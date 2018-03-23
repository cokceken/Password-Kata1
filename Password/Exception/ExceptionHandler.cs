using System.Web.Mvc;
using Password.Domain.Contract;

namespace Password.UI.Exception
{
    public class ExceptionHandler : HandleErrorAttribute
    {
        private readonly ILogger _logger;

        public ExceptionHandler(ILogger logger)
        {
            _logger = logger;
        }

        public override void OnException(ExceptionContext filterContext)
        {
            if (filterContext.ExceptionHandled)
            {
                return;
            }

            var e = filterContext.Exception;

            _logger.Error(e.Message, e);

            filterContext.ExceptionHandled = true;

            filterContext.HttpContext.Response.Clear();
            filterContext.HttpContext.Response.TrySkipIisCustomErrors = true;

            if (filterContext.Controller.ViewData["ErrorMessage"] != null)
            {
                filterContext.Controller.ViewData["ErrorMessage"] = e.Message;
                filterContext.HttpContext.Response.Redirect("/");
            }
            else
            {
                filterContext.Controller.ViewData["ErrorMessage"] = e.Message;
                filterContext.HttpContext.Response.Redirect(filterContext.RequestContext.HttpContext.Request
                    .RawUrl);
            }
        }
    }
}