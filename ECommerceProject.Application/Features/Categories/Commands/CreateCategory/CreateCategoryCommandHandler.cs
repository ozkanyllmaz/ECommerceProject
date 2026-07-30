using AutoMapper;
using ECommerceProject.Application.DTOs.Common;
using ECommerceProject.Application.Repositories;
using ECommerceProject.Domain.Entities;
using MediatR;
using System;
using System.Collections.Generic;
using System.Text;

namespace ECommerceProject.Application.Features.Categories.Commands.CreateCategory
{
    internal class CreateCategoryCommandHandler : IRequestHandler<CreateCategoryCommandRequest, CustomResponseDto>
    {
        private readonly ICategoryRepository _categoryRepository;
        private readonly IMapper _mapper;

        public CreateCategoryCommandHandler(ICategoryRepository categoryRepository, IMapper mapper)
        {
            _categoryRepository = categoryRepository;
            _mapper = mapper;
        }

        public async Task<CustomResponseDto> Handle(CreateCategoryCommandRequest request, CancellationToken cancellationToken)
        {
            var category = _mapper.Map<Category>(request);

            await _categoryRepository.AddAsync(category);
            await _categoryRepository.SaveAsync();

            return CustomResponseDto.Success(201, "İşlem başarılı");
        }
    }
}
