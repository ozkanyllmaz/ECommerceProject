using ECommerceProject.Application.Abstractions;
using ECommerceProject.Application.DTOs.Common;
using ECommerceProject.Application.Repositories;
using ECommerceProject.Application.Exceptions;
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
        private readonly ICurrentUserService _currentUserService;

        public RefreshTokensCommandHandler(IRefreshTokenRepository refreshTokenRepository, IUserRepository userRepository, ITokenService tokenService, ICurrentUserService currentUserService)
        {
            _refreshTokenRepository = refreshTokenRepository;
            _userRepository = userRepository;
            _tokenService = tokenService;
            _currentUserService = currentUserService;
        }

        public async Task<CustomResponseDto<RefreshTokensCommandResponse>> Handle(RefreshTokensCommandRequest request, CancellationToken cancellationToken)
        {
            var deviceId = _currentUserService.DeviceId;
            if (string.IsNullOrEmpty(deviceId))
                throw new NotFoundException("Cihaz kimliği bulunamadı"); 

            var existingToken = await _refreshTokenRepository.GetRefreshTokenWithRefreshTokenAndDeviceId(request.RefreshToken, deviceId);
            if (existingToken == null)
                throw new NotFoundException("Token bulunamadı");
            if (!existingToken.IsActive(DateTime.UtcNow))
                throw new AuthenticationException("Oturum süresi dolmuş. Lütfen tekrar giriş yapın.");

            existingToken.RevokedDate = DateTime.UtcNow;
            existingToken.ReasonRevoked = "Yeni token ile değiştirildi.";

            var createByIp = _currentUserService.CreatedById ?? "Unknown";

            var roles = await _userRepository.GetRolesByUserIdAsync(existingToken.UserId);
            var newTokenDto = _tokenService.CreateAccessToken(existingToken.User, roles, deviceId);

            var newRefreshToken = new RefreshToken
            {
                UserId = existingToken.UserId,
                Token = newTokenDto.RefreshToken,
                AccessToken = newTokenDto.AccessToken,
                ExpiresDate = newTokenDto.AccessTokenExpiration.AddDays(7),
                CreatedByIp = createByIp,
                ReplacedByToken = newTokenDto.RefreshToken,
                DeviceId = deviceId
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
