using MediatR;
using WB.Seller.Companies.Application.Models;

namespace WB.Seller.Companies.Application.Seed;

internal interface IDeepValidator<in TRequest> where TRequest : IBaseRequest
{
    Task<ValidateResult> Handle(TRequest request);
}