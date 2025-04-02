using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Identity.Application.Models
{
    public record JwtRefreshTokenDto : IToken
    {
        public required string Id { get; init; }
        public required string AccessTokenId { get; init; }
    }
}
