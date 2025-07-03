using MediatR;

namespace WB.Seller.Companies.Application.Commands;

public class AddNewSubscriberCommandHandler
    : IRequestHandler<AddNewSubscriberCommand, Unit>
{
    public Task<Unit> Handle(AddNewSubscriberCommand request, CancellationToken cancellationToken)
    {
        

        throw new NotImplementedException();
    }
}