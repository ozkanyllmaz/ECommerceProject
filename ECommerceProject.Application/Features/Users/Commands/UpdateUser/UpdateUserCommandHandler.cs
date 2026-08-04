using ECommerceProject.Application.Abstractions;
using ECommerceProject.Application.DTOs.Common;
using ECommerceProject.Application.Repositories;
using ECommerceProject.Application.Exceptions;
using MediatR;
using System;
using System.Collections.Generic;
using System.Text;
using AutoMapper;
using AutoMapper.QueryableExtensions;
using ECommerceProject.Domain.Entities;
using ECommerceProject.Application.Abstractions.UnitOfWorks;
using ECommerceProject.Application.Security.Hashing;

namespace ECommerceProject.Application.Features.Users.Commands.UpdateUser
{
    internal class UpdateUserCommandHandler : IRequestHandler<UpdateUserCommandRequest, CustomResponseDto>
    {
        private readonly IUserRepository _userRepository;
        private readonly ICurrentUserService _currentUserService;
        private readonly IMapper _mapper;
        private readonly IUnitOfWork _unitOfWork;

        public UpdateUserCommandHandler(IUserRepository userRepository, ICurrentUserService currentUserService, IMapper mapper, IUnitOfWork unitOfWork)
        {
            _userRepository = userRepository;
            _currentUserService = currentUserService;
            _mapper = mapper;
            _unitOfWork = unitOfWork;
        }

        public async Task<CustomResponseDto> Handle(UpdateUserCommandRequest request, CancellationToken cancellationToken)
        {
            var userId = _currentUserService.UserId;
            if (string.IsNullOrEmpty(userId))
                throw new NotFoundException($"Kullanıcı bulunamadı: {userId}");

            var user = await _userRepository.GetByIdAsync(userId);
            if(user == null)
                throw new NotFoundException($"Kullanıcı bulunamadı: {user}");

            _mapper.Map(request, user);

            if (!string.IsNullOrWhiteSpace(request.Password))
            {
                HashingHelper.CreatePasswordHash(request.Password, out byte[] passwordHash, out byte[] passwordSalt);

                user.PasswordHash = passwordHash;
                user.PasswordSalt = passwordSalt;
            }

            _userRepository.Update(user);

            await _unitOfWork.SaveChangesAsync(cancellationToken);

            return CustomResponseDto.Success(200, "Kullanıcı başarıyla güncellendi");
        }
    }
}
