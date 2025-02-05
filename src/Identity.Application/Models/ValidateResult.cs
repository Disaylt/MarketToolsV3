using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Identity.Application.Models
{
    public class ValidateResult(bool isValid, string errorMessage = "Ошибка валидации не указана.")
    {
        public bool IsValid { get; } = isValid;
        public string ErrorMessage { get; } = errorMessage;
    }
}
