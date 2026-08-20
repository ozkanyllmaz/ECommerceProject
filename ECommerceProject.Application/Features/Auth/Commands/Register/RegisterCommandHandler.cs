using AutoMapper;
using ECommerceProject.Application.Abstractions.UnitOfWorks;
using ECommerceProject.Application.DTOs.Common;
using ECommerceProject.Application.Exceptions;
using ECommerceProject.Application.Repositories;
using ECommerceProject.Application.Security.Hashing;
using ECommerceProject.Domain.Entities;
using MediatR;
using System;
using System.Collections.Generic;
using System.Text;

namespace ECommerceProject.Application.Features.Auth.Commands.Register
{
    internal class RegisterCommandHandler : IRequestHandler<RegisterCommandRequest, CustomResponseDto<RegisterCommandResponse>>
    {
        private readonly IUserRepository _userRepository;
        private readonly IMapper _mapper;
        private readonly IUserRoleRepository _userRoleRepository;
        private readonly IUnitOfWork _unitOfWork;

        public RegisterCommandHandler(IUserRepository userRepository, IMapper mapper, IUserRoleRepository userRoleRepository, IUnitOfWork unitOfWork)
        {
            _userRepository = userRepository;
            _mapper = mapper;
            _userRoleRepository = userRoleRepository;
            _unitOfWork = unitOfWork;
        }

        public async Task<CustomResponseDto<RegisterCommandResponse>> Handle(RegisterCommandRequest request, CancellationToken cancellationToken)
        {
            var isEmailExist = await _userRepository.AnyAsync(x => x.Email == request.Email);
            if (isEmailExist)
                throw new BusinessException("Email zaten sistemde kayıtlı");

            HashingHelper.CreatePasswordHash(request.Password, out byte[] passwordHash, out byte[] passwordSalt);

            var user = _mapper.Map<User>(request);
            user.PasswordHash = passwordHash;
            user.PasswordSalt = passwordSalt;
            user.Status = true;
            
            await _userRepository.AddAsync(user);

            var userRole = new UserRole
            {
                UserId = user.Id,
                RoleId = Guid.Parse("cbd86d8d-ed20-4f99-da06-08deebd7254d")
            };

            await _userRoleRepository.AddAsync(userRole);
            await _unitOfWork.SaveChangesAsync();    

            return CustomResponseDto<RegisterCommandResponse>.Success(201, "Kayıt oluşturuldu");

        }
    }
}
