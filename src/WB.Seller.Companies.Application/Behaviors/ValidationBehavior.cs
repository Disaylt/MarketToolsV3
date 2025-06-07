using FluentValidation;
using MediatR;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Text;
using System.Threading.Tasks;
using WB.Seller.Companies.Domain.Seed;

namespace WB.Seller.Companies.Application.Behaviors
{
    internal class ValidationBehavior<TRequest, TResponse>(IEnumerable<IValidator<TRequest>> validators)
        : IPipelineBehavior<TRequest, TResponse> where TRequest : IRequest<TResponse>
    {
        public async Task<TResponse> Handle(TRequest request, RequestHandlerDelegate<TResponse> next, CancellationToken cancellationToken)
        {
            if (validators.Any())
            {
                var context = new ValidationContext<TRequest>(request);

                var validationResults = await Task.WhenAll(
                    validators.Select(v =>
                        v.ValidateAsync(context, cancellationToken)));

                var errorMessages = validationResults
                    .Where(r => r.Errors.Count > 0)
                    .SelectMany(r => r.Errors)
                    .Select(e => e.ErrorMessage)
                    .ToArray();

                if (errorMessages.Length > 0)
                    throw new RootServiceException(HttpStatusCode.BadRequest, errorMessages);
            }

            return await next(cancellationToken);
        }
    }
}
