using ECommerceProject.Application.Abstractions;
using ECommerceProject.Application.DTOs.Address;
using ECommerceProject.Application.DTOs.Common;
using ECommerceProject.Domain.Entities;
using MediatR;
using System;
using System.Collections.Generic;
using System.Text;

namespace ECommerceProject.Application.Features.Orders.Commands.CreateOrder
{
    public class CreateOrderCommandRequest : IRequest<CustomResponseDto>, ISecuredRequest
    {
        public AddressDto ShippingAddress { get; set; } = null!;
        public AddressDto BillingAddress { get; set; } = null!;
        public string[] Roles => ["Admin", "Manager", "Customer"];
    }
}
