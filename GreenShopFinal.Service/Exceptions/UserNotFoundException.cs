using GreenShopFinal.Service.Exceptions.BaseExceptionHandler;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Text;
using System.Threading.Tasks;

namespace GreenShopFinal.Service.Exceptions
{
    public class UserNotFoundException : BaseException
    {
        public UserNotFoundException(string msg, HttpStatusCode statusCode = HttpStatusCode.InternalServerError) : base(msg, statusCode)
        {

        }
    }
}
