using Identity.Infrastructure.Services;
using Newtonsoft.Json.Linq;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Security.Claims;
using System.Text;
using System.Threading.Tasks;

namespace MarketToolsV3.Users.UnitTests.Tests.Infrastructure.Services
{
    internal class RolesClaimServiceTest
    {
        [TestCase("1")]
        [TestCase("2")]
        [TestCase("3")] 
        public void Create_RoleName_ContainsValue(string value)
        {
            RolesClaimService serviceTest = new RolesClaimService();

            IEnumerable<string> values = new List<string> { value };

            IEnumerable<Claim> claims = serviceTest.Create(values);

            Assert.Contains(value, claims.Select(x=> x.Value).ToList());
        }

        [Test]
        public void Create_ReturnAllTypesAsRole()
        {
            RolesClaimService serviceTest = new RolesClaimService();

            IEnumerable<string> values = new List<string> { "", "", "" };

            bool isAllRoleType = serviceTest.Create(values)
                .Select(x=> x.Type)
                .All(x=> x == ClaimTypes.Role);

            Assert.That(isAllRoleType, Is.True);
        }
    }
}
