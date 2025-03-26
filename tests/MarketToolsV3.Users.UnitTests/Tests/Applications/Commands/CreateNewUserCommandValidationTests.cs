using NUnit.Framework;
using FluentValidation;
using FluentValidation.TestHelper;
using Bogus;
using Identity.Application.Commands;

namespace MarketToolsV3.Users.UnitTests.Tests.Applications.Commands
{
    [TestFixture]
    public class CreateNewUserCommandValidationTests
    {
        private CreateNewUserCommandValidation _validator;
        private Faker _faker;

        [SetUp]
        public void Setup()
        {
            _validator = new CreateNewUserCommandValidation();
            _faker = new Faker();
        }

        [Test]
        public void Validate_Login_Should_Have_Error_For_Invalid_Login()
        {
            var command = new CreateNewUserCommand
            {
                Login = _faker.Random.String2(2),
                Password = _faker.Internet.Password(12),
                Email = _faker.Internet.Email()
            };
            _validator.TestValidate(command).ShouldHaveValidationErrorFor(c => c.Login);
        }

        [Test]
        public void Validate_Login_Should_Not_Have_Error_For_Valid_Login()
        {
            var command = new CreateNewUserCommand
            {
                Login = _faker.Internet.UserName(),
                Password = _faker.Internet.Password(12),
                Email = _faker.Internet.Email()
            };
            _validator.TestValidate(command).ShouldNotHaveValidationErrorFor(c => c.Login);
        }

        [Test]
        public void Validate_Password_Should_Have_Error_For_Invalid_Password()
        {
            var command = new CreateNewUserCommand
            {
                Login = _faker.Internet.UserName(),
                Password = _faker.Random.String2(3),
                Email = _faker.Internet.Email()
            };
            _validator.TestValidate(command).ShouldHaveValidationErrorFor(c => c.Password);
        }

        [Test]
        public void Validate_Password_Should_Not_Have_Error_For_Valid_Password()
        {
            var command = new CreateNewUserCommand
            {
                Login = _faker.Internet.UserName(),
                Password = _faker.Internet.Password(12),
                Email = _faker.Internet.Email()
            };
            _validator.TestValidate(command).ShouldNotHaveValidationErrorFor(c => c.Password);
        }

        [Test]
        public void Validate_Email_Should_Have_Error_For_Invalid_Email()
        {
            var command = new CreateNewUserCommand
            {
                Login = _faker.Internet.UserName(),
                Password = _faker.Internet.Password(12),
                Email = "invalid-email"
            };
            _validator.TestValidate(command).ShouldHaveValidationErrorFor(c => c.Email);
        }

        [Test]
        public void Validate_Email_Should_Not_Have_Error_For_Valid_Email()
        {
            var command = new CreateNewUserCommand
            {
                Login = _faker.Internet.UserName(),
                Password = _faker.Internet.Password(12),
                Email = _faker.Internet.Email()
            };
            _validator.TestValidate(command).ShouldNotHaveValidationErrorFor(c => c.Email);
        }
    }
}
