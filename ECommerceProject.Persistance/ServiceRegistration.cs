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
        public static void AddPersistenceServices(this IServiceCollection services, string decryptedConnectionString)
        {
            //Db bağlantımızı IoC container'a ekliyoruz.
            services.AddDbContext<ECommerceDbContext>(options => options.UseSqlServer(decryptedConnectionString));

            //Repository bağımlılıklarımızı ekliyoruz.
            services.AddScoped<IProductRepository, ProductRepository>();

            services.AddScoped<IUserRepository, UserRepository>();
            services.AddScoped<IRoleRepository, RoleRepository>();
            services.AddScoped<IRefreshTokenRepository, RefreshTokenRepository>();
        }
    }
}
