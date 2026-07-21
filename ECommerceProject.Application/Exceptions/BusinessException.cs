using System;
using System.Collections.Generic;
using System.Text;

namespace ECommerceProject.Application.Exceptions
{
    public class BusinessException : Exception
    {
        public BusinessException(string? message) : base(message)
        {
        }
    }
}
