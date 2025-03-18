using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Identity.Application.Models
{
    public class ServiceAuthInfoDto
    {
        public int ProviderType { get; set; }
        public int ProviderId { get; set; }
        public Dictionary<int, int> ClaimTypeAndValuePairs { get; set; } = new();
    }
}
