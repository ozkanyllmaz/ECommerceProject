using ECommerceProject.Application.Abstractions;
using ECommerceProject.Application.DTOs.Common;
using MediatR;
using System;
using System.Collections.Generic;
using System.Text;

namespace ECommerceProject.Application.Features.Orders.Commands.ApproveOrderByAdmin
{
    public class ApproveOrderByAdminCommandRequest : IRequest<CustomResponseDto>, ISecuredRequest
    {
        public string OrderId { get; set; } = null!;
        public string[] Roles => ["Admin"];
    }
}
