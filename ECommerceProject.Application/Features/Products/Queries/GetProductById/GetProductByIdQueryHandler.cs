using AutoMapper;
using ECommerceProject.Application.DTOs.Common;
using ECommerceProject.Application.Repositories;
using ECommerceProject.Domain.Entities;
using MediatR;
using System;
using System.Collections.Generic;
using System.Text;

namespace ECommerceProject.Application.Features.Products.Queries.GetProductById
{
    internal class GetProductByIdQueryHandler : IRequestHandler<GetProductByIdQueryRequest, CustomResponseDto<GetProductByIdQueryResponse>>
    {
        private readonly IProductRepository _productRepository;
        private readonly IMapper _mapper;

        public GetProductByIdQueryHandler(IProductRepository productRepository, IMapper mapper)
        {
            _productRepository = productRepository;
            _mapper = mapper;
        }

        public async Task<CustomResponseDto<GetProductByIdQueryResponse>> Handle(GetProductByIdQueryRequest request, CancellationToken cancellationToken)
        {
            var product = await _productRepository.GetByIdAsync(request.Id.ToString());
            if(product == null)
            {
                return CustomResponseDto<GetProductByIdQueryResponse>.Fail(404, "İstenilen id'de bir ürün bulunamadı");
            }
            var productResponse = _mapper.Map<GetProductByIdQueryResponse>(product);

            return CustomResponseDto<GetProductByIdQueryResponse>.Success(200, productResponse, "Ürün başarıyle getirildi");
        }
    }
}
