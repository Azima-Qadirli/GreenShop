using GreenShopFinal.Service.Exceptions.BaseExceptionHandler;
using System.Net;

namespace GreenShopFinal.Service.Exceptions
{
    public class UserNotFoundException : BaseException
    {
        public UserNotFoundException(string msg, HttpStatusCode statusCode = HttpStatusCode.NotFound) : base(msg, statusCode)
        {
        }
    }
}
