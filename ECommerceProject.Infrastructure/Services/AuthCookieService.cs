using ECommerceProject.Application.Abstractions;
using Microsoft.AspNetCore.Http;
using System;
using System.Collections.Generic;
using System.Text;

namespace ECommerceProject.Infrastructure.Services
{
    public class AuthCookieService : IAuthCookieService
    {
        private readonly IHttpContextAccessor _httpContextAccessor;

        public AuthCookieService(IHttpContextAccessor httpContextAccessor)
        {
            _httpContextAccessor = httpContextAccessor;
        }

        public void setDeviceCookie(string deviceId)
        {
            var cookieOptions = new CookieOptions
            {
                HttpOnly = true, // XSS saldırında JS ile okunamaz
                Secure = true, // sadece Https üzerinden gider
                SameSite = SameSiteMode.Strict, // CSRF saldırılarına karşı korur
                Expires = DateTime.UtcNow.AddDays(7) // refresh token süresiyle uyumlu
            };
            _httpContextAccessor.HttpContext?.Response.Cookies.Append("X-Device-Id", deviceId, cookieOptions);
        }
    }
}
