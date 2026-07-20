using AutoMapper;
using ECommerceProject.Application.DTOs.Common;
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

        public RegisterCommandHandler(IUserRepository userRepository, IMapper mapper)
        {
            _userRepository = userRepository;
            _mapper = mapper;
        }

        public async Task<CustomResponseDto<RegisterCommandResponse>> Handle(RegisterCommandRequest request, CancellationToken cancellationToken)
        {
            var isEmailExist = await _userRepository.AnyAsync(x => x.Email == request.Email);
            if (isEmailExist)
                return CustomResponseDto<RegisterCommandResponse>.Fail(400, "Bu email sistemde kayıtlı");

            HashingHelper.CreatePasswordHash(request.Password, out byte[] passwordHash, out byte[] passwordSalt);

            var user = _mapper.Map<User>(request);
            user.PasswordHash = passwordHash;
            user.PasswordSalt = passwordSalt;
            user.Status = true;

            await _userRepository.AddAsync(user);
            await _userRepository.SaveAsync();

            return CustomResponseDto<RegisterCommandResponse>.Success(201, "Kayıt oluşturuldu");

        }
    }
}
