using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Identity.Application.Models
{
    public record JwtAccessTokenDto : IToken
    {
        public required string Id { get; set; }
        public required string SessionId { get; set; }
        public required string UserId { get; init; }
        public List<string> Roles { get; } = [];
        public ModuleAuthInfoDto? ModuleAuthInfo { get; set; }
    }
}
