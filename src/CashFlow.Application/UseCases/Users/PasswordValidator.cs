using CashFlow.Exception;
using FluentValidation;
using FluentValidation.Validators;
using System.Text.RegularExpressions;

namespace CashFlow.Application.UseCases.Users
{
    public partial class PasswordValidator<T> : PropertyValidator<T, string>
    {
        private const string ERROR_MESSAGE = "ErrorMessage";
        public override string Name => "PasswordValidator";

        protected override string GetDefaultMessageTemplate(string errorCode)
        {
            return $"{{{ERROR_MESSAGE}}}";
        }

        public override bool IsValid(ValidationContext<T> context, string value)
        {
            if (string.IsNullOrWhiteSpace(value))
            {
                context.MessageFormatter.AppendArgument(ERROR_MESSAGE, ResourceErrorMessages.PASSWORD_INVALID);
                return false;
            }

            if (value.Length < 8)
            {
                context.MessageFormatter.AppendArgument(ERROR_MESSAGE, ResourceErrorMessages.PASSWORD_INVALID);
                return false;
            }

            if (!UppercaseLetter().IsMatch(value))
            {
                context.MessageFormatter.AppendArgument(ERROR_MESSAGE, ResourceErrorMessages.PASSWORD_INVALID);
                return false;
            }

            if (!LowercaseLetter().IsMatch(value))
            {
                context.MessageFormatter.AppendArgument(ERROR_MESSAGE, ResourceErrorMessages.PASSWORD_INVALID);
                return false;
            }

            if (!Number().IsMatch(value))
            {
                context.MessageFormatter.AppendArgument(ERROR_MESSAGE, ResourceErrorMessages.PASSWORD_INVALID);
                return false;
            }

            if (!SpecialSymbol().IsMatch(value))
            {
                context.MessageFormatter.AppendArgument(ERROR_MESSAGE, ResourceErrorMessages.PASSWORD_INVALID);
                return false;
            }

            return true;
        }

        [GeneratedRegex(@"[\!\?\*\.]+")]
        private static partial Regex SpecialSymbol();

        [GeneratedRegex(@"[A-Z]+")]
        private static partial Regex UppercaseLetter();

        [GeneratedRegex(@"[a-z]+")]
        private static partial Regex LowercaseLetter();

        [GeneratedRegex(@"[0-9]+")]
        private static partial Regex Number();

    }
}
