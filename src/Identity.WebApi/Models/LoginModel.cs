using System.ComponentModel.DataAnnotations;

namespace Identity.WebApi.Models
{
    public class LoginModel
    {
        [EmailAddress(ErrorMessage = "Почта введена неверно.")]
        [Required(ErrorMessage = "Введите почту.")]
        public required string Email { get; set; }

        [Required(ErrorMessage = "Введите пароль.")]
        public required string Password { get; set; }
    }
}
