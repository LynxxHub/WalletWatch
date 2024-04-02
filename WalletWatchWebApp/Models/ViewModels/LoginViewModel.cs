using System.ComponentModel.DataAnnotations;

namespace WalletWatchWebApp.Models.ViewModels
{
    public class LoginViewModel
    {
        [Required]
        [StringLength(100, MinimumLength = 3, ErrorMessage = "Username must be between 3 and 100 characters.")]
        [RegularExpression("^[a-zA-Z0-9]*$", ErrorMessage = "Username must only contain letters and numbers.")]
        public string Username { get; set; }

        [Required]
        [StringLength(100, MinimumLength = 6, ErrorMessage = "Password must be at least 6 characters long.")]
        [DataType(DataType.Password)]
        public string Password { get; set; }

        [Required]
        public bool RememberMe { get; set; }
    }
}
