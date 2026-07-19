using AutoMapper;
using ECommerceProject.Application.DTOs.Common;
using ECommerceProject.Application.Repositories;
using ECommerceProject.Domain.Entities;
using MediatR;
using System;
using System.Collections.Generic;
using System.Text;

namespace ECommerceProject.Application.Features.Products.Queries.GetAllProducts
{
    internal class GetAllProductsQueryHandler : IRequestHandler<GetAllProductsQueryRequest, CustomResponseDto<List<GetAllProductsQueryResponse>>>
    {
        private readonly IProductRepository _productRepository;
        private readonly IMapper _mapper;

        public GetAllProductsQueryHandler(IProductRepository productRepository, IMapper mapper)
        {
            _productRepository = productRepository;
            _mapper = mapper;
        }

        public async Task<CustomResponseDto<List<GetAllProductsQueryResponse>>> Handle(GetAllProductsQueryRequest request, CancellationToken cancellationToken)
        {
            var products = await _productRepository.GetAll(tracking: false);

            var mappedProduct = _mapper.Map<List<GetAllProductsQueryResponse>>(products);

            return CustomResponseDto<List<GetAllProductsQueryResponse>>.Success(200,mappedProduct , "Ürünler başarıyla getirildi");
        }
    }
}
