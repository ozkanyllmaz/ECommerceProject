using AutoMapper;
using AutoMapper.QueryableExtensions;
using ECommerceProject.Application.DTOs.Common;
using ECommerceProject.Application.Extensions;
using ECommerceProject.Application.Features.Auth.Commands.RefreshTokens;
using ECommerceProject.Application.Repositories;
using MediatR;
using System;
using System.Collections.Generic;
using System.Text;

namespace ECommerceProject.Application.Features.Products.Queries.GetUpdatedProduct
{
    internal class GetUpdatedProductQueryHandler : IRequestHandler<GetUpdatedProductQueryRequest, CustomResponseDto<PaginationResult<GetUpdatedProductQueryResponse>>>
    {
        private readonly IProductRepository _productRepository;
        private readonly IMapper _mapper;

        public GetUpdatedProductQueryHandler(IProductRepository productRepository, IMapper mapper)
        {
            _productRepository = productRepository;
            _mapper = mapper;
        }

        public async Task<CustomResponseDto<PaginationResult<GetUpdatedProductQueryResponse>>> Handle(GetUpdatedProductQueryRequest request, CancellationToken cancellationToken)
        {
            var query = _productRepository.GetListWithFilterAsQueryable(p => p.UpdatedDate != null, false);

            var pagedResult = await query
                .ProjectTo<GetUpdatedProductQueryResponse>(_mapper.ConfigurationProvider)
                .ToPaginatedResultAsync(request.paginationParameter, cancellationToken);
                
            return CustomResponseDto<PaginationResult<GetUpdatedProductQueryResponse>>.Success(200, pagedResult, "Güncellenmiş ürünler getirildi");
        }
    }
}
