using System;
using System.Collections.Generic;
using System.Text;

namespace ECommerceProject.Application.Exceptions
{
    public class AuthenticationException : Exception
    {
        public AuthenticationException(string? message) : base(message)
        {
        }
    }
}
