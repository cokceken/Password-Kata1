using System.ComponentModel;
using System.ComponentModel.DataAnnotations;

namespace Password.UI.Models
{
    public class ForgotPasswordModel
    {
        [DisplayName("Email")]
        [Required]
        public string Email { get; set; }

        public bool? IsSuccessfulMail { get; set; }
    }
}