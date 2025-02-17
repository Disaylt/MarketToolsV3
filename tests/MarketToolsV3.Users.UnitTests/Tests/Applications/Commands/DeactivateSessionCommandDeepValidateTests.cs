using System;
using System.Threading.Tasks;
using Identity.Application.Commands;
using Identity.Application.Models;
using Identity.Application.Services.Abstract;
using Identity.Application.Utilities.Abstract.Validation;
using Moq;
using NUnit.Framework;

namespace Identity.Application.UnitTests.Commands
{
    [TestFixture]
    public class DeactivateSessionCommandDeepValidateTests
    {
        private Mock<IStringIdQuickSearchService<SessionDto>> _sessionSearchServiceMock;
        private DeactivateSessionCommandDeepValidate _validator;

        [SetUp]
        public void Setup()
        {
            _sessionSearchServiceMock = new Mock<IStringIdQuickSearchService<SessionDto>>();
            _validator = new DeactivateSessionCommandDeepValidate(_sessionSearchServiceMock.Object);
        }

        [Test]
        public async Task Handle_InvalidData_ShouldReturnValidationError()
        {
            var request = new DeactivateSessionCommand { Id = "invalid-id", UserId = "user123" };

            _sessionSearchServiceMock
                .Setup(service => service.GetAsync(request.Id, TimeSpan.FromMinutes(15)))
                .ReturnsAsync(new SessionDto
                {
                    Id = "invalid-id",
                    UserAgent = "TestAgent",
                    CreateDate = DateTime.UtcNow,
                    Updated = DateTime.UtcNow,
                    IsActive = true,
                    UserId = "differentUser"
                });

            var result = await _validator.Handle(request);

            Assert.That(result.IsValid, Is.False);
            Assert.That(result.ErrorMessages, Contains.Item("Нет доступа к сессии."));
        }

        [Test]
        public async Task Handle_ValidData_ShouldPassValidation()
        {
            var request = new DeactivateSessionCommand { Id = "valid-id", UserId = "user123" };

            _sessionSearchServiceMock
                .Setup(service => service.GetAsync(request.Id, TimeSpan.FromMinutes(15)))
                .ReturnsAsync(new SessionDto
                {
                    Id = "valid-id",
                    UserAgent = "TestAgent",
                    CreateDate = DateTime.UtcNow,
                    Updated = DateTime.UtcNow,
                    IsActive = true,
                    UserId = "user123"
                });

            var result = await _validator.Handle(request);

            Assert.That(result.IsValid, Is.True);
            Assert.That(result.ErrorMessages, Is.Empty);
        }
    }
}
