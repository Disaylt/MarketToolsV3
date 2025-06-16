using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace WB.Seller.Features.Automation.PriceManager.Application.Services.Abstract
{
    public interface IExternalPermissionsService
    {
        Task RefreshPermissionsAsync();
    }
}
