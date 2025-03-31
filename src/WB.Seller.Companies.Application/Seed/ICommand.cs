using MediatR;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace WB.Seller.Companies.Application.Seed
{
    public interface ICommand<out T> : IRequest<T>
    {

    }
}
