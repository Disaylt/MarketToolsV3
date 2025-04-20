using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace UserNotifications.Applications.Models
{
    public class PaginationDto<T>
    {
        public IEnumerable<T> Items { get; set; } = [];
        public int Total { get; set; }
    }
}
