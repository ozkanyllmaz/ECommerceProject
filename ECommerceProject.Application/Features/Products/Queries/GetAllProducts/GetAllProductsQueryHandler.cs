using AutoMapper;
using AutoMapper.QueryableExtensions;
using ECommerceProject.Application.DTOs.Common;
using ECommerceProject.Application.Extensions;
using ECommerceProject.Application.Repositories;
using ECommerceProject.Domain.Entities;
using MediatR;
using System;
using System.Collections.Generic;
using System.Text;

namespace ECommerceProject.Application.Features.Products.Queries.GetAllProducts
{
    internal class GetAllProductsQueryHandler : IRequestHandler<GetAllProductsQueryRequest, CustomResponseDto<PaginationResult<GetAllProductsQueryResponse>>>
    {
        private readonly IProductRepository _productRepository;
        private readonly IMapper _mapper;

        public GetAllProductsQueryHandler(IProductRepository productRepository, IMapper mapper)
        {
            _productRepository = productRepository;
            _mapper = mapper;
        }

        public async Task<CustomResponseDto<PaginationResult<GetAllProductsQueryResponse>>> Handle(GetAllProductsQueryRequest request, CancellationToken cancellationToken)
        {
            var query = _productRepository.GetListAsQueryable(tracking: false);

            // AutoMapper'ın ProjectTo metodu ile SQL sorgusunu DTO'ya uygun hale getirir ve extension metot ile pagging yapar.
            var pagedResult = await query
                .ProjectTo<GetAllProductsQueryResponse>(_mapper.ConfigurationProvider)
                .ToPaginatedResultAsync(request.paginationParameter, cancellationToken);

            return CustomResponseDto<PaginationResult<GetAllProductsQueryResponse>>.Success(200, pagedResult, "Ürünler başarıyla getirildi");


            //var products = await _productRepository.GetListAsync(tracking: false);

            //var mappedProduct = _mapper.Map<GetAllProductsQueryResponse>(products);

            //return CustomResponseDto<PaginationResult<GetAllProductsQueryResponse>>.Success(200, mappedProduct, "Ürünler başarıyla getirildi");
        }
    }
}
