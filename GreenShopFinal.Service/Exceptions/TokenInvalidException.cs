using GreenShopFinal.Service.Exceptions.BaseExceptionHandler;
using System.Net;

namespace GreenShopFinal.Service.Exceptions
{
    public class TokenInvalidException : BaseException
    {
        public TokenInvalidException(string msg, HttpStatusCode statusCode = HttpStatusCode.InternalServerError) : base(msg, statusCode)
        {
        }
    }
}
