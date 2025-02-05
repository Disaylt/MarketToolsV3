using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Identity.Application.Seed;
using Identity.Domain.Entities;

namespace Identity.Application.Mappers.Abstract
{
    public interface ISessionMapper<out TResponse> 
        : IMapper<TResponse, Session> 
        where TResponse : IMap
    {
    }
}
