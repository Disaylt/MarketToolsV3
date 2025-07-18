﻿using MediatR;
using Microsoft.Extensions.Logging;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using WB.Seller.Companies.Application.Seed;
using WB.Seller.Companies.Domain.Seed;

namespace WB.Seller.Companies.Application.Behaviors
{
    public class TransactionBehavior<TRequest, TResponse>(IUnitOfWork unitOfWork,
        ILogger<TransactionBehavior<TRequest, TResponse>> logger)
        : IPipelineBehavior<TRequest, TResponse> where TRequest : ICommand<TResponse>
    {
        public async Task<TResponse> Handle(TRequest request, RequestHandlerDelegate<TResponse> next, CancellationToken cancellationToken)
        {
            Guid? transactionId = null;

            try
            {
                if (unitOfWork.HasTransaction)
                {
                    return await next(cancellationToken);
                }

                transactionId = await unitOfWork.BeginTransactionAsync(cancellationToken);

                logger.LogInformation("Create transaction id - {transactionId}.", transactionId);

                TResponse response = await next(cancellationToken);

                await unitOfWork.CommitAsync(cancellationToken);

                logger.LogInformation("Transaction id - {transactionId} commited.", transactionId);

                return response;
            }
            catch (Exception ex)
            {
                if (transactionId != null)
                {
                    await unitOfWork.RollbackAsync(cancellationToken);
                    logger.LogError(ex, "Error handling transaction id - {transactionId}", transactionId.Value);
                }

                throw;
            }
        }
    }
}
