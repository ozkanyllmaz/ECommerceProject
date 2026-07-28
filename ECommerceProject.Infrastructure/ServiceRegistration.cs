using ECommerceProject.Application.Abstractions;
using ECommerceProject.Infrastructure.Services;
using ECommerceProject.Infrastructure;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.AspNetCore.Authentication.JwtBearer;
using System;
using System.Collections.Generic;
using System.Text;
using Microsoft.IdentityModel.Tokens;
using ECommerceProject.Application.Security.Hashing;

namespace ECommerceProject.Infrastructure
{
    public static class ServiceRegistration
    {
        public static void AddInfrastructureServices(this IServiceCollection services, IConfiguration configuration)
        {
            services.AddScoped<ITokenService, TokenService>();
            services.AddScoped<ICurrentUserService, CurrentUserService>();
            services.AddScoped<IAuthCookieService, AuthCookieService>();

            var masterKey = configuration["Jwt:EncryptionMasterKey"];

            var encryptedSecurityKey = configuration["CustomTokenOption:SecurityKey"];
            var decryptedSecurityKey = EncryptionHelper.Decrypt(encryptedSecurityKey!, masterKey!);

            services.AddAuthentication(options =>
            {
                options.DefaultAuthenticateScheme = JwtBearerDefaults.AuthenticationScheme;
                options.DefaultChallengeScheme = JwtBearerDefaults.AuthenticationScheme;
            })
            .AddJwtBearer(options =>
            {
                options.TokenValidationParameters = new Microsoft.IdentityModel.Tokens.TokenValidationParameters
                {
                    ValidateIssuer = true,
                    ValidateAudience = true,
                    ValidateLifetime = true,
                    ValidateIssuerSigningKey = true,

                    ValidIssuer = configuration["CustomTokenOption:Issuer"],
                    ValidAudiences = configuration.GetSection("CustomTokenOption:Audience").Get<string[]>(),
                    IssuerSigningKey = new SymmetricSecurityKey(
                        Encoding.UTF8.GetBytes(decryptedSecurityKey))
                };
                options.Events = new JwtBearerEvents
                {
                    OnTokenValidated = context =>
                    {
                        // token içine claim olarak eklediğimiz DeviceId yi al
                        var tokenDeviceId = context.Principal?.FindFirst("DeviceId")?.Value;

                        // tarayıcının gönderdiği Httponly Cookie'yi oku
                        var cookieDeviceId = context.Request.Cookies["X-Device-Id"];

                        if(string.IsNullOrEmpty(tokenDeviceId) ||
                            string.IsNullOrEmpty(cookieDeviceId) ||
                            cookieDeviceId != tokenDeviceId)
                        {
                            context.Fail("Device Id uyuşmazlığı tespit edildi. Şüpheli işlem!!");
                        }
                        return Task.CompletedTask;
                    }
                };
            });
        }
    }
}
