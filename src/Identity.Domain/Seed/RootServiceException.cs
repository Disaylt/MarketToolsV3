using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Text;
using System.Threading.Tasks;

namespace Identity.Domain.Seed
{
    public class RootServiceException(HttpStatusCode statusCode = HttpStatusCode.BadRequest) : Exception
    {
        private const string NameOfMessage = "Messages";

        private readonly Dictionary<string, List<string>> _keyAndMessagesPairs = new();

        public HttpStatusCode StatusCode { get; } = statusCode;
        public IReadOnlyDictionary<string, IReadOnlyCollection<string>> KeyAndMessagesPairs =>
            _keyAndMessagesPairs.ToDictionary(
                x => x.Key, 
                x => x.Value.AsReadOnly() as IReadOnlyCollection<string>);

        public RootServiceException AddMessages(params string[] messages)
        {
            return AddKeyMessages(NameOfMessage, messages);
        }

        public RootServiceException AddKeyMessages(string key, params string[] messages)
        {
            if (_keyAndMessagesPairs.TryAdd(key, messages.ToList()) == false)
            {
                _keyAndMessagesPairs[key].AddRange(messages);
            }

            return this;
        }
    }
}
