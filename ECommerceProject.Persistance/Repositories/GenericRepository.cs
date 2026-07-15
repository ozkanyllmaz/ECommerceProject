using ECommerceProject.Application.Repositories;
using ECommerceProject.Domain.Entities.Common;
using ECommerceProject.Persistance.Contexts;
using Microsoft.EntityFrameworkCore.ChangeTracking;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq.Expressions;
using System.Text;

namespace ECommerceProject.Persistance.Repositories
{
    public class GenericRepository<T> : IGenericRepository<T> where T : BaseEntity
    {
        private readonly ECommerceDbContext _context;

        public GenericRepository(ECommerceDbContext context)
        {
            _context = context;
        }

        public DbSet<T> Table => _context.Set<T>();

        //Metotlarda .SaveChangesAsync() yok. Unit of Work gereği ekleme ile kaydetme birbirinden ayrılır.
        //Tüm işlemleri Ef Core hafızasında tutar ve await _productRepository.SaveAsync() ile hepsini kaydeder. Eğer hata olursa RollBack ile tüm işlemleri geri alır(Transaction).

        public async Task<bool> AddAsync(T model)
        {
            EntityEntry<T> entityEntry = await _context.Set<T>().AddAsync(model);
            return entityEntry.State == EntityState.Added;
        }

        public async Task<bool> AddRangeAsync(List<T> datas)
        {
            await _context.Set<T>().AddRangeAsync(datas);
            return true;
        }

        public async Task<List<T>> GetAll(bool tracking = true)
        {
            var query = _context.Set<T>().AsQueryable();
            if (!tracking)
            {
                query = query.AsNoTracking();
            }
            return await query.ToListAsync();
        }

        public async Task<T> GetByIdAsync(string id, bool tracking = true, bool ignoreQueryFilters = false)
        {
            var query = Table.AsQueryable();
            if (!tracking)
                query = query.AsNoTracking();

            if (ignoreQueryFilters)
            {
                query = query.IgnoreQueryFilters();
            }

            return await query.FirstOrDefaultAsync(x => x.Id == Guid.Parse(id));
        }

        public async Task<List<T>> GetWhere(Expression<Func<T, bool>> method, bool tracking = true)
        {
            var query = Table.Where(method);
            if (!tracking)
                query = query.AsNoTracking();

            return await query.ToListAsync();
        }

        public bool Remove(T model)
        {
            EntityEntry<T> entityEntry = Table.Remove(model);
            return entityEntry.State == EntityState.Deleted;

        }

        public bool Restore(T model)
        {
            model.IsDeleted = false;
            model.DeletedDate = null;

            EntityEntry<T> entityEntry = Table.Update(model);
            return entityEntry.State == EntityState.Modified;
        }

        public async Task<int> SaveAsync()
            => await _context.SaveChangesAsync();


        public bool Update(T model)
        {
            EntityEntry<T> entityEntry = Table.Update(model);
            return entityEntry.State == EntityState.Modified;
        }
    }
}
