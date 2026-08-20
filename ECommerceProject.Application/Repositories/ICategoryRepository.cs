using ECommerceProject.Application.DTOs.Dashboard;
using ECommerceProject.Domain.Entities;
using System;
using System.Collections.Generic;
using System.Text;

namespace ECommerceProject.Application.Repositories
{
    public interface ICategoryRepository : IGenericRepository<Category>
    {
        Task<List<ProductSalesResult>> GetProductSalesResultAsync();
    }
}
