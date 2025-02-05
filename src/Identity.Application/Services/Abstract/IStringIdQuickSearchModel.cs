using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Identity.Application.Seed;

namespace Identity.Application.Services.Abstract
{
    public interface IStringIdQuickSearchModel<TResponse>
        : IBaseQuickSearchModel<TResponse, string>
            where TResponse : IQuickSearchResponse
    {

    }
}
