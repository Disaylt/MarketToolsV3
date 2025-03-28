using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Input;
using Identity.Application.Seed;
using MediatR;

namespace Identity.Application.Commands
{
    public class CreateModuleCommand : ICommand<Unit>
    {
        public required string UserId { get; set; }
        public required string Path { get; set; }
        public int Id { get; set; }
        public IReadOnlyCollection<string> Roles { get; set; } = [];
        public IReadOnlyDictionary<string, int> Claims { get; set; } = new Dictionary<string, int>();
    }
}
