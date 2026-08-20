using ECommerceProject.Application.DTOs.Address;
using ECommerceProject.Application.DTOs.OrderItem;
using ECommerceProject.Domain.Entities.Enums;
using System;
using System.Collections.Generic;
using System.Text;

namespace ECommerceProject.Application.Features.Orders.Queries.ListOrderDetail
{
    public class ListOrderDetailQueryResponse
    {
        public Guid UserId { get; set; }
        public string? Id { get; set; }
        public string OrderNumber { get; set; } = null!;
        public DateTime OrderDate { get; set; }
        public OrderStatus Status { get; set; }
        public decimal TotalAmount { get; set; }

        public AddressDto ShippingAddress { get; set; } = null!;
        public AddressDto BillingAddress { get; set; } = null!;

        public List<OrderItemDto> OrderItems { get; set; } = new();
    }
}
