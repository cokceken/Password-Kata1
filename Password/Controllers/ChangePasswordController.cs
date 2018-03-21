using System.Web.Mvc;
using Password.Application;
using Password.Application.DTO.Request;
using Password.UI.Models;

namespace Password.UI.Controllers
{
    public class ChangePasswordController : Controller
    {
        private readonly IPasswordService _passwordService;

        public ChangePasswordController(IPasswordService passwordService)
        {
            _passwordService = passwordService;
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
            };

            return View("Index", model);
        }

        [HttpPost]
        public ActionResult ChangePassword(ChangePasswordModel model)
        {
            var result = _passwordService.ChangePassword(new ChangePasswordRequest()
            {
                Token = model.Token,
                UserId = model.UserId,
                NewPassword = model.NewPassword
            });

            model.IsSuccessfulChangePassword = result.Result;

            return View("Index", model);
        }
    }
}