using CashFlow.Comunication.Enums;
using CashFlow.Comunication.Requests;
using CashFlow.Comunication.Responses;

namespace CashFlow.Application.UseCases.Expenses.Register
{
    public class RegisterExpenseUseCase
    {
        public ResponseRegisteredExpenseJson Execute(RequestRegisterExpenseJson request)
        {
            Validate(request);
            return new ResponseRegisteredExpenseJson();
        }

        private void Validate(RequestRegisterExpenseJson request)
        {
            var titleIsEmpty = string.IsNullOrWhiteSpace(request.Title);
            if(titleIsEmpty)
            {
                throw new ArgumentException("The title is required");
            }

            if(request.Amount <= 0)
            {
                throw new ArgumentException("The Amoun must be greater than zero");
            }
            
            var result = DateTime.Compare(request.Date, DateTime.UtcNow);
            if (result > 0)
            {
                throw new ArgumentException("Expenses cannot be for the future");
            }

            var paymentTypeIsValid = Enum.IsDefined(typeof(PaymentType), request.paymentType);
            if(!paymentTypeIsValid)
            {
                throw new ArgumentException("Payment type is not valid");
            }
        }

    }
}
