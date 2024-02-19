using System.ComponentModel.DataAnnotations;

namespace Jevellery.ViewModels.Account
{
    public class LoginViewModel
    {
        [Required(ErrorMessage = "Username or Email cannot be empty.")]
        public string? UserNameOrEmail { get; set; }
        [Required]
        [DataType(DataType.Password)]
        public string? Password { get; set; }
        public bool RememberMe { get; set; }
    }
}
