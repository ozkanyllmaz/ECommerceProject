using ECommerceProject.Application.Abstractions;
using ECommerceProject.Application.DTOs.Common;
using MediatR;
using System;
using System.Collections.Generic;
using System.Text;

namespace ECommerceProject.Application.Features.Products.Queries.GetUpdatedProduct
{
    public class GetUpdatedProductQueryRequest : IRequest<CustomResponseDto<PaginationResult<GetUpdatedProductQueryResponse>>>, ISecuredRequest
    {
        public string[] Roles => ["Admin", "Manager", "Customer"];
        public PaginationParameter paginationParameter { get; set; } = new PaginationParameter();
    }
}
