using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Text;
using System.Threading.Tasks;

namespace GreenShopFinal.Service.Exceptions.BaseExceptionHandler
{
    public class BaseException:Exception
    {
        public HttpStatusCode StatusCode {  get; set; }

        public BaseException(string msg,HttpStatusCode statusCode = HttpStatusCode.InternalServerError)
        {
            StatusCode = statusCode;
        }
    }
}
