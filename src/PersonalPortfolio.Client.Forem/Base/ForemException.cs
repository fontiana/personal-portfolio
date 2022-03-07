using System;
using System.Net;

namespace PersonalPortfolio.Client.Forem.Base
{
    public class ForemException : Exception
    {
        public string Content { get; set; }
        public HttpStatusCode StatusCode { get; set; }

        public ForemException(string message, string content, HttpStatusCode statusCode) : base(message)
        {
            Content = content;
            StatusCode = statusCode;
        }
    }
}
