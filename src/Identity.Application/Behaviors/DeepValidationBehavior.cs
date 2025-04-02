using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Text;
using System.Threading.Tasks;
using FluentValidation;
using Identity.Application.Models;
using Identity.Application.Seed;
using Identity.Domain.Seed;
using MediatR;
using Microsoft.Extensions.Logging;

namespace Identity.Application.Behaviors
{
    internal class DeepValidationBehavior<TRequest, TResponse>(IEnumerable<IDeepValidator<TRequest>> validators)
        : IPipelineBehavior<TRequest, TResponse> where TRequest : IRequest<TResponse>
    {
        public async Task<TResponse> Handle(TRequest request, RequestHandlerDelegate<TResponse> next, CancellationToken cancellationToken)
        {
            List<ValidateResult> deepValidateResults = [];

            foreach (var validator in validators)
            {
                var result = await validator.Handle(request);
                deepValidateResults.Add(result);
            }

            string[] messages = deepValidateResults
                .Where(x => x.IsValid == false)
                .SelectMany(x => x.ErrorMessages)
                .ToArray();

            if (messages.Length > 0)
            {
                throw new RootServiceException(messages: messages);
            }

            return await next();
        }
    }
}
