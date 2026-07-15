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

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            base.OnModelCreating(modelBuilder);

            //Entity çağırıldığı zaman silinmişleri otomatik gizle
            modelBuilder.Entity<Product>().HasQueryFilter(p => !p.IsDeleted);
        }

        //Ekleme ve güncelleme işlemlerinde CreatedDate ve UpdatedDate otomatik olarak ayarlanacak. 
        //Araya girme mantığı (Interceptor)
        public override Task<int> SaveChangesAsync(CancellationToken cancellationToken = default) 
        {
            var datas = ChangeTracker.Entries<BaseEntity>();

            foreach(var data in datas)
            {
                switch (data.State)
                {
                    case EntityState.Added:
                        data.Entity.CreatedDate = DateTime.UtcNow;
                        break;

                    case EntityState.Modified:
                        data.Entity.UpdatedDate = DateTime.UtcNow;
                        break;

                        //soft delete 
                    case EntityState.Deleted:
                        data.State = EntityState.Modified;
                        data.Entity.IsDeleted = true;
                        data.Entity.DeletedDate = DateTime.UtcNow;
                        break;
                }

            }
            return base.SaveChangesAsync(cancellationToken);
        }

    }
}
