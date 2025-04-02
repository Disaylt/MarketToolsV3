using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Text;
using System.Text.Json;
using System.Threading.Tasks;
using Identity.Application.Services.Abstract;
using Identity.Domain.Entities;
using Identity.Domain.Events;
using Identity.Domain.Seed;
using Identity.Infrastructure.Repositories;
using Microsoft.AspNetCore.Identity;
using Microsoft.Extensions.Logging;

namespace Identity.Infrastructure.Services.Implementation
{
    public class IdentityPersonService(UserManager<IdentityPerson> userManager,
        IRepository<IdentityPerson> identityRepository,
        ILogger<IdentityPersonService> logger,
        IEventRepository eventRepository)
        : IIdentityPersonService
    {
        public async Task<IdentityPerson> AddAsync(IdentityPerson identity, string password)
        {
            IdentityResult result = await userManager.CreateAsync(identity, password);

            if (result.Succeeded == false)
            {
                logger.LogWarning("Bad create identity. Errors - {error}",
                    string.Join(',', result.Errors.Select(er => er.Description)));
                throw new RootServiceException()
                    .AddMessages("Не удалось создать пользователя, попробуйте изменить данные или обратитесь в тех. поддержку.");
            }

            logger.LogInformation("Create new identity - {id}", identity.Id);

            IdentityCreated registerEvent = new(identity);
            eventRepository.AddNotification(registerEvent);

            await identityRepository.UnitOfWork.SaveEntitiesAsync();

            return identity;
        }

        public async Task<bool> CheckPassword(IdentityPerson identity, string password)
        {
            logger.LogInformation("Check password for {name}", identity.UserName);

            return await userManager.CheckPasswordAsync(identity, password);
        }

        public async Task<IdentityPerson?> FindByEmailAsync(string email)
        {
            logger.LogInformation("Find identity by email - {email}", email);

            return await userManager.FindByEmailAsync(email);
        }

        public async Task<IdentityPerson?> FindByIdAsync(string id)
        {
            return await userManager.FindByIdAsync(id);
        }

        public async Task<IdentityPerson> FindByIdRequiredAsync(string id)
        {
            return await userManager.FindByIdAsync(id)
                   ?? throw new RootServiceException(HttpStatusCode.NotFound)
                       .AddMessages("Пользователь не найден.");
        }
    }
}
