using ECommerceProject.Application.Repositories;
using ECommerceProject.Domain.Entities;
using ECommerceProject.Persistance.Contexts;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Text;

namespace ECommerceProject.Persistance.Repositories
{
    public class OrderRepository : GenericRepository<Order>, IOrderRepository
    {
        public OrderRepository(ECommerceDbContext context) : base(context)
        {
        }

        public async Task<int> GetTotalOrderCount()
        {
            return await _context.Orders
                .CountAsync();
        }

        public async Task<decimal> GetTotalRevenue()
        {
            return await _context.Orders
                .SumAsync(x => x.TotalAmount);
        }
    }
}
