using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Identity.Application.Models;
using Identity.Application.Seed;
using Identity.Domain.Seed;
using MediatR;

namespace Identity.Application.Utilities.Abstract.Validation
{
    public abstract class BaseDeactivateSessionCommandDeepValidate<TRequest> : IDeepValidator<TRequest> where TRequest : IBaseRequest
    {
        protected RootServiceException Exception { get; } = new();
        public abstract Task<ValidateResult> Handle(TRequest request, CancellationToken cancellationToken);

        protected ValidateResult CreateResult()
        {
            if (Exception.KeyAndMessagesPairs.Count > 0)
            {
                return new ValidateResult(false, Exception);
            }

            return new ValidateResult(true, Exception);
        }
    }
}
