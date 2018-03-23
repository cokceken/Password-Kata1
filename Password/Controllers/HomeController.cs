using System.Web.Mvc;
using Password.Domain.Contract.AuthenticationContract;
using Password.UI.Models;

namespace Password.UI.Controllers
{
    public class HomeController : Controller
    {
        private readonly IAuthenticationService _authenticationService;

        public HomeController(IAuthenticationService authenticationService)
        {
            ViewBag.Message = "Home Page";

            _authenticationService = authenticationService;
        }

        public ActionResult Index()
        {
            return View();
        }

        public ActionResult ChangePassword(string token, int userId)
        {
            return View();
        }

        [HttpPost]
        public ActionResult Login(UserCredentialModel model)
        {
            model.IsSuccessfulLogin = _authenticationService.AreValidUserCredentials(model.Username, model.Password);
            model.IsSuccessfulLogin = true;

            return View("Index", model);
        }
    }
}