using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace WB.Seller.Companies.Domain.Enums
{
    public enum CompanyState
    {
        [Description("Ожидает подтверждение токена")]
        AwaitTokenConfirmation,

        [Description("Активно")]
        Active,

        [Description("Недействительный токен")]
        InvalidToken
    }
}
