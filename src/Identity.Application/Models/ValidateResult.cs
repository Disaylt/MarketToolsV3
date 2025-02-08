using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Identity.Application.Models
{
    public class ValidateResult(bool isValid, params string[] errorMessage)
    {
        public bool IsValid { get; } = isValid;
        public IReadOnlyCollection<string> ErrorMessages { get; } = errorMessage;
    }
}
