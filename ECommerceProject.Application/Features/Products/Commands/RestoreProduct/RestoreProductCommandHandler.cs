using AutoMapper;
using ECommerceProject.Application.DTOs.Common;
using ECommerceProject.Application.Repositories;
using MediatR;
using System;
using System.Collections.Generic;
using System.Text;

namespace ECommerceProject.Application.Features.Products.Commands.RestoreProduct
{
    internal class RestoreProductCommandHandler(IProductRepository _productRepository, IMapper _mapper) : IRequestHandler<RestoreProductCommandRequest, CustomResponseDto<RestoreProductCommandResponse>>
    {
        public async Task<CustomResponseDto<RestoreProductCommandResponse>> Handle(RestoreProductCommandRequest request, CancellationToken cancellationToken)
        {
            var softDeletedProduct = await _productRepository.GetByIdAsync(request.Id.ToString(),ignoreQueryFilters: true);
            if (softDeletedProduct == null)
                return CustomResponseDto<RestoreProductCommandResponse>.Fail(404, "Aranılan ürün bulunamadı.");
            else
                _productRepository.Restore(softDeletedProduct);
            await _productRepository.SaveAsync();

            return CustomResponseDto<RestoreProductCommandResponse>.Success(200, "Ürün getirildi (Aktif)");
        }
    }
}
