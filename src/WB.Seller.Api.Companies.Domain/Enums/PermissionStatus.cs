using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace WB.Seller.Api.Companies.Domain.Enums
{
    public enum PermissionStatus
    {
        [Description("Недоступно")]
        None,

        [Description("Только чтение")]
        OnlyRead,

        [Description("Чтение и запись")]
        ReadAndWrite
    }
}
