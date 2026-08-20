using ECommerceProject.Application.DTOs.Dashboard;
using ECommerceProject.Application.Repositories;
using ECommerceProject.Domain.Entities;
using ECommerceProject.Persistance.Contexts;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Text;

namespace ECommerceProject.Persistance.Repositories
{
    public class CategoryRepository : GenericRepository<Category>, ICategoryRepository
    {
        public CategoryRepository(ECommerceDbContext context) : base(context)
        {
        }

        public async Task<List<ProductSalesResult>> GetProductSalesResultAsync()
        {
            return await _context.OrderItems
                .GroupBy(oi => oi.ProductName)
                .Select(group => new ProductSalesResult
                {
                    ProductName = group.Key,
                    Value = group.Sum(oi => oi.Quantity)
                })
                .ToListAsync();

        }
    }
}
