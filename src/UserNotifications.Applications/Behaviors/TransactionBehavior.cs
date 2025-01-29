using MediatR;
using Microsoft.Extensions.Logging;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using UserNotifications.Applications.Seed;
using UserNotifications.Domain.Seed;

namespace UserNotifications.Applications.Behaviors
{
    public class TransactionBehavior<TRequest, TResponse>(IUnitOfWork unitOfWork,
        ILogger<TransactionBehavior<TRequest, TResponse>> logger)
        : IPipelineBehavior<TRequest, TResponse> where TRequest : ICommand<TResponse>
    {
        public async Task<TResponse> Handle(TRequest request, RequestHandlerDelegate<TResponse> next, CancellationToken cancellationToken)
        {
            string transactionId = "";

            try
            {
                if (unitOfWork.HasTransaction)
                {
                    return await next();
                }

                await unitOfWork.BeginTransactionAsync();

                logger.LogInformation("Create transaction id - {transactionId}.", transactionId);

                TResponse response = await next();

                await unitOfWork.CommitTransactionAsync();

                logger.LogInformation("Transaction id - {transactionId} commited.", transactionId);

                return response;
            }
            catch (Exception ex)
            {
                if (unitOfWork.HasTransaction)
                {
                    await unitOfWork.RollbackTransactionAsync();
                    logger.LogError(ex, "Error handling transaction id - {transactionId}", transactionId);
                }

                throw;
            }
        }
    }
}
