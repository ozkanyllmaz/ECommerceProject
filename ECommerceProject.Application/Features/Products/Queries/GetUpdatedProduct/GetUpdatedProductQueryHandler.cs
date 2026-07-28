using AutoMapper;
using ECommerceProject.Application.DTOs.Common;
using ECommerceProject.Application.Features.Auth.Commands.RefreshTokens;
using ECommerceProject.Application.Repositories;
using MediatR;
using System;
using System.Collections.Generic;
using System.Text;

namespace ECommerceProject.Application.Features.Products.Queries.GetUpdatedProduct
{
    internal class GetUpdatedProductQueryHandler : IRequestHandler<GetUpdatedProductQueryRequest, CustomResponseDto<List<GetUpdatedProductQueryResponse>>>
    {
        private readonly IProductRepository _productRepository;
        private readonly IMapper _mapper;

        public GetUpdatedProductQueryHandler(IProductRepository productRepository, IMapper mapper)
        {
            _productRepository = productRepository;
            _mapper = mapper;
        }

        public async Task<CustomResponseDto<List<GetUpdatedProductQueryResponse>>> Handle(GetUpdatedProductQueryRequest request, CancellationToken cancellationToken)
        {
            var filteredProduct = await _productRepository.GetListAsync(p => p.UpdatedDate != null, false);

            var filteredProductDto = _mapper.Map<List<GetUpdatedProductQueryResponse>>(filteredProduct);

            return CustomResponseDto<List<GetUpdatedProductQueryResponse>>.Success(200, filteredProductDto, "Güncellenmiş ürünler getirildi");
        }
    }
}
