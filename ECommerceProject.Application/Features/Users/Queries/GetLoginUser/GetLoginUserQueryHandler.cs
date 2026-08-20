using AutoMapper;
using ECommerceProject.Application.Abstractions;
using ECommerceProject.Application.Exceptions;
using ECommerceProject.Application.DTOs.Common;
using MediatR;
using System;
using System.Collections.Generic;
using System.Text;
using ECommerceProject.Application.Repositories;
using AutoMapper.QueryableExtensions;

namespace ECommerceProject.Application.Features.Users.Queries.GetLoginUser
{
    internal class GetLoginUserQueryHandler : IRequestHandler<GetLoginUserQueryRequest, CustomResponseDto<GetLoginUserQueryResponse>>
    {
        private readonly ICurrentUserService _currentUserService;
        private readonly IUserRepository _userRepository;
        private readonly IMapper _mapper;

        public GetLoginUserQueryHandler(ICurrentUserService currentUserService, IMapper mapper, IUserRepository userRepository)
        {
            _currentUserService = currentUserService;
            _mapper = mapper;
            _userRepository = userRepository;
        }

        public async Task<CustomResponseDto<GetLoginUserQueryResponse>> Handle(GetLoginUserQueryRequest request, CancellationToken cancellationToken)
        {
            var userId = _currentUserService.UserId;
            if (userId == null)
                throw new NotFoundException("Login olan kullanıcı bulunamadı");

            var query = _userRepository.GetLoginUser(userId)
                .ProjectTo<GetLoginUserQueryResponse>(_mapper.ConfigurationProvider);

            var user = await _userRepository.FirstOrDefaultAsync(query ,cancellationToken);
            if (user == null)
                throw new NotFoundException("Kullanıcı bulunamadı");

            return CustomResponseDto<GetLoginUserQueryResponse>.Success(200, user, "Login olan kullanıcı başarıyla getirildi");
        }
    }
}
