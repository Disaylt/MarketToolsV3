using System.ComponentModel.DataAnnotations;

namespace Identity.WebApi.Models
{
    public class NewUserModel
    {
        [Required(ErrorMessage = "Введите почту.")]
        [EmailAddress(ErrorMessage = "Почта введена с ошибками.")]
        [MaxLength(150, ErrorMessage = "Максимальная длинна почты 150 символов.")]
        public required string Email { get; set; }

        [Required(ErrorMessage = "Введите логин.")]
        [MinLength(6, ErrorMessage = "Минимальная длинна логина 6 символов.")]
        [MaxLength(150, ErrorMessage = "Максимальная длинна логина 150 символов.")]
        public required string Login { get; set; }

        [Required(ErrorMessage = "Введите пароль.")]
        [MinLength(6, ErrorMessage = "Минимальная длинна пароля 6 символов.")]
        [MaxLength(50, ErrorMessage = "Максимальная длинна пароля 50 символов.")]
        public required string Password { get; set; }
    }
}
