using AutoMapper;
using AutoMapper.QueryableExtensions;
using ECommerceProject.Application.DTOs.Common;
using ECommerceProject.Application.Repositories;
using MediatR;
using System;
using System.Collections.Generic;
using System.Text;

namespace ECommerceProject.Application.Features.Users.Queries.GetAllUsers
{
    internal class GetAllusersQueryHandler : IRequestHandler<GetAllusersQueryRequest, CustomResponseDto<List<GetAllusersQueryResponse>>>
    {
        private readonly IUserRepository _userRepository;
        private readonly IMapper _mapper;

        public GetAllusersQueryHandler(IUserRepository userRepository, IMapper mapper)
        {
            _userRepository = userRepository;
            _mapper = mapper;
        }

        public async Task<CustomResponseDto<List<GetAllusersQueryResponse>>> Handle(GetAllusersQueryRequest request, CancellationToken cancellationToken)
        {
            var query = _userRepository.GetListAsQueryable()
                .ProjectTo<GetAllusersQueryResponse>(_mapper.ConfigurationProvider);

            var users = await _userRepository.ToListAsync(query, cancellationToken);

            return CustomResponseDto<List<GetAllusersQueryResponse>>.Success(200, users, "Tüm kullanıcılar başarıyla getirildi");
        }
    }
}
