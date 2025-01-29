using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace UserNotifications.Domain.Enums
{
    public enum Category
    {
        [Description("Общее")]
        General,
        [Description("Авторизация")]
        Identity,
    }
}
