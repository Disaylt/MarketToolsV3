﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Text;
using System.Threading.Tasks;

namespace Identity.Domain.Seed
{
    public class RootServiceException(HttpStatusCode statusCode = HttpStatusCode.BadRequest, params string[] messages) : Exception
    {
        public override string Message => string.Join('|', messages);
        public IEnumerable<string> Messages { get; } = messages;
        public HttpStatusCode StatusCode { get; } = statusCode;
    }
}