﻿using Identity.Application.Models;
using Identity.Application.Services;
using Identity.Domain.Entities;
using Identity.Domain.Seed;
using MediatR;
using Microsoft.Extensions.Options;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Identity.Application.Queries
{
    public class CheckSessionActiveStatusQueryHandler(ITokenService<JwtRefreshTokenDto> refreshTokenService,
        ICacheRepository<SessionStatusDto> sessionCacheRepository,
        IRepository<Session> sessionRepository,
        IOptions<ServiceConfiguration> options)
        : IRequestHandler<CheckSessionActiveStatusQuery, bool>
    {
        private readonly ServiceConfiguration _configuration = options.Value;

        public async Task<bool> Handle(CheckSessionActiveStatusQuery request, CancellationToken cancellationToken)
        {
            if (await refreshTokenService.IsValid(request.RefreshToken) == false)
            {
                return false;
            }

            JwtRefreshTokenDto refreshTokenData = refreshTokenService.Read(request.RefreshToken);

            SessionStatusDto? sessionStatus = await sessionCacheRepository.GetAsync(refreshTokenData.Id);

            if (sessionStatus == null)
            {
                Session entity = await sessionRepository.FindByIdRequiredAsync(refreshTokenData.Id, cancellationToken);
                SessionStatusDto newSessionStatus = new SessionStatusDto { Id = entity.Id, IsActive = entity.IsActive };

                await sessionCacheRepository.SetAsync(newSessionStatus.Id, 
                    newSessionStatus, 
                    TimeSpan.FromHours(_configuration.ExpireRefreshTokenHours));

                return entity.IsActive;
            }

            return sessionStatus.IsActive;
        }
    }
}