using ECommerceProject.Application.Repositories;
using ECommerceProject.Domain.Entities;
using ECommerceProject.Persistance.Contexts;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.ChangeTracking;
using System;
using System.Collections.Generic;
using System.Text;

namespace ECommerceProject.Persistance.Repositories
{
    public class RefreshTokenRepository : GenericRepository<RefreshToken>, IRefreshTokenRepository
    {
        public RefreshTokenRepository(ECommerceDbContext context) : base(context)
        {
        }

        public Task<RefreshToken?> GetRefreshTokenWithRefreshTokenAndDeviceId(string refreshToken, string deviceId)
        {
            return _context.RefreshTokens
                .Include(x => x.User)
                .Where(rt => rt.Token == refreshToken && rt.DeviceId == deviceId)
                .FirstOrDefaultAsync();
        }

        public async Task<RefreshToken?> GetTokenByUserIdAndDeviceAsync(string userId, string deviceId)
        {
            return await _context.RefreshTokens
                .Include(u => u.User)
                .Where(rt => rt.UserId == Guid.Parse(userId) && rt.DeviceId == deviceId)
                .OrderByDescending(rt => rt.CreatedDate)
                .FirstOrDefaultAsync();
        }

        public async Task<RefreshToken?> GetAccessTokenWithUserAsync(string token)
        {
            // Token'ı ararken ilişkili kullanıcı bilgisini de join et
            return await _context.RefreshTokens
                .Include(rt => rt.User)
                .FirstOrDefaultAsync(rt => rt.AccessToken == token);
        }

        public async Task<string?> GetDeviceIdByRefreshToken(string refreshToken)
        {
            return await _context.RefreshTokens
                .Where(x => x.Token == refreshToken)
                .Select(x => x.DeviceId)
                .FirstOrDefaultAsync();
                
        }
    }
}
