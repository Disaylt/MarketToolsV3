using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Input;
using MediatR;
using WB.Seller.Companies.Application.Seed;

namespace WB.Seller.Companies.Application.Commands
{
    public class AddUserCommand : ICommand<Unit>
    {
        public required string UserId { get; set; }
        public required string Login { get; set; }
    }
}
