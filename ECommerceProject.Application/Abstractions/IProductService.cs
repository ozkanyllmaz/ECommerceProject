using ECommerceProject.Application.DTOs.Product;
using ECommerceProject.Domain.Entities;
using System;
using System.Collections.Generic;
using System.Text;

namespace ECommerceProject.Application.Abstractions
{
    public interface IProductService
    {
        Task<List<ProductListDto>> GetAllProductListAsync();
        Task<ProductDetailDto> GetProductById(Guid id);
        Task<bool> InsertAsync(ProductCreateDto model);
        Task<bool> DeleteProductAsync(Guid id);
        Task<bool> RestoreProductAsync(Guid id);
        Task<bool> UpdateProductAsync(Guid id, ProductUpdateDto model);
        
    }
}
