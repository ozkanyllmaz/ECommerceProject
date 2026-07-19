using AutoMapper;
using ECommerceProject.Application.DTOs.Common;
using ECommerceProject.Application.Repositories;
using ECommerceProject.Domain.Entities;
using MediatR;
using System;
using System.Collections.Generic;
using System.Text;

namespace ECommerceProject.Application.Features.Products.Commands.CreateProduct
{
    internal class CreateProductCommandHandler : IRequestHandler<CreateProductCommandRequest, CustomResponseDto<CreateProductCommandResponse>>
    {
        private readonly IProductRepository _productRepository;
        private readonly IMapper _mapper;

        public CreateProductCommandHandler(IProductRepository productRepository, IMapper mapper)
        {
            _productRepository = productRepository;
            _mapper = mapper;
        }

        public async Task<CustomResponseDto<CreateProductCommandResponse>> Handle(CreateProductCommandRequest request, CancellationToken cancellationToken)
        {
            var product = _mapper.Map<Product>(request);

            await _productRepository.AddAsync(product);
            int affectedRows = await _productRepository.SaveAsync();

            if(affectedRows > 0)
            {
                var responseData = new CreateProductCommandResponse { IsSuccess = true };
                return CustomResponseDto<CreateProductCommandResponse>.Success(201, "Ürün başarıyla eklendi");
            }

            return CustomResponseDto<CreateProductCommandResponse>.Fail(400, "Ürün db ye eklenirken bir hata oluştu");
            
        }
    }
}
