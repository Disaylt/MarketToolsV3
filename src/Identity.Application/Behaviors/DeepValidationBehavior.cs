using Identity.Application.Seed;
using MediatR;
using Microsoft.Extensions.Logging;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Identity.Application.Behaviors
{
    internal class DeepValidationBehavior<TRequest, TResponse>(IEnumerable<IDeepValidator<TRequest>> deepValidators)
    : IPipelineBehavior<TRequest, TResponse> where TRequest : IRequest<TResponse>
    {
        public async Task<TResponse> Handle(TRequest request, RequestHandlerDelegate<TResponse> next, CancellationToken cancellationToken)
        {

            foreach (var deepValidator in deepValidators)
            {
                await deepValidator.Handle(request);
            }

            return await next();
        }
    }
}
