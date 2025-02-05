using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Identity.Application.Models;
using Identity.Application.Seed;
using Identity.Application.Services.Abstract;

namespace Identity.Application.Commands
{
    public class DeactivateSessionCommandDeepValidate(IStringIdQuickSearchService<SessionDto> sessionSearchService)
        : IDeepValidator<DeactivateSessionCommand>
    {
        public async Task<ValidateResult> Handle(DeactivateSessionCommand request)
        {
            SessionDto session = await sessionSearchService.GetAsync(request.Id, TimeSpan.FromMinutes(15));

            if (session.UserId != request.UserId)
            {
                return new ValidateResult(false, "У вас нет доступа для манипуляций данной сессией");
            }

            return new ValidateResult(true);
        }
    }
}
