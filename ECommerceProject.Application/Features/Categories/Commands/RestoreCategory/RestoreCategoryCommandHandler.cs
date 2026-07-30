using ECommerceProject.Application.DTOs.Common;
using ECommerceProject.Application.Repositories;
using ECommerceProject.Application.Exceptions;
using MediatR;
using System;
using System.Collections.Generic;
using System.Text;

namespace ECommerceProject.Application.Features.Categories.Commands.RestoreCategory
{
    internal class RestoreCategoryCommandHandler : IRequestHandler<RestoreCategoryCommandRequest, CustomResponseDto>
    {
        private readonly ICategoryRepository _categoryRepository;

        public RestoreCategoryCommandHandler(ICategoryRepository categoryRepository)
        {
            _categoryRepository = categoryRepository;
        }

        public async Task<CustomResponseDto> Handle(RestoreCategoryCommandRequest request, CancellationToken cancellationToken)
        {
            var category = await _categoryRepository.GetByIdAsync(request.Id, ignoreQueryFilters: true);
            if (category == null)
                throw new NotFoundException("Kategori bulunamadı");

            _categoryRepository.Restore(category);
            await _categoryRepository.SaveAsync();

            return CustomResponseDto.Success(200, "Kategori aktifleştirildi");
        }
    }
}
