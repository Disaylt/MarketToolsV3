using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Identity.Domain.Seed;

namespace Identity.Application.Models
{
    public record ValidateResult(bool IsValid, RootServiceException Exception)
    {
    }
}
