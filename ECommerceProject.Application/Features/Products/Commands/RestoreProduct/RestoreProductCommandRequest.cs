using ECommerceProject.Application.Abstractions;
using ECommerceProject.Application.DTOs.Common;
using MediatR;
using System;
using System.Collections.Generic;
using System.Text;

namespace ECommerceProject.Application.Features.Products.Commands.RestoreProduct
{
    public class RestoreProductCommandRequest : IRequest<CustomResponseDto<RestoreProductCommandResponse>>, ISecuredRequest
    {
        public Guid Id { get; set; }
        public string[] Roles => ["Admin", "Manager"];
    }
}
