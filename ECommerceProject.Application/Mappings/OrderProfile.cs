using AutoMapper;
using ECommerceProject.Application.DTOs.Address;
using ECommerceProject.Application.DTOs.OrderItem;
using ECommerceProject.Application.Features.Orders.Queries.ListOrder;
using ECommerceProject.Application.Features.Orders.Queries.ListOrderByAdmin;
using ECommerceProject.Application.Features.Orders.Queries.ListOrderByManager;
using ECommerceProject.Application.Features.Orders.Queries.ListOrderDetail;
using ECommerceProject.Domain.Entities;
using ECommerceProject.Domain.Entities.ValueObjects;
using System;
using System.Collections.Generic;
using System.Text;

namespace ECommerceProject.Application.Mappings
{
    public class OrderProfile : Profile
    {
        public OrderProfile()
        {
            CreateMap<Order, ListOrderQueryResponse>()
                // isimleri farklı olan veya özel mantık gerektiren alanlar manuel eşlenir.
                .ForMember(dest => dest.OrderId, opt => opt.MapFrom(src => src.Id))
                .ForMember(dest => dest.TotalItemCount, opt => opt.MapFrom(src => src.OrderItems.Count));

            CreateMap<Address, AddressDto>();

            CreateMap<OrderItem, OrderItemDto>();

            CreateMap<Order, ListOrderDetailQueryResponse>()
                .ForMember(dest => dest.Status, opt => opt.MapFrom(src => src.Status.ToString()));

            CreateMap<Order, ListOrderByManagerQueryResponse>()
                .ForMember(dest => dest.OrderId, opt => opt.MapFrom(src => src.Id))
                .ForMember(dest => dest.TotalItemCount, opt => opt.MapFrom(src => src.OrderItems.Count));

            CreateMap<Order, ListOrderByAdminQueryResponse>()
                .ForMember(dest => dest.OrderId, opt => opt.MapFrom(src => src.Id))
                .ForMember(dest => dest.TotalItemCount, opt => opt.MapFrom(src => src.OrderItems.Count));
        }
    }
}
