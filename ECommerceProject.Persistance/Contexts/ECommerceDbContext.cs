using ECommerceProject.Application.Abstractions;
using ECommerceProject.Domain.Entities;
using ECommerceProject.Domain.Entities.Common;
using ECommerceProject.Domain.Entities.LogEntities;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.ChangeTracking;
using Microsoft.Extensions.Logging;
using System;
using System.Collections.Generic;
using System.Text;

namespace ECommerceProject.Persistance.Contexts
{
    public class ECommerceDbContext : DbContext
    {
        private readonly ICurrentUserService _currentUserService;
        private readonly ILogger<ECommerceDbContext> _logger;
        public ECommerceDbContext(DbContextOptions<ECommerceDbContext> options, ICurrentUserService currentUserService, ILogger<ECommerceDbContext> logger) : base(options)
        {
            _currentUserService = currentUserService;
            _logger = logger;
        }

        public DbSet<Product> Products { get; set; }
        public DbSet<User> Users { get; set; }
        public DbSet<Role> Roles { get; set; }
        public DbSet<UserRole> UserRoles { get; set; }
        public DbSet<RefreshToken> RefreshTokens { get; set; }
        public DbSet<Category> Categories { get; set; }
        public DbSet<ShoppingCart> ShoppingCarts { get; set; }
        public DbSet<ShoppingCartItem> ShoppingCartItems { get; set; }
        public DbSet<Order> Orders { get; set; }
        public DbSet<OrderItem> OrderItems {  get; set; }
        public DbSet<ExceptionLog> ExceptionLogs { get; set; }
        public DbSet<AuditLog> AuditLogs { get; set; }
        public DbSet<RequestLog> RequestLogs { get; set; }


        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            base.OnModelCreating(modelBuilder);

            //Entity çağırıldığı zaman silinmişleri otomatik gizle
            modelBuilder.Entity<ShoppingCart>().HasQueryFilter(sc => !sc.IsDeleted);
            modelBuilder.Entity<ShoppingCartItem>().HasQueryFilter(sc => !sc.IsDeleted);

            modelBuilder.Entity<Order>().HasQueryFilter(o => !o.IsDeleted);
            modelBuilder.Entity<OrderItem>().HasQueryFilter(o => !o.IsDeleted);

            modelBuilder.Entity<Product>().HasQueryFilter(p => !p.IsDeleted);

            modelBuilder.Entity<Category>().HasQueryFilter(c => !c.IsDeleted);

            modelBuilder.Entity<RefreshToken>().HasQueryFilter(r => !r.IsDeleted);

            modelBuilder.Entity<User>().HasQueryFilter(u => !u.IsDeleted);
            modelBuilder.Entity<UserRole>().HasQueryFilter(ur => !ur.IsDeleted);

            modelBuilder.Entity<Product>().Property(p => p.Price).HasColumnType("decimal(18,2)");
            modelBuilder.Entity<OrderItem>().Property(oi => oi.UnitPrice).HasColumnType("decimal(18,2)");
            modelBuilder.Entity<OrderItem>().Property(oi => oi.TotalPrice).HasColumnType("decimal(18,2)");

            modelBuilder.Entity<ExceptionLog>().ToTable("ExceptionLogs");
            modelBuilder.Entity<ExceptionLog>().HasKey(e => e.Id);

            modelBuilder.Entity<AuditLog>().ToTable("AuditLogs");
            modelBuilder.Entity<AuditLog>().HasKey(a => a.Id);

            modelBuilder.Entity<RequestLog>().ToTable("RequestLogs");
            modelBuilder.Entity<RequestLog>().HasKey(r => r.Id);

            modelBuilder.ApplyConfigurationsFromAssembly(typeof(ECommerceDbContext).Assembly);

        }

        //Ekleme ve güncelleme işlemlerinde CreatedDate ve UpdatedDate otomatik olarak ayarlanacak. 
        //Araya girme mantığı (Interceptor)
        public override Task<int> SaveChangesAsync(CancellationToken cancellationToken = default) 
        {
            BaseEntityOperations();
            GenerateAuditLogs();

            return base.SaveChangesAsync(cancellationToken);
        }

        private void BaseEntityOperations()
        {
            var datas = ChangeTracker.Entries<BaseEntity>();

            foreach (var data in datas)
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
        }

        private void GenerateAuditLogs()
        {
            var userId = _currentUserService.UserId;

            var auditEntries = ChangeTracker.Entries()
                .Where(x => x.State == EntityState.Added || x.State == EntityState.Modified || x.State == EntityState.Deleted)
                .ToList();

            foreach(var entry in auditEntries)
            {
                var tableName = entry.Metadata.GetTableName();
                var actionType = entry.State.ToString();

                if(entry.Entity is BaseEntity baseEntity && entry.State == EntityState.Modified && baseEntity.IsDeleted)
                {
                    actionType = "SoftDeleted";
                }

                var oldValues = new Dictionary<string, object>();
                var newValues = new Dictionary<string, object>();
                var changedColumns = new List<string>();

                foreach(var property in entry.Properties)
                {
                    var propertyName = property.Metadata.Name;

                    if (propertyName.Contains("Password", StringComparison.OrdinalIgnoreCase) ||
                        propertyName.Contains("Token", StringComparison.OrdinalIgnoreCase))
                        continue;

                    switch (entry.State)
                    {
                        case EntityState.Added:
                            newValues[propertyName] = property.CurrentValue!;
                            break;

                        case EntityState.Deleted:
                            oldValues[propertyName] = property.OriginalValue!;
                            break;

                        case EntityState.Modified:
                            if (property.IsModified)
                            {
                                var original = property.OriginalValue;
                                var current = property.CurrentValue;

                                if(!Equals(original, current))
                                {
                                    oldValues[propertyName] = original;
                                    newValues[propertyName] = current;
                                    changedColumns.Add(propertyName);
                                }
                            }
                            break;
                    }      
                }

                if(oldValues.Count > 0 || newValues.Count > 0)
                {
                    var auditLog = new
                    {
                        UserId = userId,
                        TableName = tableName,
                        Action = actionType,
                        Timestamp = DateTime.UtcNow,
                        OldValues = oldValues,
                        NewValues = newValues,
                        ChangedColumns = changedColumns
                    };

                    _logger.LogInformation("AuditLog: {@AuditLog}", auditLog);
                }
            }

        }

    }
}
