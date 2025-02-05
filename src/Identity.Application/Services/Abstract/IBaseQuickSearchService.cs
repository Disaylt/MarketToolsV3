using Identity.Application.Seed;

namespace Identity.Application.Services.Abstract;

public interface IBaseQuickSearchService<TResponse, in TKey>
    where TResponse : IQuickSearchResponse

{
    Task<TResponse> GetAsync(TKey id, TimeSpan expire);
    Task ClearAsync(TKey id);
}