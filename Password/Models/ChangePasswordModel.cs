using System.ComponentModel;
using System.ComponentModel.DataAnnotations;

namespace Password.UI.Models
{
    public class ChangePasswordModel
    {
        [DisplayName("NewPassword")]
        [Required]
        public string NewPassword { get; set; }
        public int UserId { get; set; }
        public string Token { get; set; }
        public bool? IsSuccessfulChangePassword { get; set; }
    }
}