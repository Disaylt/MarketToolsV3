using Identity.Domain.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Identity.Domain.Seed;

namespace Identity.Application.QueryObjects
{
    public class FindModuleQueryObject : IQueryObject<Module>
    {
        public required string Path { get; set; }
        public required string UserId { get; set; }
        public int Id { get; set; }
    }
}
