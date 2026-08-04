using ECommerceProject.Application.Abstractions;
using ECommerceProject.Application.DTOs.Common;
using MediatR;
using System;
using System.Collections.Generic;
using System.Text;

namespace ECommerceProject.Application.Features.Orders.Commands.DeleteOrder
{
    public class DeleteOrderCommandRequest : IRequest<CustomResponseDto>, ISecuredRequest
    {
        public string OrderId { get; set; } = null!;
        public string? CancellationReason { get; set; }
        public string[] Roles => ["Admin", "Manager", "Customer"];
    }
}
