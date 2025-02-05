using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Identity.Application.Seed;

namespace Identity.Application.Services.Abstract
{
    public interface IStringIdQuickSearchService<TResponse>
        : IBaseQuickSearchService<TResponse, string>
            where TResponse : IQuickSearchResponse
    {

    }
}
