using ECommerceProject.Application.Abstractions;
using ECommerceProject.Application.DTOs.Common;
using ECommerceProject.Application.Repositories;
using ECommerceProject.Application.Exceptions;
using MediatR;
using Microsoft.AspNetCore.Http;
using System;
using System.Collections.Generic;
using System.Text;
using ECommerceProject.Application.Abstractions.UnitOfWorks;

namespace ECommerceProject.Application.Features.Auth.Commands.Logout
{
    internal class LogoutCommandHandler : IRequestHandler<LogoutCommandRequest, CustomResponseDto>
    {
        private readonly ICurrentUserService _currentUserService;
        private readonly IRefreshTokenRepository _refreshTokenRepository;
        private readonly IUnitOfWork _unitOfWork;

        public LogoutCommandHandler(IRefreshTokenRepository refreshTokenRepository, ICurrentUserService currentUserService, IUnitOfWork unitOfWork)
        {
            _refreshTokenRepository = refreshTokenRepository;
            _currentUserService = currentUserService;
            _unitOfWork = unitOfWork;
        }

        public async Task<CustomResponseDto> Handle(LogoutCommandRequest request, CancellationToken cancellationToken)
        {
            var userId = _currentUserService.UserId;
            var deviceId = _currentUserService.DeviceId;
            if (userId == null || deviceId == null)
                throw new NotFoundException("Kullanıcı bulunamadı");

            var refreshToken = await _refreshTokenRepository.GetTokenByUserIdAndDeviceAsync(userId, deviceId);
            if (refreshToken == null)
                throw new NotFoundException("refreshToken bulunamadı");

            _refreshTokenRepository.Remove(refreshToken);
            await _unitOfWork.SaveChangesAsync(cancellationToken);

            return CustomResponseDto.Success(200, "Sistemden çıkış yapıldı");
        }
    }
}
