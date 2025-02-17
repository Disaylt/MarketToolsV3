using FluentValidation;
using Identity.Application.Commands;
using Identity.Application;
using Moq;
using NUnit.Framework;

namespace MarketToolsV3.Users.UnitTests.Tests.Applications.Commands
{
    [TestFixture]
    public class CreateNewUserCommandValidationTests
    {
        private Mock<CreateNewUserCommandValidation> _validatorMock;

        [SetUp]
        public void Setup()
        {
            _validatorMock = new Mock<CreateNewUserCommandValidation>() { CallBase = true };
        }

        [TestCase("ValidLogin", true)]
        [TestCase("sh", false)]
        [TestCase("", false)]
        [TestCase("12345678901234567890123456789012345678901234567890", false)]
        public void Validate_Login(string login, bool expectedIsValid)
        {
            var command = new CreateNewUserCommand { Login = login, Password = "ValidPassword123", Email = "test@example.com" };
            var result = _validatorMock.Object.Validate(command);
            Assert.That(result.IsValid, Is.EqualTo(expectedIsValid));
        }

        [TestCase("StrongPass123", true)]
        [TestCase("123", false)]
        [TestCase("", false)]
        [TestCase("12345678901234567890123456789012345678901234567890", false)]
        public void Validate_Password(string password, bool expectedIsValid)
        {
            var command = new CreateNewUserCommand { Login = "ValidLogin", Password = password, Email = "test@example.com" };
            var result = _validatorMock.Object.Validate(command);
            Assert.That(result.IsValid, Is.EqualTo(expectedIsValid));
        }
    }
}