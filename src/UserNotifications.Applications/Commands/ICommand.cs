﻿using MediatR;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace UserNotifications.Applications.Commands
{
    public interface ICommand<out T> : IRequest<T>
    {

    }
}