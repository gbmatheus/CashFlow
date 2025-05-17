using CashFlow.Application.UseCases.Users.Register;
using CashFlow.Exception;
using CommonTestUtilities.Request;
using FluentAssertions;

namespace Validators.Tests.Users.Register
{
    public class RegisterUserValidatorTest
    {
        [Fact]
        public void Sucess()
        {
            //Arrange
            var request = RequestRegisterUserJsonBuilder.Builder();
            var validator = new RegisterUserValidator();

            //Action
            var result = validator.Validate(request);

            //Assert
            result.IsValid.Should().BeTrue();
        }

        [Theory]
        [InlineData("")]
        [InlineData("         ")]
        [InlineData(null)]
        public void Error_Name_Empty(string name)
        {
            var validator = new RegisterUserValidator();
            var request = RequestRegisterUserJsonBuilder.Builder();
            
            request.Name = name;

            var result = validator.Validate(request);

            result.IsValid.Should().BeFalse();
            result.Errors.Should().ContainSingle().And.Contain(e => e.ErrorMessage.Equals(ResourceErrorMessages.NAME_EMPTY));
        }


        [Theory]
        [InlineData("")]
        [InlineData("         ")]
        [InlineData(null)]
        public void Error_Email_Empty(string email)
        {
            var validator = new RegisterUserValidator();
            var request = RequestRegisterUserJsonBuilder.Builder();

            request.Email = email;

            var result = validator.Validate(request);

            result.IsValid.Should().BeFalse();
            result.Errors.Should().ContainSingle().And.Contain(e => e.ErrorMessage.Equals(ResourceErrorMessages.EMAIL_EMPTY));
        }

        [Fact]
        public void Error_Email_Invalid()
        {
            var validator = new RegisterUserValidator(); 
            var request = RequestRegisterUserJsonBuilder.Builder();
            request.Email = request.Name;
            
            var result = validator.Validate(request);

            result.IsValid.Should().BeFalse();
            result.Errors.Should().ContainSingle().And.Contain(e => e.ErrorMessage.Equals(ResourceErrorMessages.EMAIL_INVALID));
        }

        [Fact]
        public void Error_Password_Empty()
        {
            var validator = new RegisterUserValidator();
            var request = RequestRegisterUserJsonBuilder.Builder();
            request.Password = string.Empty;

            var result = validator.Validate(request);

            result.IsValid.Should().BeFalse();
            result.Errors.Should().ContainSingle().And.Contain(e => e.ErrorMessage.Equals(ResourceErrorMessages.PASSWORD_INVALID));
        }
    }
}
