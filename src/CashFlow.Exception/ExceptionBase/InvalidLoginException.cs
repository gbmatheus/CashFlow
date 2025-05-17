﻿using System.Net;

namespace CashFlow.Exception.ExceptionBase
{
    public class InvalidLoginException : CashFlowException
    {
        public InvalidLoginException(string message) : base(message) { }

        public override int StatusCode => (int)HttpStatusCode.NotFound;

        public override List<string> GetErrors()
        {
            return new List<string> { Message };
        }
    }
}
