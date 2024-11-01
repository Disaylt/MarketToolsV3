﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Identity;

namespace Identity.Domain.Entities
{
    public class IdentityPerson : IdentityUser
    {
        public DateTime CreateDate { get; } = DateTime.UtcNow;

        private readonly List<Session> _sessions = new List<Session>();
        public IReadOnlyCollection<Session> Sessions => _sessions.AsReadOnly();
    }
}