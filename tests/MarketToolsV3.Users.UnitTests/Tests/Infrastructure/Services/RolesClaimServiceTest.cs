using Identity.Infrastructure.Services.Implementation;
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
            RolesClaimService serviceTest = new();

            IEnumerable<string> values = [value];

            IEnumerable<Claim> claims = serviceTest.Create(values);

            Assert.That(claims.Select(x => x.Value).ToList(), Does.Contain(value));
        }

        [Test]
        public void Create_ReturnAllTypesAsRole()
        {
            RolesClaimService serviceTest = new();

            IEnumerable<string> values = ["", "", ""];

            bool isAllRoleType = serviceTest.Create(values)
                .Select(x=> x.Type)
                .All(x=> x == ClaimTypes.Role);

            Assert.That(isAllRoleType, Is.True);
        }
    }
}
