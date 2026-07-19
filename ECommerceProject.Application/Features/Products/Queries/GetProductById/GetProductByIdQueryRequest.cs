using ECommerceProject.Application.DTOs.Common;
using MediatR;
using System;
using System.Collections.Generic;
using System.Text;

namespace ECommerceProject.Application.Features.Products.Queries.GetProductById
{
    public class GetProductByIdQueryRequest : IRequest<CustomResponseDto<GetProductByIdQueryResponse>>
    {
        public Guid Id { get; set; }
    }
}
