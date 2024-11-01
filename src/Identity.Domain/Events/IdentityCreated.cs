using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Identity.Domain.Entities;
using MediatR;

namespace Identity.Domain.Events
{
    public class IdentityCreated(IdentityPerson identity) : INotification
    {
        public IdentityPerson Identity => identity;
    }
}
