using System;
using System.Collections.Generic;
using System.Text;
using ECommerceProject.Application.Abstractions;
using Microsoft.Extensions.DependencyInjection;
using System.Reflection;
using ECommerceProject.Application.Behaviors;
using FluentValidation;


namespace ECommerceProject.Application
{
    public static class ServiceRegistration
    {
        public static void AddApplicationServices(this IServiceCollection services)
        {
            var assembly = Assembly.GetExecutingAssembly();

            // MediatR kaydı
            // Application katmanındaki tüm Command ve Query Handler'ları otomatik bulup kaydeder.
            services.AddMediatR(cfg =>
            {
                cfg.RegisterServicesFromAssembly(assembly);
                cfg.AddOpenBehavior(typeof(ValidationBehavior<,>));
            });

            // FluentValidation kaydı
            // Bu assembly Application içindeki AbstractValidator'dan miras alan tüm sınıfları otomatik bulur ve
            // sisteme dahil eder.
            services.AddValidatorsFromAssembly(assembly);
        }
    }
}
