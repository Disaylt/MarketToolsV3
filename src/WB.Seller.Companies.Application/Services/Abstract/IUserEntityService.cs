using WB.Seller.Companies.Domain.Entities;

namespace WB.Seller.Companies.Application.Services.Abstract;

public interface IUserEntityService
{
    public Task<UserEntity?> FindUserByLoginAsync(string login);
}