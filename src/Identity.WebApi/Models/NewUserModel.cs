using System.ComponentModel.DataAnnotations;

namespace Identity.WebApi.Models
{
    public class NewUserModel
    {
        [Required(ErrorMessage = "Введите почту.")]
        [EmailAddress(ErrorMessage = "Почта введена с ошибками.")]
        public required string Email { get; set; }

        [Required(ErrorMessage = "Введите логин.")]
        [MinLength(4, ErrorMessage = "Минимальная длинна логина 4 символов.")]
        public required string Login { get; set; }

        [Required(ErrorMessage = "Введите пароль.")]
        [MinLength(6, ErrorMessage = "Минимальная длинна пароля 6 символов.")]
        public required string Password { get; set; }
    }
}
