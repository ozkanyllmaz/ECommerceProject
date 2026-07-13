using System;
using System.Collections.Generic;
using System.Text;
using ECommerceProject.Application.Abstractions;
using ECommerceProject.Application.Services;
using Microsoft.Extensions.DependencyInjection;

namespace ECommerceProject.Application
{
    public static class ServiceRegistration
    {
        public static void AddApplicationServices(this IServiceCollection services)
        {
            services.AddScoped<IProductService, ProductService>();
        }
    }
}
