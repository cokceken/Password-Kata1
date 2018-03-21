using System.ComponentModel;
using System.ComponentModel.DataAnnotations;

namespace Password.UI.Models
{
    public class UserCredentialModel
    {
        [DisplayName("Username")]
        [Required]
        public string Username { get; set; }
        [DisplayName("Password")]
        [Required]
        public string Password { get; set; }
        public bool? IsSuccessfulLogin { get; set; }
    }
}