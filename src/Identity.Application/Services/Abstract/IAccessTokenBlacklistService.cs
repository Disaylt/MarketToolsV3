using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Identity.Application.Services.Abstract
{
    public interface IAccessTokenBlacklistService
    {
        Task AddAsync(string id, CancellationToken cancellationToken);
    }
}
