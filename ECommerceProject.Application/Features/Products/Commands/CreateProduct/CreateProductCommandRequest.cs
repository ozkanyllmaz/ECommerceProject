using ECommerceProject.Application.DTOs.Common;
using MediatR;
using System;
using System.Collections.Generic;
using System.Text;

namespace ECommerceProject.Application.Features.Products.Commands.CreateProduct
{
    // MediatR'ın bu request'e karşılık hangi response'u döneceğini bilmesi için 
    // IRequest<T> arayüzünü uygulamamız gerekiyor.
    public class CreateProductCommandRequest : IRequest<CustomResponseDto<CreateProductCommandResponse>>
    {
        public string Name { get; set; } = null!;
        public string? Description { get; set; }
        public decimal Price { get; set; }
        public int Stock { get; set; }
        public string? ImageUrl { get; set; }
    }
}
