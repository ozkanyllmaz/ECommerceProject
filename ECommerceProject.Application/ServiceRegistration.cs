using System;
using System.Collections.Generic;
using System.Text;
using ECommerceProject.Application.Abstractions;
using ECommerceProject.Application.Services;
using Microsoft.Extensions.DependencyInjection;
using System.Reflection;


namespace ECommerceProject.Application
{
    public static class ServiceRegistration
    {
        public static void AddApplicationServices(this IServiceCollection services)
        {
            services.AddScoped<IProductService, ProductService>();
            services.AddScoped<IAuthService, AuthService>();

            // MediatR kaydı
            // Application katmanındaki tüm Command ve Query Handler'ları otomatik bulup kaydeder.
            services.AddMediatR(cfg => cfg.RegisterServicesFromAssembly(Assembly.GetExecutingAssembly()));
        }
    }
}
