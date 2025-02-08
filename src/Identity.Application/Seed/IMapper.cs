using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Identity.Application.Seed
{
    public interface IMapper<out TResponse, in TModel>
        where TResponse : IMap
    {
        TResponse Map(TModel model);
    }
}
