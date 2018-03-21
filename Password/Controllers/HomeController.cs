using System.Web.Mvc;
using Password.Application;
using Password.Application.DTO.Request;
using Password.UI.Models;

namespace Password.UI.Controllers
{
    public class HomeController : Controller
    {
        private readonly IPasswordService _passwordService;

        public HomeController(IPasswordService passwordService)
        {
            _passwordService = passwordService;
        }

        public ActionResult Index()
        {
            return View();
        }

        public ActionResult ChangePassword(string token, int userId)
        {
            ViewBag.Message = "Change Password Page";

            return View();
        }

        [HttpPost]
        public ActionResult Login(UserCredentialModel model)
        {
            var areValidUserCredentialsRequest = new AreValidUserCredentialsRequest()
            {
                Password = model.Password,
                Username = model.Username
            };

            var result = _passwordService.AreValidUserCredentials(areValidUserCredentialsRequest);

            model.IsSuccessfulLogin = result.Result;

            return View("Index", model);
        }
    }
}