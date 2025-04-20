using NUnit.Framework;
using FluentValidation;
using FluentValidation.TestHelper;
using Bogus;
using Identity.Application.Commands;
using Elastic.CommonSchema;

namespace MarketToolsV3.Users.UnitTests.Tests.Applications.Commands
{
    [TestFixture]
    public class CreateNewUserCommandValidationTests
    {
        private CreateNewUserCommandValidation _validator;
        private Faker _faker;

        private const string Domain = "@test.test";

        [SetUp]
        public void Setup()
        {
            _validator = new CreateNewUserCommandValidation();
            _faker = new Faker();
        }

        private CreateNewUserCommand CreateDefaultCommand()
        {
            return new()
            {
                Email = _faker.Random.String2(1, 150 - Domain.Length) + Domain,
                Login = _faker.Random.String2(6, 150),
                Password = _faker.Random.String2(6, 50)
            };
        }

        [Test]
        public void ValidatePassword_WhenPasswordIsLongerThenMaxLength_ExpectedError()
        {
            var command = CreateDefaultCommand();
            command.Password = _faker.Random.String2(51);

            _validator.TestValidate(command).ShouldHaveValidationErrorFor(c => c.Password);
        }

        [Test]
        public void ValidatePassword_WhenPasswordEqualsMaxLength_ExpectedValid()
        {
            var command = CreateDefaultCommand();
            command.Password = _faker.Random.String2(50);

            _validator.TestValidate(command).ShouldNotHaveValidationErrorFor(c=> c.Password);
        }

        [Test]
        public void ValidatePassword_WhenPasswordEqualsMinLength_ExpectedValid()
        {
            var command = CreateDefaultCommand();
            command.Password = _faker.Random.String2(6);

            _validator.TestValidate(command).ShouldNotHaveValidationErrorFor(c => c.Password);
        }

        [Test]
        public void ValidatePassword_WhenPasswordIsShortedThenMinLength_ExpectedError()
        {
            var command = CreateDefaultCommand();
            command.Password = _faker.Random.String2(5);

            _validator.TestValidate(command).ShouldHaveValidationErrorFor(c => c.Password);
        }

        [Test]
        public void ValidateLogin_WhenLoginIsLongerThenMaxLength_ExpectedError()
        {
            var command = CreateDefaultCommand();
            command.Login = _faker.Random.String2(151);

            _validator.TestValidate(command).ShouldHaveValidationErrorFor(c => c.Login);
        }

        [Test]
        public void ValidateLogin_WhenLoginEqualsMaxLength_ExpectedValid()
        {
            var command = CreateDefaultCommand();
            command.Login = _faker.Random.String2(150);

            _validator.TestValidate(command).ShouldNotHaveValidationErrorFor(c => c.Login);
        }

        [Test]
        public void ValidateLogin_WhenLoginEqualsMinLength_ExpectedValid()
        {
            var command = CreateDefaultCommand();
            command.Login = _faker.Random.String2(6);

            _validator.TestValidate(command).ShouldNotHaveValidationErrorFor(c => c.Login);
        }

        [Test]
        public void ValidateLogin_WhenLoginIsShortedThenMinLength_ExpectedError()
        {
            var command = CreateDefaultCommand();
            command.Login = _faker.Random.String2(5);

            _validator.TestValidate(command).ShouldHaveValidationErrorFor(c => c.Login);
        }

        [Test]
        public void ValidateEmail_WhenEmailIsLongerThenMaxLength_ExpectedError()
        {
            var command = CreateDefaultCommand();
            command.Email = _faker.Random.String2( 151 - Domain.Length) + Domain;

            _validator.TestValidate(command).ShouldHaveValidationErrorFor(c => c.Email);
        }

        [Test]
        public void ValidateEmail_WhenEmailEqualsMaxLength_ExpectedValid()
        {
            var command = CreateDefaultCommand();
            command.Email = _faker.Random.String2(150 - Domain.Length) + Domain;

            _validator.TestValidate(command).ShouldNotHaveValidationErrorFor(c => c.Email);
        }

        [Test]
        public void ValidateEmail_WhenWriteValidEmail_ExpectedValid()
        {
            var command = CreateDefaultCommand();

            _validator.TestValidate(command).ShouldNotHaveValidationErrorFor(c => c.Email);
        }

        [Test]
        public void ValidateEmail_WhenWriteInvalidEmail_ExpectedInvalid()
        {
            var command = CreateDefaultCommand();
            command.Email = _faker.Random.String2(6, 150);

            _validator.TestValidate(command).ShouldHaveValidationErrorFor(c => c.Email);
        }
    }

}