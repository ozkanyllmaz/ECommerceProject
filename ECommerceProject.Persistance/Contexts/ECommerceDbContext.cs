using ECommerceProject.Domain.Entities;
using ECommerceProject.Domain.Entities.Common;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.ChangeTracking;
using System;
using System.Collections.Generic;
using System.Text;

namespace ECommerceProject.Persistance.Contexts
{
    public class ECommerceDbContext : DbContext
    {
        public ECommerceDbContext(DbContextOptions<ECommerceDbContext> options) : base(options)
        {
        }

        public DbSet<Product> Products { get; set; }

        //Ekleme ve güncelleme işlemlerinde CreatedDate ve UpdatedDate otomatik olarak ayarlanacak.
        public override Task<int> SaveChangesAsync(CancellationToken cancellationToken = default) 
        {
            var datas = ChangeTracker.Entries<BaseEntity>();

            foreach(var data in datas)
            {
                _ = data.State switch
                {
                    EntityState.Added => data.Entity.CreatedDate = DateTime.UtcNow,
                    EntityState.Modified => data.Entity.UpdatedDate = DateTime.UtcNow,
                    _ => DateTime.UtcNow
                };
            }
            return base.SaveChangesAsync(cancellationToken);
        }

    }
}
