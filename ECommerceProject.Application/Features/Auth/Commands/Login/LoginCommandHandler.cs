using AutoMapper;
using ECommerceProject.Application.Abstractions;
using ECommerceProject.Application.DTOs.Common;
using ECommerceProject.Application.Repositories;
using ECommerceProject.Application.Security.Hashing;
using ECommerceProject.Application.Exceptions;
using ECommerceProject.Domain.Entities;
using MediatR;
using System;
using System.Collections.Generic;
using System.Text;

namespace ECommerceProject.Application.Features.Auth.Commands.Login
{
    internal class LoginCommandHandler : IRequestHandler<LoginCommandRequest, CustomResponseDto<LoginCommandResponse>>
    {
        private readonly IUserRepository _userRepository;
        private readonly ITokenService _tokenService;
        private readonly IRefreshTokenRepository _refreshTokenRepository;

        public LoginCommandHandler(IUserRepository userRepository, ITokenService tokenService, IRefreshTokenRepository refreshTokenRepository)
        {
            _userRepository = userRepository;
            _tokenService = tokenService;
            _refreshTokenRepository = refreshTokenRepository;
        }

        public async Task<CustomResponseDto<LoginCommandResponse>> Handle(LoginCommandRequest request, CancellationToken cancellationToken)
        {
            var user = await _userRepository.GetByEmailAsync(request.Email);
            if (user == null)
                throw new AuthenticationException("Email veya şifre hatalı");

            if (!HashingHelper.VerifyPasswordHash(request.Password, user.PasswordHash, user.PasswordSalt))
                throw new AuthenticationException("Email veya şifre hatalı");

            var roles = await _userRepository.GetRolesByUserIdAsync(user.Id);
            var tokenDto = _tokenService.CreateAccessToken(user, roles);

            var refreshToken = new RefreshToken
            {
                UserId = user.Id,
                Token = tokenDto.RefreshToken,
                ExpiresDate = tokenDto.AccessTokenExpiration.AddDays(7),
                CreatedByIp = "Unknown"
            };

            await _refreshTokenRepository.AddAsync(refreshToken);
            await _refreshTokenRepository.SaveAsync();

            var responseData = new LoginCommandResponse
            {
                AccessToken = tokenDto.AccessToken,
                AccessTokenExpiration = tokenDto.AccessTokenExpiration,
                RefreshToken = refreshToken.Token
            };

            return CustomResponseDto<LoginCommandResponse>.Success(200, responseData, "Giriş başarılı");
        }
    }
}
