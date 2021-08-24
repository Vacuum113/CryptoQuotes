using System;
using System.Net;

namespace Application.Exceptions
{
    public class RestException : Exception
    {
        public RestException(HttpStatusCode code, string error = null)
        {
            Code = code;
            Error = error;
        }

        public HttpStatusCode Code { get; }

        public string Error { get; set; }
    }
}
