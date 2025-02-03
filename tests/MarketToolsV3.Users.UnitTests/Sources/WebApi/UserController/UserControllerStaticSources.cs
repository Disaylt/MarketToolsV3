using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Identity.Application.Models;
using Identity.WebApi.Models;
using Moq;

namespace MarketToolsV3.Users.UnitTests.Sources.WebApi.UserController
{
    internal static class UserControllerStaticSources
    {
        public static NewUserModel CreateNewUserBody()
        {
            return new()
            {
                Email = It.IsAny<string>(),
                Login = It.IsAny<string>(),
                Password = It.IsAny<string>()
            };
        }

        public static LoginModel CreateLoginBody()
        {
            return new()
            {
                Email = It.IsAny<string>(),
                Password = It.IsAny<string>()
            };
        }

        public static AuthResultDto CreateAuthResult()
        {
            return new AuthResultDto()
            {
                AuthDetails = new()
                {
                    AuthToken = It.IsAny<string>(),
                    SessionToken = It.IsAny<string>()
                },
                IdentityDetails = new IdentityDetailsDto()
                {
                    Email = It.IsAny<string>(),
                    Id = It.IsAny<string>(),
                    Name = It.IsAny<string>()
                }
            };
        }
    }
}
