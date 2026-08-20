using AutoMapper;
using AutoMapper.QueryableExtensions;
using ECommerceProject.Application.DTOs.Common;
using ECommerceProject.Application.Extensions;
using ECommerceProject.Application.Repositories;
using MediatR;
using System;
using System.Collections.Generic;
using System.Text;

namespace ECommerceProject.Application.Features.Products.Queries.GetProductsByCategory
{
    internal class GetProductsByCategoryQueryHandler : IRequestHandler<GetProductsByCategoryQueryRequest, CustomResponseDto<PaginationResult<GetProductsByCategoryQueryResponse>>>
    {
        private readonly IProductRepository _productRepository;
        private readonly IMapper _mapper;

        public GetProductsByCategoryQueryHandler(IProductRepository productRepository, IMapper mapper)
        {
            _productRepository = productRepository;
            _mapper = mapper;
        }

        public async Task<CustomResponseDto<PaginationResult<GetProductsByCategoryQueryResponse>>> Handle(GetProductsByCategoryQueryRequest request, CancellationToken cancellationToken)
        {
            var products = await _productRepository.GetListWithFilterAsQueryable(x => x.CategoryId == request.CategoryId)
                .ProjectTo<GetProductsByCategoryQueryResponse>(_mapper.ConfigurationProvider)
                .ToPaginatedResultAsync(request.PaginationParameter, cancellationToken);

            //var products = await _productRepository.ToListAsync(query, cancellationToken);

            return CustomResponseDto<PaginationResult<GetProductsByCategoryQueryResponse>>.Success(200, products, "Kategoriye ait ürünler başarıyla çekildi");

        }
    }
}
