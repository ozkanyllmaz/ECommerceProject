using System;
using System.Collections.Generic;
using System.Text;

namespace ECommerceProject.Application.Abstractions
{
    public interface IAuthCookieService
    {
        void setDeviceCookie(string deviceId);
        void deleteCookies(string cookieName);
    }
}
