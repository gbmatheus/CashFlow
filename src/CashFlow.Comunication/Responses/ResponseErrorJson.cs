namespace CashFlow.Comunication.Responses
{
    public class ResponseErrorJson
    {
        public string ErrorMessage {  get; set; } = string.Empty;

        public ResponseErrorJson(string errorMensage) { 
            ErrorMessage = errorMensage;
        }
    }
}
