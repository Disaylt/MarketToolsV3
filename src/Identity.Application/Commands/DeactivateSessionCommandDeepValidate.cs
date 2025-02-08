using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Identity.Application.Models;
using Identity.Application.Seed;
using Identity.Application.Services.Abstract;
using Identity.Application.Utilities.Abstract.Validation;

namespace Identity.Application.Commands
{
    public class DeactivateSessionCommandDeepValidate(IStringIdQuickSearchService<SessionDto> sessionSearchService)
        : BaseDeactivateSessionCommandDeepValidate<DeactivateSessionCommand>
    {
        public override async Task<ValidateResult> Handle(DeactivateSessionCommand request)
        {
            await ValidateId(request);

            return CreateResult();
        }

        private async Task ValidateId(DeactivateSessionCommand request)
        {
            SessionDto session = await sessionSearchService.GetAsync(request.Id, TimeSpan.FromMinutes(15));

            if (session.UserId != request.UserId)
            {
                ErrorMessages.Add("Нет доступа к сессии.");
            }
        }
    }
}
