using System.ComponentModel.DataAnnotations;

namespace Jevellery.ViewModels.Account
{
    public class RegisterViewModel
    {
        [Required]
        public string? Username { get; set; }
        [Required]
        [DataType(DataType.EmailAddress)]
        public string? Email { get; set; }
        [Required]
        [DataType(DataType.Password)]
        public string? Password { get; set; }
        [Required]
        [DataType(DataType.Password)]
        [Compare("Password", ErrorMessage = "The password and confirmation password does not match.")]
        public string? ConfirmPassword { get; set; }

    }
}
