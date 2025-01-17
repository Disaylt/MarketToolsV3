﻿using MediatR;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Identity.Application.DeepValidation
{
    internal interface IDeepValidator<in TRequest>
    {
        Task Handle(TRequest request);
    }
}
