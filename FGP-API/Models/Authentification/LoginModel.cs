using System.ComponentModel.DataAnnotations;

namespace FGP_API.Models.Authentification
{
    public class LoginModel
    {
        [Required(ErrorMessage = "Username or Email is required")]
        public string? Identifier { get; set; }

        [Required(ErrorMessage = "Password is required")]
        public string? Password { get; set; }

        [Required(ErrorMessage = "Purpose is required")]
        public string? Purpose { get; set; }

        [Required(ErrorMessage = "ApplicationName is required")]
        public string? ApplicationName { get; set; }
    }
}
