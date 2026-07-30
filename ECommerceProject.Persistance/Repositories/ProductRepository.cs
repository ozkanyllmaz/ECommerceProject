using ECommerceProject.Application.Repositories;
using ECommerceProject.Domain.Entities;
using ECommerceProject.Persistance.Contexts;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Text;

namespace ECommerceProject.Persistance.Repositories
{
    public class ProductRepository : GenericRepository<Product>, IProductRepository
    {
        public ProductRepository(ECommerceDbContext context) : base(context)
        {
        }

        public IQueryable<Product> GetProductByDeletedCategoryAsync(Guid categoryId)
        {
            return _context.Products
                .IgnoreQueryFilters()
                .Include(x => x.Category)
                .Where(x => x.CategoryId == categoryId)
                .Where(x => x.Category != null && x.Category.IsDeleted == true)
                .AsNoTracking();
        }
    }
}
