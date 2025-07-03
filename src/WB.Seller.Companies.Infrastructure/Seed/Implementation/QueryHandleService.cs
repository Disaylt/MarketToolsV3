using System.Net;
using Microsoft.EntityFrameworkCore;
using WB.Seller.Companies.Application.Seed;
using WB.Seller.Companies.Domain.Seed;

namespace WB.Seller.Companies.Infrastructure.Seed.Implementation;

public class QueryHandleService
    : IQueryHandleService
{
    public async Task<T> FirstAsync<T>(IQueryable<T> query)
        => await query.FirstOrDefaultAsync()
            ?? throw new RootServiceException(HttpStatusCode.NotFound, "Не удалось найти данные удовлетворяющие условию.");

    public Task<T?> FirstOrDefaultAsync<T>(IQueryable<T> query)
        => query.FirstOrDefaultAsync();
}