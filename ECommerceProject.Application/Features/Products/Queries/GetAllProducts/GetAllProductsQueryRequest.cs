using ECommerceProject.Application.DTOs.Common;
using MediatR;
using System;
using System.Collections.Generic;
using System.Text;

namespace ECommerceProject.Application.Features.Products.Queries.GetAllProducts
{
    // filtreleme ve paggination işlemleri
    public class GetAllProductsQueryRequest : IRequest<CustomResponseDto<List<GetAllProductsQueryResponse>>>
    {
    }
}
