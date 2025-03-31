using Identity.Application.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Identity.Application.Services.Abstract
{
    public interface ITokenService<T> where T : IToken
    {
        public string Create(T value);
        public T Read(string token);
        public Task<bool> IsValid(string token);
    }
}
