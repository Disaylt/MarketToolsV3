﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Text;
using System.Threading;
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
            IReadOnlyCollection<ValidateResult> deepValidateResults = await HandleValidatorsAsync(request, cancellationToken);

            var exceptions = CollectInvalidResults(deepValidateResults);

            if (exceptions.Length > 0)
            {
                throw CreatePrimeException(exceptions);
            }

            return await next(cancellationToken);
        }

        private RootServiceException CreatePrimeException(RootServiceException[] exceptions)
        {
            RootServiceException generalException = new RootServiceException();

            var problemDetails = exceptions.SelectMany(x => x.KeyAndMessagesPairs);

            foreach (var problemDetail in problemDetails)
            {
                generalException.AddKeyMessages(problemDetail.Key, problemDetail.Value.ToArray());
            }

            return generalException;
        }

        private RootServiceException[] CollectInvalidResults(IReadOnlyCollection<ValidateResult> results)
        {
            return results
                .Where(x => x.IsValid == false)
                .Select(x => x.Exception)
                .ToArray();
        }

        private async Task<IReadOnlyCollection<ValidateResult>> HandleValidatorsAsync(TRequest request, CancellationToken cancellationToken)
        {
            if (validators.Any() == false)
            {
                return [];
            }

            List<ValidateResult> deepValidateResults = [];

            foreach (var validator in validators)
            {
                var result = await validator.Handle(request, cancellationToken);
                deepValidateResults.Add(result);
            }

            return deepValidateResults;
        }
    }
}
