using AutoMapper;
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
        private readonly IMapper _mapper;

        public ProductService(IProductRepository productRepository, IMapper mapper)
        {
            _productRepository = productRepository;
            _mapper = mapper;
        }

        public async Task<bool> DeleteProductAsync(Guid id)
        {
            var product = await _productRepository.GetByIdAsync(id.ToString());
            if(product == null)
            {
                return false;
            }
            _productRepository.Remove(product);
            await _productRepository.SaveAsync();
            return true;
        }

        public async Task<List<ProductListDto>> GetAllProductListAsync()
        {
            //listeleme yapacağımız için sadece, tracking false yapıyoruz performans için.
            var products = await _productRepository.GetAll(tracking: false);

            // entity -> Dto dönüştürür.
            var productDtos = _mapper.Map<List<ProductListDto>>(products);

            return productDtos;
        }

        public async Task<ProductDetailDto> GetProductById(Guid id)
        {
            var product = await _productRepository.GetByIdAsync(id.ToString());
            if( product == null)
            {
                return null;
            }
            var productDto = _mapper.Map<ProductDetailDto>(product);

            return productDto;
        }

        public async Task<bool> InsertAsync(ProductCreateDto model)
        {
            var product = _mapper.Map<Product>(model);

            //Product product = new Product
            //{
            //    Name = model.Name,
            //    Description = model.Description,
            //    Price = model.Price,
            //    Stock = model.Stock,
            //    ImageUrl = model.ImageUrl
            //};

            await _productRepository.AddAsync(product);
            await _productRepository.SaveAsync();
            //daha sonra global try-catch yapısı kurucam.SaveAsync da hata fırlatırsa globalde yakalıcaz.
            return true;
        }

        public async Task<bool> RestoreProductAsync(Guid id)
        {
            var deletedProduct = await _productRepository.GetByIdAsync(id.ToString(), ignoreQueryFilters: true);
            if(deletedProduct == null)
            {
                return false;
            }
            _productRepository.Restore(deletedProduct);
            await _productRepository.SaveAsync();
            return true;
        }

        public async Task<bool> UpdateProductAsync(Guid id, ProductUpdateDto model)
        {
            var product = await _productRepository.GetByIdAsync(id.ToString());
            if(product == null)
            {
                return false;
            }

            _mapper.Map(model, product);
            await _productRepository.SaveAsync();

            return true;
        }
    }
}
