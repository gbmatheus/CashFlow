namespace CashFlow.Exception.ExceptionBase
{
    public class ErroOnValidationException : CashFlowException
    {

        public List<string> Errors { get; set; }

        public ErroOnValidationException(List<string> errorMessages) : base(string.Empty)
        {
            Errors = errorMessages;
        }
    }
}
