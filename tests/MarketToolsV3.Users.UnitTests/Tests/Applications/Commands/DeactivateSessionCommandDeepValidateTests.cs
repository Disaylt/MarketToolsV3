using NUnit.Framework;
using FluentValidation.TestHelper;
using Identity.Application.Commands;
using Identity.Application.Models;
using Identity.Application.Utilities.Abstract.Validation;
using Moq;
using System;
using System.Threading.Tasks;
using Identity.Application.Services.Abstract;

namespace Identity.Application.UnitTests.Commands
{
    [TestFixture]
    public class DeactivateSessionCommandDeepValidateTests
    {
        private DeactivateSessionCommandDeepValidate _validator;
        private Mock<IStringIdQuickSearchService<SessionDto>> _sessionSearchServiceMock;

        [SetUp]
        public void Setup()
        {
            _sessionSearchServiceMock = new Mock<IStringIdQuickSearchService<SessionDto>>();
            _validator = new DeactivateSessionCommandDeepValidate(_sessionSearchServiceMock.Object);
        }

        [Test]
        public async Task Should_have_error_when_user_id_does_not_match()
        {
            var command = new DeactivateSessionCommand { Id = "valid-id", UserId = "user123" };

            _sessionSearchServiceMock
                .Setup(service => service.GetAsync(command.Id, TimeSpan.FromMinutes(15)))
                .ReturnsAsync(new SessionDto
                {
                    Id = "valid-id",
                    UserAgent = "TestAgent",
                    CreateDate = DateTime.UtcNow,
                    Updated = DateTime.UtcNow,
                    IsActive = true,
                    UserId = "differentUser"
                });

            var result = await _validator.Handle(command);

            Assert.That(result.IsValid, Is.False);
            Assert.That(result.ErrorMessages, Contains.Item("Нет доступа к сессии."));
        }

        [Test]
        public async Task Should_not_have_error_when_session_is_valid()
        {
            var command = new DeactivateSessionCommand { Id = "valid-id", UserId = "user123" };

            _sessionSearchServiceMock
                .Setup(service => service.GetAsync(command.Id, TimeSpan.FromMinutes(15)))
                .ReturnsAsync(new SessionDto
                {
                    Id = "valid-id",
                    UserAgent = "TestAgent",
                    CreateDate = DateTime.UtcNow,
                    Updated = DateTime.UtcNow,
                    IsActive = true,
                    UserId = "user123"
                });

            var result = await _validator.Handle(command);

            Assert.That(result.IsValid, Is.True);
            Assert.That(result.ErrorMessages, Is.Empty);
        }
    }
}
