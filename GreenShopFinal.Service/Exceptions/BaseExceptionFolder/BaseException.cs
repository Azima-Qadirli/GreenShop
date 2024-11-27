using System.Net;

namespace GreenShopFinal.Service.Exceptions.BaseExceptionHandler
{
    public class BaseException : Exception
    {
        public HttpStatusCode StatusCode { get; set; }

        public BaseException(string msg, HttpStatusCode statusCode = HttpStatusCode.InternalServerError)
        {
            StatusCode = statusCode;
        }
    }
}
