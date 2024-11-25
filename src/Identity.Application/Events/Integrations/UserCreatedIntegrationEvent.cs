﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Identity.Application.Events.Integrations
{
    public class UserCreatedIntegrationEvent(string userId)
    {
        public string UserId { get; } = userId;
    }
}
