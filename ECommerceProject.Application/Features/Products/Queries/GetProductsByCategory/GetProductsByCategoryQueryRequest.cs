using ECommerceProject.Application.DTOs.Common;
using MediatR;
using System;
using System.Collections.Generic;
using System.Text;

namespace ECommerceProject.Application.Features.Products.Queries.GetProductsByCategory
{
    public class GetProductsByCategoryQueryRequest : IRequest<CustomResponseDto<PaginationResult<GetProductsByCategoryQueryResponse>>>
    {
        public Guid CategoryId { get; set; }
        public PaginationParameter PaginationParameter { get; set; } = null!;
    }
}
