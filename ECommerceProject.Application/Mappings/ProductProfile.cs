using AutoMapper;
using ECommerceProject.Application.Features.Products.Commands.CreateProduct;
using ECommerceProject.Application.Features.Products.Commands.UpdateProduct;
using ECommerceProject.Application.Features.Products.Queries.GetAllProducts;
using ECommerceProject.Application.Features.Products.Queries.GetProductById;
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
            CreateMap<Product, GetAllProductsQueryResponse>();
            CreateMap<Product, GetProductByIdQueryResponse>();

            // Veri ekleme, güncelle
            // Akış Yönü: Frontend -> Backend -> Db
            CreateMap<UpdateProductCommandRequest, Product>();
            CreateMap<CreateProductCommandRequest, Product>();
        }
    }
}
