using GreenShopFinal.Service.Exceptions.BaseExceptionHandler;
using System.Net;

namespace GreenShopFinal.Service.Exceptions
{
    public class ItemNotFoundException : BaseException
    {
        public ItemNotFoundException(string msg, HttpStatusCode statusCode = HttpStatusCode.NotFound) : base(msg, statusCode)
        {
        }
    }
}
