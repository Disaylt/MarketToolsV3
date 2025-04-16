using Identity.Domain.Seed;
using MediatR;
using Microsoft.Extensions.Logging;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Identity.Application.Seed;
using MarketToolsV3.EventLogBus.Services.Abstract;

namespace Identity.Application.Behaviors
{
    public class TransactionBehavior<TRequest, TResponse>(IUnitOfWork unitOfWork,
        ILogger<TransactionBehavior<TRequest, TResponse>> logger,
        IEventLogBus eventLogBus)
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

                logger.LogInformation("Create transaction id - {transactionId}.", transactionId.Value);

                TResponse response = await next(cancellationToken);

                await unitOfWork.CommitAsync(cancellationToken);

                logger.LogInformation("Transaction id - {transactionId} commited.", transactionId.Value);

                await eventLogBus.PublishNewByTransactionAsync(transactionId.Value, cancellationToken);

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
