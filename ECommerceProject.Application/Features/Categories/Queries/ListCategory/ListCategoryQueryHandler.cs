using AutoMapper;
using AutoMapper.QueryableExtensions;
using ECommerceProject.Application.DTOs.Common;
using ECommerceProject.Application.Extensions;
using ECommerceProject.Application.Repositories;
using MediatR;
using System;
using System.Collections.Generic;
using System.Text;

namespace ECommerceProject.Application.Features.Categories.Queries.ListCategory
{
    internal class ListCategoryQueryHandler : IRequestHandler<ListCategoryQueryRequest, CustomResponseDto<PaginationResult<ListCategoryQueryResponse>>>
    {
        private readonly ICategoryRepository _categoryRepository;
        private readonly IMapper _mapper;

        public ListCategoryQueryHandler(ICategoryRepository categoryRepository, IMapper mapper)
        {
            _categoryRepository = categoryRepository;
            _mapper = mapper;
        }

        public async Task<CustomResponseDto<PaginationResult<ListCategoryQueryResponse>>> Handle(ListCategoryQueryRequest request, CancellationToken cancellationToken)
        {
            var query = _categoryRepository.GetListAsQueryable(tracking: false);

            var pagedResult = await query
                .ProjectTo<ListCategoryQueryResponse>(_mapper.ConfigurationProvider)
                .ToPaginatedResultAsync(request.paginationParameter, cancellationToken);

            return CustomResponseDto<PaginationResult<ListCategoryQueryResponse>>.Success(200, pagedResult, "Kategoriler başarıyla getirildi");
        }
    }
}
