using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Identity.Application.Models;
using Identity.Application.Seed;

namespace Identity.Application.Commands
{
    public class DeactivateSessionCommandDeepValidate()
        : IDeepValidator<DeactivateSessionCommand>
    {
        public Task<DeepValidateResult> Handle(DeactivateSessionCommand request)
        {
            throw new NotImplementedException();
        }
    }
}
