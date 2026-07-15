using AutoMapper;
using ECommerceProject.Application.DTOs.Product;
using ECommerceProject.Domain.Entities;
using System;
using System.Collections.Generic;
using System.Text;

namespace ECommerceProject.Application.Mappings
{
    public class ProductProfile : Profile
    {
        public ProductProfile()
        {
            // CreateMap<Kaynak, Hedef>();

            // Veri okuma, listeleme
            // Akış Yönü: Db -> Backend -> Frontend 
            CreateMap<Product, ProductListDto>();
            CreateMap<Product, ProductDetailDto>();

            // Veri ekleme, güncelle
            // Akış Yönü: Frontend -> Backend -> Db
            CreateMap<ProductUpdateDto, Product>();
            CreateMap<ProductCreateDto, Product>();
        }
    }
}
