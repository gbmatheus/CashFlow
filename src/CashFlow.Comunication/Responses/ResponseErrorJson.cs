namespace CashFlow.Comunication.Responses
{
    public class ResponseErrorJson
    {
        public List<string> ErrorMessages {  get; set; }

        public ResponseErrorJson(string errorMensage) 
        { 
            ErrorMessages = new List<string> { errorMensage };
        }

        public ResponseErrorJson(List<string> errorMensages)
        {
            ErrorMessages = errorMensages;
        }
    }
}
