using System.Web.Mvc;
using Password.Domain.Contract.AuthenticationContract;
using Password.UI.Models;

namespace Password.UI.Controllers
{
    public class ForgotPasswordController : Controller
    {
        private readonly IAuthenticationService _authenticationService;

        public ForgotPasswordController(IAuthenticationService authenticationService)
        {
            ViewBag.Message = "Forgot Password Page";

            _authenticationService = authenticationService;
        }

        public ActionResult Index()
        {
            return View();
        }

        [HttpPost]
        public ActionResult SendResetEmail(ForgotPasswordModel model)
        {
            _authenticationService.SendResetEmail(model.Email);
            model.IsSuccessfulMail = true;

            return View("Index", model);
        }
    }
}