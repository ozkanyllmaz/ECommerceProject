using AutoMapper;
using ECommerceProject.Application.DTOs.Common;
using ECommerceProject.Application.Repositories;
using ECommerceProject.Application.Exceptions;
using MediatR;
using System;
using System.Collections.Generic;
using System.Text;

namespace ECommerceProject.Application.Features.Products.Commands.DeleteProduct
{
    internal class DeleteProductCommandHandler : IRequestHandler<DeleteProductCommandRequest, CustomResponseDto<DeleteProductCommandResponse>>
    {
        private readonly IProductRepository _productRepository;
        private readonly IMapper _mapper;

        public DeleteProductCommandHandler(IProductRepository productRepository, IMapper mapper)
        {
            _productRepository = productRepository;
            _mapper = mapper;
        }

        public async Task<CustomResponseDto<DeleteProductCommandResponse>> Handle(DeleteProductCommandRequest request, CancellationToken cancellationToken)
        {
            var product = await _productRepository.GetByIdAsync(request.Id.ToString());
            if (product == null)
                throw new NotFoundException($"Ürün bulunamadı Id={request.Id}");
            else
                _productRepository.Remove(product);
            await _productRepository.SaveAsync();

            return CustomResponseDto<DeleteProductCommandResponse>.Success(200, "Ürün silme başarılı.");
        }
    }
}
