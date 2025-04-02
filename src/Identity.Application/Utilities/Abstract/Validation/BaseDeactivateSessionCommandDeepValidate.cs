using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Identity.Application.Models;
using Identity.Application.Seed;
using MediatR;

namespace Identity.Application.Utilities.Abstract.Validation
{
    public abstract class BaseDeactivateSessionCommandDeepValidate<TRequest> : IDeepValidator<TRequest> where TRequest : IBaseRequest
    {
        protected ICollection<string> ErrorMessages { get; } = [];
        public abstract Task<ValidateResult> Handle(TRequest request, CancellationToken cancellationToken);

        protected ValidateResult CreateResult()
        {
            if (ErrorMessages.Count > 0)
            {
                return new ValidateResult(false, [.. ErrorMessages]);
            }

            return new ValidateResult(true);
        }
    }
}
