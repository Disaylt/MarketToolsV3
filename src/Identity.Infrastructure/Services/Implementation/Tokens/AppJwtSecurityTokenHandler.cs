using Identity.Infrastructure.Services.Abstract.Tokens;
using System;
using System.Collections.Generic;
using System.IdentityModel.Tokens.Jwt;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Identity.Infrastructure.Services.Implementation.Tokens
{
    public class AppJwtSecurityTokenHandler : JwtSecurityTokenHandler, IJwtSecurityTokenHandler
    {
    }
}
