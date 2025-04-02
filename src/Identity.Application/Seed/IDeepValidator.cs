using Identity.Application.Models;
using MediatR;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Identity.Application.Seed
{
    internal interface IDeepValidator<in TRequest> where TRequest : IBaseRequest
    {
        Task<ValidateResult> Handle(TRequest request, CancellationToken cancellationToken);
    }
}
