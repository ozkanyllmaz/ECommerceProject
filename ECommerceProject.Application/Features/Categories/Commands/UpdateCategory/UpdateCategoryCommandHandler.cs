using AutoMapper;
using ECommerceProject.Application.DTOs.Common;
using ECommerceProject.Application.Repositories;
using ECommerceProject.Domain.Entities;
using ECommerceProject.Application.Exceptions;
using MediatR;
using System;
using System.Collections.Generic;
using System.Text;

namespace ECommerceProject.Application.Features.Categories.Commands.UpdateCategory
{
    internal class UpdateCategoryCommandHandler : IRequestHandler<UpdateCategoryCommandRequest, CustomResponseDto>
    {
        private readonly ICategoryRepository _categoryRepository;
        private readonly IMapper _mapper;

        public UpdateCategoryCommandHandler(ICategoryRepository categoryRepository, IMapper mapper)
        {
            _categoryRepository = categoryRepository;
            _mapper = mapper;
        }

        public async Task<CustomResponseDto> Handle(UpdateCategoryCommandRequest request, CancellationToken cancellationToken)
        {
            var category = await _categoryRepository.GetByIdAsync(request.Id);
            if (category == null)
                throw new NotFoundException("Kategori bulunamadı");
            _mapper.Map(request, category);

            _categoryRepository.Update(category);
            await _categoryRepository.SaveAsync();

            return CustomResponseDto.Success(200, "Güncelleme başarılı");
        }
    }
}
