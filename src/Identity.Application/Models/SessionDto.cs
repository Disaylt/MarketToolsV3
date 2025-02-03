using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Identity.Application.Utilities.Sessions.Abstract;

namespace Identity.Application.Models
{
    public class SessionDto : ISessionSearchModel
    {
        public required string Id { get; set; }
        public required string UserAgent { get; set; }
        public DateTime CreateDate { get; set; }
        public DateTime Updated { get; set; }
        public bool IsCurrent { get; set; }
    }
}
