﻿using GreenShopFinal.Service.Exceptions.BaseExceptionHandler;
using System.Net;

namespace GreenShopFinal.Service.Exceptions
{
    public class ProductNotFoundException : BaseException
    {
        public ProductNotFoundException(string msg, HttpStatusCode statusCode = HttpStatusCode.InternalServerError) : base(msg, statusCode)
        {
        }
    }
}
