using ECommerceProject.Persistance.Contexts;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Text;
using ECommerceProject.Application.Repositories;
using ECommerceProject.Persistance.Repositories;

namespace ECommerceProject.Persistance
{
    public static class ServiceRegistration
    {
        public static void AddPersistenceServices(this IServiceCollection services, IConfiguration configuration)
        {
            //Db bağlantımızı IoC container'a ekliyoruz.
            services.AddDbContext<ECommerceDbContext>(options => options.UseSqlServer(
                configuration.GetConnectionString("MsSql")   
             ));

            //Repository bağımlılıklarımızı ekliyoruz.
            services.AddScoped<IProductRepository, ProductRepository>();
        }
    }
}
