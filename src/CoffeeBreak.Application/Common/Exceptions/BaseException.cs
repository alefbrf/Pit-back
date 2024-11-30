using System.Net;

namespace CoffeeBreak.Application.Common.Exceptions
{
    public class BaseException : Exception
    {
        public HttpStatusCode StatusCode;
        public BaseException(string message, HttpStatusCode statusCode = HttpStatusCode.InternalServerError) : base(message)
        {
            StatusCode = statusCode;
        }
    }
}
