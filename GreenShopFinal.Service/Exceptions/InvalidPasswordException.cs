using GreenShopFinal.Service.Exceptions.BaseExceptionHandler;
using System.Net;

namespace GreenShopFinal.Service.Exceptions
{
    public class InvalidPasswordException : BaseException
    {
        public InvalidPasswordException(string msg, HttpStatusCode statusCode = HttpStatusCode.InternalServerError) : base(msg, statusCode)
        {
        }
    }
}
