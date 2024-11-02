using Identity.Application.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Identity.Application.Commands
{
    public class CreateAuthInfo : ICommand<AuthInfoDto>
    {
        public required AuthDetailsDto Details { get; set; }
        public string UserAgent { get; set; } = "Unknown";
    }
}
