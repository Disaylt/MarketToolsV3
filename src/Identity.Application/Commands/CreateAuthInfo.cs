using Identity.Application.Models;
using Identity.Application.Seed;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Identity.Application.Commands
{
    public class CreateAuthInfo : ICommand<AuthInfoDto>
    {
        public string UserAgent { get; set; } = "Unknown";
        public required string RefreshToken { get; set; }
    }
}
