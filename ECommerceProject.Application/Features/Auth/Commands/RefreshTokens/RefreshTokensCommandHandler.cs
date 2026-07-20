using ECommerceProject.Application.Abstractions;
using ECommerceProject.Application.DTOs.Common;
using ECommerceProject.Application.Repositories;
using ECommerceProject.Domain.Entities;
using MediatR;
using System;
using System.Collections.Generic;
using System.Text;

namespace ECommerceProject.Application.Features.Auth.Commands.RefreshTokens
{
    internal class RefreshTokensCommandHandler : IRequestHandler<RefreshTokensCommandRequest, CustomResponseDto<RefreshTokensCommandResponse>>
    {
        private readonly IRefreshTokenRepository _refreshTokenRepository;
        private readonly IUserRepository _userRepository;
        private readonly ITokenService _tokenService;

        public RefreshTokensCommandHandler(IRefreshTokenRepository refreshTokenRepository, IUserRepository userRepository, ITokenService tokenService)
        {
            _refreshTokenRepository = refreshTokenRepository;
            _userRepository = userRepository;
            _tokenService = tokenService;
        }

        public async Task<CustomResponseDto<RefreshTokensCommandResponse>> Handle(RefreshTokensCommandRequest request, CancellationToken cancellationToken)
        {
            var existingToken = await _refreshTokenRepository.GetTokenWithUserAsync(request.RefreshToken);
            if (existingToken == null)
                return CustomResponseDto<RefreshTokensCommandResponse>.Fail(404, "Token bulunamadı");
            if(!existingToken.IsActive(DateTime.UtcNow))
                return CustomResponseDto<RefreshTokensCommandResponse>.Fail(400, "Token süresi dolmuş veya iptal edilmiş");

            existingToken.RevokedDate = DateTime.UtcNow;
            existingToken.ReasonRevoked = "Yeni token ile değiştirildi.";

            var roles = await _userRepository.GetRolesByUserIdAsync(existingToken.UserId);
            var newTokenDto = _tokenService.CreateAccessToken(existingToken.User, roles);

            var newRefreshToken = new RefreshToken
            {
                UserId = existingToken.UserId,
                Token = newTokenDto.RefreshToken,
                ExpiresDate = newTokenDto.AccessTokenExpiration.AddDays(7),
                CreatedByIp = "Unknown",
                ReplacedByToken = newTokenDto.RefreshToken
            };

            _refreshTokenRepository.Update(existingToken);
            await _refreshTokenRepository.AddAsync(newRefreshToken);
            await _refreshTokenRepository.SaveAsync();

            var responseData = new RefreshTokensCommandResponse
            {
                AccessToken = newTokenDto.AccessToken,
                AccessTokenExpiration = newTokenDto.AccessTokenExpiration,
                RefreshToken = newTokenDto.RefreshToken
            };

            return CustomResponseDto<RefreshTokensCommandResponse>.Success(200, responseData, "Yeni refresh token oluşturuldu");

        }
    }
}
