using FluentValidation;
using MediatR;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Text;
using System.Threading.Tasks;
using Identity.Domain.Seed;
using Identity.Application.Commands;
using Identity.Application.Models;
using System.Threading;
using FluentValidation.Results;

namespace Identity.Application.Behaviors
{
    internal class ValidationBehavior<TRequest, TResponse>(IEnumerable<IValidator<TRequest>> validators)
        : IPipelineBehavior<TRequest, TResponse> where TRequest : IRequest<TResponse>
    {
        public async Task<TResponse> Handle(TRequest request, RequestHandlerDelegate<TResponse> next, CancellationToken cancellationToken)
        {
            var validationResults = await HandleValidatorsAsync(request, cancellationToken);
            var errorDetails = CollectErrorDetails(validationResults);

            if (errorDetails.Length > 0)
            {
                throw CreatePrimeException(errorDetails);
            }

            return await next();
        }

        private RootServiceException CreatePrimeException((string PropertyName, string ErrorMessage)[] exceptions)
        {
            RootServiceException generalException = new RootServiceException();

            foreach (var problemDetail in exceptions)
            {
                if (string.IsNullOrEmpty(problemDetail.PropertyName))
                {
                    generalException.AddMessages(problemDetail.ErrorMessage);
                }
                else
                {
                    generalException.AddKeyMessages(problemDetail.PropertyName, problemDetail.ErrorMessage);
                }
            }

            return generalException;
        }

        private (string PropertyName, string ErrorMessage)[] CollectErrorDetails(
            ValidationResult[] validationResults)
        {
            return validationResults
                .Where(r => r.Errors.Count > 0)
                .SelectMany(r => r.Errors)
                .Select(e => (e.PropertyName, e.ErrorMessage))
                .ToArray();
        }

        private async Task<ValidationResult[]> HandleValidatorsAsync(TRequest request, CancellationToken cancellationToken)
        {
            if (validators.Any() == false)
            {
                return [];
            }

            var context = new ValidationContext<TRequest>(request);

            return await Task.WhenAll(
            validators.Select(v =>
                    v.ValidateAsync(context, cancellationToken)));
        }
    }
}
