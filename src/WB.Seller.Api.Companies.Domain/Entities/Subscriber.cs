using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using WB.Seller.Api.Companies.Domain.Seed;

namespace WB.Seller.Api.Companies.Domain.Entities
{
    public class Subscriber : Entity
    {
        public string SubId { get; private set; }
        public string Email { get; private set; }
        public string? Note {get; private set; }


        protected Subscriber()
        {
            SubId = null!;
            Email = null!;
        }

        public Subscriber(string id, string email, string? note) : this()
        {
            SubId = id;
            Email = email;
            Note = note;
        }
    }
}
