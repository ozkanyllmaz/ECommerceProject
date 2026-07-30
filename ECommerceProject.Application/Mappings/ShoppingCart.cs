using AutoMapper;
using ECommerceProject.Application.Features.ShoppingCarts.Queries.ListItemInShoppingCart;
using ECommerceProject.Domain.Entities;
using System;
using System.Collections.Generic;
using System.Text;

namespace ECommerceProject.Application.Mappings
{
    public class ShoppingCart : Profile
    {
        public ShoppingCart()
        {
            CreateMap<ShoppingCartItem, ListItemInShoppingCartQueryResponse>()
                // Sepet satırının kimlik eşleştirmesi
                .ForMember(dest => dest.CarItemId, opt => opt.MapFrom(src => src.Id))
                // product detaylarının eşleştirilmesi
                .ForMember(dest => dest.Name, opt => opt.MapFrom(src => src.Product.Name))
                .ForMember(dest => dest.Stock, opt => opt.MapFrom(src => src.Product.Stock))
                .ForMember(dest => dest.Price, opt => opt.MapFrom(src => src.Product.Price))
                .ForMember(dest => dest.ImageUrl, opt => opt.MapFrom(src => src.Product.ImageUrl));

        }
    }
}
