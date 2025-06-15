using MediatR;

namespace MarketToolsV3.PermissionStore.Application.Seed;

public interface ICommand<out T> : IRequest<T>
{

}