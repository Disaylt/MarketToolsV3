using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace WB.Seller.Api.Companies.Domain.Entities
{
    public class Subscriber
    {
        public string Id { get; private set; }
        public string Email { get; private set; }
        public string? Note {get; private set; }

        protected Subscriber()
        {
            Id = null!;
            Email = null!;
        }

        public Subscriber(string id, string email, string? note) : this()
        {
            Id = id;
            Email = email;
            Note = note;
        }
    }
}
