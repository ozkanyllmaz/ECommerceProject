using ECommerceProject.Application.Abstractions;
using ECommerceProject.Application.DTOs.Product;
using ECommerceProject.Application.Repositories;
using ECommerceProject.Domain.Entities;
using System;
using System.Collections.Generic;
using System.Text;

namespace ECommerceProject.Application.Services
{
    public class ProductService : IProductService
    {
        private readonly IProductRepository _productRepository;

        public ProductService(IProductRepository productRepository)
        {
            _productRepository = productRepository;
        }

        public async Task<List<ProductListDto>> GetAllProductListAsync()
        {
            //listeleme yapacağımız için sadece, tracking false yapıyoruz performans için.
            var products = await _productRepository.GetAll(tracking: false);

            //entity modelini manuel olarak dto modeline dönüştürüyoruz(mapping)
            return products.Select(p => new ProductListDto
            {
                Id = p.Id,
                Name = p.Name,
                Price = p.Price,
                Stock = p.Stock,
                ImageUrl = p.ImageUrl
            }).ToList();
        }

        public async Task<bool> InsertAsync(ProductCreateDto model)
        {
            Product product = new Product
            {
                Name = model.Name,
                Description = model.Description,
                Price = model.Price,
                Stock = model.Stock,
                ImageUrl = model.ImageUrl
            };

            await _productRepository.AddAsync(product);
            await _productRepository.SaveAsync();
            //daha sonra global try-catch yapısı kurucam.SaveAsync da hata fırlatırsa globalde yakalıcaz.
            return true;
        }
    }
}
