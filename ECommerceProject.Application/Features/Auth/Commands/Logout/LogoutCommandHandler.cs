using ECommerceProject.Application.Abstractions;
using ECommerceProject.Application.DTOs.Common;
using ECommerceProject.Application.Repositories;
using ECommerceProject.Application.Exceptions;
using MediatR;
using Microsoft.AspNetCore.Http;
using System;
using System.Collections.Generic;
using System.Text;

namespace ECommerceProject.Application.Features.Auth.Commands.Logout
{
    internal class LogoutCommandHandler : IRequestHandler<LogoutCommandRequest, CustomResponseDto>
    {
        private readonly ICurrentUserService _currentUserService;
        private readonly IRefreshTokenRepository _refreshTokenRepository;
        private readonly IAuthCookieService _authCookieService;

        public LogoutCommandHandler(IRefreshTokenRepository refreshTokenRepository, ICurrentUserService currentUserService, IAuthCookieService authCookieService)
        {
            _refreshTokenRepository = refreshTokenRepository;
            _currentUserService = currentUserService;
            _authCookieService = authCookieService;
        }

        public async Task<CustomResponseDto> Handle(LogoutCommandRequest request, CancellationToken cancellationToken)
        {
            var userId = _currentUserService.UserId;
            if (userId == null)
                throw new NotFoundException("Kullanıcı bulunamadı");

            var refreshToken = await _refreshTokenRepository.DeleteTokenAsync(userId);
            if (refreshToken == null)
                throw new NotFoundException("refreshToken bulunamadı");

            _refreshTokenRepository.Remove(refreshToken);
            await _refreshTokenRepository.SaveAsync();

            _authCookieService.deleteCookies("X-Device-Id");

            return CustomResponseDto.Success(200, "Sistemden çıkış yapıldı");
        }
    }
}
