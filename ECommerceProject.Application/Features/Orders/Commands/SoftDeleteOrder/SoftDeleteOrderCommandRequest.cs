using ECommerceProject.Application.Abstractions;
using ECommerceProject.Application.DTOs.Common;
using MediatR;
using System;
using System.Collections.Generic;
using System.Text;

namespace ECommerceProject.Application.Features.Orders.Commands.SoftDeleteOrder
{
    public class SoftDeleteOrderCommandRequest : IRequest<CustomResponseDto>, ISecuredRequest
    {
        public string OrderId { get; set; } = null!;
        public string[] Roles => ["Admin", "Manager", "Customer"];
    }
}
