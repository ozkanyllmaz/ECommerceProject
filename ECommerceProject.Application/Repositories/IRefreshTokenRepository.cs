using ECommerceProject.Domain.Entities;
using System;
using System.Collections.Generic;
using System.Text;

namespace ECommerceProject.Application.Repositories
{
    public interface IRefreshTokenRepository:IGenericRepository<RefreshToken>
    {
        Task<RefreshToken?> GetAccessTokenWithUserAsync(string refreshToken);
        Task<RefreshToken?> GetRefreshTokenWithRefreshTokenAndDeviceId(string refreshToken, string deviceId);
        Task<RefreshToken?> GetTokenByUserIdAndDeviceAsync(string userId, string deviceId);

    }
}
