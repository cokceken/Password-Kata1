using System.Web.Mvc;
using Password.Domain.Contract;
using Password.Domain.Contract.AuthenticationContract;
using Password.UI.Models;

namespace Password.UI.Controllers
{
    public class HomeController : Controller
    {
        private readonly IAuthenticationService _authenticationService;
        private readonly ILogger _logger;

        public HomeController(IAuthenticationService authenticationService, ILogger logger)
        {
            ViewBag.Message = "Home Page";

            _authenticationService = authenticationService;
            _logger = logger;
        }

        public ActionResult Index()
        {
            return View();
        }

        [HttpPost]
        public ActionResult Login(UserCredentialModel model)
        {
            model.IsSuccessfulLogin = _authenticationService.AreValidUserCredentials(model.Username, model.Password);

            return View("Index", model);
        }
    }
}