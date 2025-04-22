using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using UserNotifications.Applications.Models;

namespace UserNotifications.Applications.Utilities.Abstract
{
    public interface IEnumParserUtility
    {
        IReadOnlyCollection<EnumInfoDto> Parse<T>() where T : struct, Enum;
    }
}
