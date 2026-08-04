using ECommerceProject.Application;
using ECommerceProject.Application.Exceptions;
using ECommerceProject.Application.Security.Hashing;
using ECommerceProject.Domain;
using ECommerceProject.Domain.Entities;
using ECommerceProject.Persistance.Contexts;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.DependencyInjection;
using System;
using System.Collections.Generic;
using System.Text;

namespace ECommerceProject.Persistance.Seed
{
    public static class DataSeedExtension
    {
        public static async Task SeedDatabaseAsync(this IServiceProvider serviceProvider)
        {
            // servicelere erişebilmek için scope oluşturulur.
            using var scope = serviceProvider.CreateScope();

            var context = scope.ServiceProvider.GetRequiredService<ECommerceDbContext>();

            try
            {
                await context.Database.MigrateAsync();

                if (!await context.Roles.AnyAsync())
                {
                    await context.Roles.AddRangeAsync(
                        new Domain.Entities.Role { Id = Guid.NewGuid(), Name = "Admin" },
                        new Domain.Entities.Role { Id = Guid.NewGuid(), Name = "Manager" },
                        new Domain.Entities.Role { Id = Guid.NewGuid(), Name = "Customer" }
                    );
                    await context.SaveChangesAsync();
                }
                const string adminEmail = "dev.ozkanyilmaz@gmail.com";
                const string managerEmail = "ozkanyilmaz.dev@gmail.com";
                const string customerEmail = "deneme123.dev@gmail.com";

                // Admin
                if (!await context.Users.AnyAsync(u => u.Email == adminEmail))
                {
                    HashingHelper.CreatePasswordHash("Ozkan123*", out byte[] passwordHash, out byte[] passwordSalt);

                    var adminUser = new User
                    {
                        Id = Guid.NewGuid(),
                        Email = adminEmail,
                        FirstName = "System",
                        LastName = "Admin",
                        PasswordHash = passwordHash,
                        PasswordSalt = passwordSalt,
                        Status = true
                    };
                    await context.Users.AddAsync(adminUser);
                    await context.SaveChangesAsync();

                    // Admin kullanıcısına Admin rolu atama
                    var adminRole = await context.Roles.FirstOrDefaultAsync(r => r.Name == "Admin");

                    if (adminRole != null)
                    {
                        var userRole = new UserRole
                        {
                            UserId = adminUser.Id,
                            RoleId = adminRole.Id,
                        };
                        await context.UserRoles.AddAsync(userRole);
                        await context.SaveChangesAsync();
                    }
                }
                // Manager
                if (!await context.Users.AnyAsync(u => u.Email == managerEmail))
                {
                    HashingHelper.CreatePasswordHash("Ozkan123*", out byte[] passwordHash, out byte[] passwordSalt);

                    var managerUser = new User
                    {
                        Id = Guid.NewGuid(),
                        Email = managerEmail,
                        FirstName = "System",
                        LastName = "Manager",
                        PasswordHash = passwordHash,
                        PasswordSalt = passwordSalt,
                        Status = true
                    };
                    await context.Users.AddAsync(managerUser);
                    await context.SaveChangesAsync();

                    // Manager kullanıcısına Manager rolu atama
                    var managerRole = await context.Roles.FirstOrDefaultAsync(r => r.Name == "Manager");

                    if (managerRole != null)
                    {
                        var userRole = new UserRole
                        {
                            UserId = managerUser.Id,
                            RoleId = managerRole.Id,
                        };
                        await context.UserRoles.AddAsync(userRole);
                        await context.SaveChangesAsync();
                    }
                }

                //Customer
                if (!await context.Users.AnyAsync(u => u.Email == customerEmail))
                {
                    HashingHelper.CreatePasswordHash("Ozkan123*", out byte[] passwordHash, out byte[] passwordSalt);

                    var customerUser = new User
                    {
                        Id = Guid.NewGuid(),
                        Email = customerEmail,
                        FirstName = "System",
                        LastName = "Customer",
                        PasswordHash = passwordHash,
                        PasswordSalt = passwordSalt,
                        Status = true
                    };
                    await context.Users.AddAsync(customerUser);
                    await context.SaveChangesAsync();

                    // Manager kullanıcısına customer rolu atama
                    var customerRole = await context.Roles.FirstOrDefaultAsync(r => r.Name == "Customer");

                    if (customerRole != null)
                    {
                        var userRole = new UserRole
                        {
                            UserId = customerUser.Id,
                            RoleId = customerRole.Id,
                        };
                        await context.UserRoles.AddAsync(userRole);
                        await context.SaveChangesAsync();
                    }
                }
            }
            catch (Exception ex)
            {
                throw;
            }


        }
    }
}
