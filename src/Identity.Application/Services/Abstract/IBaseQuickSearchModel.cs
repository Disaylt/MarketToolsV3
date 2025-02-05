using Identity.Application.Seed;

namespace Identity.Application.Services.Abstract;

public interface IBaseQuickSearchModel<TResponse, in TKey>
    where TResponse : IQuickSearchResponse

{
    Task<TResponse> GetAsync(TKey id, TimeSpan expire);
    Task ClearAsync(TKey id);
}