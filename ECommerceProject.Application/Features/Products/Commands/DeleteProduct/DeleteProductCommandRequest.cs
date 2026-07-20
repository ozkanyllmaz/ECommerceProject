using ECommerceProject.Application.DTOs.Common;
using MediatR;
using System;
using System.Collections.Generic;
using System.Text;

namespace ECommerceProject.Application.Features.Products.Commands.DeleteProduct
{
    public class DeleteProductCommandRequest : IRequest<CustomResponseDto<DeleteProductCommandResponse>>
    {
        public Guid Id { get; set; }
    }
}
