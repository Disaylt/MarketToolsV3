using Identity.Application.Commands;
using MediatR;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Identity.Application.DeepValidation
{
    public interface ISessionDeepValidationRequest
    {
        string Id { get; set; }
        string UserId { get; set; }
    }

    internal class SessionDeepValidator : IDeepValidator<ISessionDeepValidationRequest>
    {
        public Task Handle(ISessionDeepValidationRequest request)
        {
           
            return Task.CompletedTask;
        }
    }
}
