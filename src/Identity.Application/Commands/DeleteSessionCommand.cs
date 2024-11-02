﻿using MediatR;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Identity.Application.Commands
{
    public class DeleteSessionCommand : ICommand<Unit>
    {
        public required string Id { get; set; }
    }
}