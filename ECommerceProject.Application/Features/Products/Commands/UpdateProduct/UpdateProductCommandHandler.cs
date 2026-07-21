using AutoMapper;
using ECommerceProject.Application.DTOs.Common;
using ECommerceProject.Application.Exceptions;
using ECommerceProject.Application.Repositories;
using ECommerceProject.Domain.Entities;
using MediatR;
using System;
using System.Collections.Generic;
using System.Text;

namespace ECommerceProject.Application.Features.Products.Commands.UpdateProduct
{
    internal class UpdateProductCommandHandler(IProductRepository _productRepository, IMapper _mapper) : IRequestHandler<UpdateProductCommandRequest, CustomResponseDto<UpdateProductCommandResponse>>
    {
        public async Task<CustomResponseDto<UpdateProductCommandResponse>> Handle(UpdateProductCommandRequest request, CancellationToken cancellationToken)
        {
            var product = await _productRepository.GetByIdAsync(request.Id.ToString());
            if (product == null)
                throw new NotFoundException($"Ürün bulunamadı Id={request.Id}");

            var updatedProduct = _mapper.Map(request, product);

            _productRepository.Update(updatedProduct);
            await _productRepository.SaveAsync();

            return CustomResponseDto<UpdateProductCommandResponse>.Success(200, "Ürün güncelleme başarılı");
        }
    }
}
