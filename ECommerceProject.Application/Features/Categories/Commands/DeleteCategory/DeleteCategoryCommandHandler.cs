using ECommerceProject.Application.DTOs.Common;
using ECommerceProject.Application.Repositories;
using ECommerceProject.Application.Exceptions;
using MediatR;
using System;
using System.Collections.Generic;
using System.Text;
using AutoMapper.QueryableExtensions;
using AutoMapper;

namespace ECommerceProject.Application.Features.Categories.Commands.DeleteCategory
{
    internal class DeleteCategoryCommandHandler : IRequestHandler<DeleteCategoryCommandRequest, CustomResponseDto>
    {
        private readonly ICategoryRepository _categoryRepository;
        private readonly IProductRepository _productRepository;
        private readonly IMapper _mapper;

        public DeleteCategoryCommandHandler(ICategoryRepository categoryRepository, IProductRepository productRepository, IMapper mapper)
        {
            _categoryRepository = categoryRepository;
            _productRepository = productRepository;
            _mapper = mapper;
        }

        public async Task<CustomResponseDto> Handle(DeleteCategoryCommandRequest request, CancellationToken cancellationToken)
        {
            var category = await _categoryRepository.GetByIdAsync(request.Id);
            if (category == null)
                throw new NotFoundException("Kategori bulunamadı");

            var productsInCategory = _productRepository
                .Where(x => x.CategoryId == category.Id)
                .ToList();

            var defaultCategoryId = "aa5ddda8-72e9-4f89-44a2-08deed724037";

            foreach (var product in productsInCategory)
            {
                product.CategoryId = Guid.Parse(defaultCategoryId);
            }

            _categoryRepository.Remove(category);
            await _categoryRepository.SaveAsync();

            return CustomResponseDto.Success(200, "Kategori başarıyla silindi.");
        }
    }
}
