using System;
using System.Collections.Generic;
using System.Text;
using ECommerceProject.Application.Abstractions;
using Microsoft.Extensions.DependencyInjection;
using System.Reflection;


namespace ECommerceProject.Application
{
    public static class ServiceRegistration
    {
        public static void AddApplicationServices(this IServiceCollection services)
        {
            // MediatR kaydı
            // Application katmanındaki tüm Command ve Query Handler'ları otomatik bulup kaydeder.
            services.AddMediatR(cfg => cfg.RegisterServicesFromAssembly(Assembly.GetExecutingAssembly()));
        }
    }
}
