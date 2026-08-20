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
    public class OrderItemRepository : GenericRepository<OrderItem>, IOrderItemRepository
    {
        public OrderItemRepository(ECommerceDbContext context) : base(context)
        {
        }

        public async Task<Dictionary<DateTime, decimal>> GetWeeklyRevenueDtoAsync(DateTime startDate)
        {
            return await _context.Orders
                .Where(o => o.CreatedDate >= startDate)
                .GroupBy(o => o.CreatedDate.Date)
                .ToDictionaryAsync(
                    group => group.Key,
                    group => group.Sum(o => o.TotalAmount)
                );

        }
    }
}
