using System.Web.Mvc;
using Password.Application;
using Password.Application.DTO.Request;
using Password.UI.Models;

namespace Password.UI.Controllers
{
    public class ForgotPasswordController : Controller
    {
        private readonly IPasswordService _passwordService;

        public ForgotPasswordController(IPasswordService passwordService)
        {
            _passwordService = passwordService;
        }

        public ActionResult Index()
        {
            return View();
        }

        [HttpPost]
        public ActionResult SendResetEmail(ForgotPasswordModel model)
        {
            var result = _passwordService.SendResetEmail(new SendResetEmailRequest()
            {
                EmailAddress = model.Email
            });

            model.IsSuccessfulMail = result.Result;

            return View("Index", model);
        }
    }
}