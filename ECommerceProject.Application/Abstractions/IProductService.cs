using ECommerceProject.Application.DTOs.Product;
using System;
using System.Collections.Generic;
using System.Text;

namespace ECommerceProject.Application.Abstractions
{
    public interface IProductService
    {
        Task<List<ProductListDto>> GetAllProductListAsync();
        Task<bool> InsertAsync(ProductCreateDto model);
    }
}
