﻿using MediatR;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using WB.Seller.Companies.Application.Commands;
using WB.Seller.Companies.Application.Queries;
using WB.Seller.Companies.WebApi.Extensions;
using WB.Seller.Companies.WebApi.Models.Companies;

namespace WB.Seller.Companies.WebApi.Controllers.Companies
{
    [Route("api/v{version:apiVersion}/companies")]
    [ApiController]
    [ApiVersion("1")]
    public class CompaniesController(IMediator mediator)
        : ControllerBase
    {
        [HttpGet("slim")]
        [MapToApiVersion("1")]
        [Authorize]
        public async Task<IActionResult> CreateAsync(CancellationToken cancellationToken)
        {
            GetSlimCompaniesQuery query = new()
            {
                UserId = HttpContext.GetUserIdRequired()
            };

            var result = await mediator.Send(query, cancellationToken);

            return Ok(result);
        }
    }
}
