using System.Web.Mvc;
using Password.Domain.Contract.AuthenticationContract;
using Password.UI.Models;

namespace Password.UI.Controllers
{
    public class ChangePasswordController : Controller
    {
        private readonly IAuthenticationService _authenticationService;

        public ChangePasswordController(IAuthenticationService authenticationService)
        {
            ViewBag.Message = "Change Password Page";

            _authenticationService = authenticationService;
        }

        public ActionResult Index()
        {
            return RedirectToAction("Index", "Home");
        }

        public ActionResult Load(string token, int userId)
        {
            var model = new ChangePasswordModel()
            {
                Token = token,
                UserId = userId,
                IsSuccessfulChangePassword = null
            };

            return View("Index", model);
        }

        [HttpPost]
        public ActionResult ChangePassword(ChangePasswordModel model)
        {
            _authenticationService.ChangePassword(model.UserId, model.Token, model.NewPassword);
            model.IsSuccessfulChangePassword = true;

            return View("Index", model);
        }
    }
}