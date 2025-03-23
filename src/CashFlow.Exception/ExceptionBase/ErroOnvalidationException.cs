using System.Net;

namespace CashFlow.Exception.ExceptionBase
{
    public class ErroOnValidationException : CashFlowException
    {
        private readonly List<string> _errors;

        public ErroOnValidationException(List<string> errorMessages) : base(string.Empty)
        {
            _errors = errorMessages;
        }

        public override int StatusCode => (int)HttpStatusCode.BadRequest;

        public override List<string> GetErrors()
        {
            return _errors;
        }
    }
}
