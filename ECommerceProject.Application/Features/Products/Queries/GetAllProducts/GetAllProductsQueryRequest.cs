using ECommerceProject.Application.Abstractions;
using ECommerceProject.Application.DTOs.Common;
using MediatR;
using System;
using System.Collections.Generic;
using System.Text;

namespace ECommerceProject.Application.Features.Products.Queries.GetAllProducts
{
    // filtreleme ve paggination işlemleri
    public class GetAllProductsQueryRequest : IRequest<CustomResponseDto<PaginationResult<GetAllProductsQueryResponse>>>
    {
        public string? CategoryId { get; set; }

        public decimal? MinPrice { get; set; }

        public decimal? MaxPrice { get; set; }

        public string? SearchTerm { get; set; }
        public PaginationParameter paginationParameter { get; set; } = new PaginationParameter();
    }
}
