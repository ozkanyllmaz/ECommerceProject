using ECommerceProject.Domain.Entities;
using System;
using System.Collections.Generic;
using System.Text;

namespace ECommerceProject.Application.Repositories
{
    public interface IProductRepository:IGenericRepository<Product>
    {
        IQueryable<Product> GetProductByDeletedCategoryAsync(Guid categoryId);
        Task<List<Product>> GetProductsByIdsAsync(IEnumerable<Guid> productIds);
    }
}
