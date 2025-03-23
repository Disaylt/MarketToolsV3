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
        public required string AccessToken { get; set; }
        public string? ModuleType { get; set; }
        public string? ModulePath { get; set; }
        public int? ModuleId { get; set; }
    }
}
