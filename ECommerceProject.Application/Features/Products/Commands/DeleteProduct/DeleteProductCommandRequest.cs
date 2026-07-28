using ECommerceProject.Application.Abstractions;
using ECommerceProject.Application.DTOs.Common;
using MediatR;
using System;
using System.Collections.Generic;
using System.Text;

namespace ECommerceProject.Application.Features.Products.Commands.DeleteProduct
{
    public class DeleteProductCommandRequest : IRequest<CustomResponseDto<DeleteProductCommandResponse>>, ISecuredRequest
    {
        public Guid Id { get; set; }
        public string[] Roles => ["Admin", "Manager"];
    }
}
