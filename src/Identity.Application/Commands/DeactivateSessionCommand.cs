using Identity.Application.DeepValidation;
using Identity.Application.Seed;
using MediatR;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Identity.Application.Commands
{
    public class DeactivateSessionCommand : ICommand<Unit>, ISessionDeepValidationRequest
    {
        public required string Id { get; set; }
        public string UserId { get; set; } = string.Empty;
    }
}
