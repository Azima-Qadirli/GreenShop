using GreenShopFinal.Service.Exceptions.BaseExceptionHandler;
using System.Net;

namespace GreenShopFinal.Service.Exceptions
{
    public class CategoryNotFound : BaseException
    {
        public CategoryNotFound(string msg, HttpStatusCode statusCode = HttpStatusCode.InternalServerError) : base(msg, statusCode)
        {
        }
    }
}
