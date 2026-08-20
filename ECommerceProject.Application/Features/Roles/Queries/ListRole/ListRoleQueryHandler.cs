using AutoMapper;
using AutoMapper.QueryableExtensions;
using ECommerceProject.Application.DTOs.Common;
using ECommerceProject.Application.Repositories;
using MediatR;
using System;
using System.Collections.Generic;
using System.Text;

namespace ECommerceProject.Application.Features.Roles.Queries.ListRole
{
    internal class ListRoleQueryHandler : IRequestHandler<ListRoleQueryRequest, CustomResponseDto<List<ListRoleQueryResponse>>>
    {
        private readonly IRoleRepository _roleRepository;
        private readonly IMapper _mapper;

        public ListRoleQueryHandler(IRoleRepository roleRepository, IMapper mapper)
        {
            _roleRepository = roleRepository;
            _mapper = mapper;
        }

        public async Task<CustomResponseDto<List<ListRoleQueryResponse>>> Handle(ListRoleQueryRequest request, CancellationToken cancellationToken)
        {
            var query = _roleRepository.GetListAsQueryable()
                .ProjectTo<ListRoleQueryResponse>(_mapper.ConfigurationProvider);

            var roles = await _roleRepository.ToListAsync(query, cancellationToken);

            return CustomResponseDto<List<ListRoleQueryResponse>>.Success(200, roles, "Roller listelendi");
        }
    }
}
