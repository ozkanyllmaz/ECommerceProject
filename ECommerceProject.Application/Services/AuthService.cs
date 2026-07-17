using AutoMapper;
using ECommerceProject.Application.Abstractions;
using ECommerceProject.Application.DTOs.Auth;
using ECommerceProject.Application.DTOs.Common;
using ECommerceProject.Application.Repositories;
using ECommerceProject.Application.Security.Hashing;
using ECommerceProject.Domain.Entities;
using System;
using System.Collections.Generic;
using System.Text;

namespace ECommerceProject.Application.Services
{
    public class AuthService : IAuthService
    {
        private readonly IUserRepository _userRepository;
        private readonly IRefreshTokenRepository _refreshTokenRepository;
        private readonly ITokenService _tokenService;
        private readonly IMapper _mapper;

        public AuthService(IUserRepository userRepository, IRefreshTokenRepository refreshTokenRepository, ITokenService tokenService, IMapper mapper)
        {
            _userRepository = userRepository;
            _refreshTokenRepository = refreshTokenRepository;
            _tokenService = tokenService;
            _mapper = mapper;
        }

        public async Task<TokenDto> CreateTokenByRefreshTokenAsync(string refreshToken)
        {
            var existingToken = await _refreshTokenRepository.GetTokenWithUserAsync(refreshToken);
            if(existingToken == null)
            {
                throw new KeyNotFoundException("Refresh token bulunamadı");
            }
            if (!existingToken.IsActive(DateTime.UtcNow))
            {
                throw new InvalidOperationException("Refresh token süresi dolmuş veya iptal edilmiş");
            }

            existingToken.RevokedDate = DateTime.UtcNow;
            existingToken.ReasonRevoked = "Yeni token ile değiştirildi";

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

            return newTokenDto; 
        }

        public async Task<TokenDto> LoginAsync(UserLoginDto userLoginDto)
        {
            var user = await _userRepository.GetByEmailAsync(userLoginDto.Email);
            if (user == null)
            {
                throw new ArgumentException("Email veya şifre hatalı");
            }
            if(!HashingHelper.VerifyPasswordHash(userLoginDto.Password, user.PasswordHash, user.PasswordSalt))
            {
                throw new ArgumentException("Email veya şifre hatalı");
            }

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

            return tokenDto;
        }

        public async Task RegisterAsync(UserRegisterDto userRegisterDto)
        {
            var isEmailExist = await _userRepository.AnyAsync(u => u.Email == userRegisterDto.Email);
            if (isEmailExist)
            {
                throw new InvalidOperationException("Bu email adresi zaten kullanılıyor.");
            }

            HashingHelper.CreatePasswordHash(userRegisterDto.Password, out byte[] passwordHash, out byte[] passwordSalt);

            var user = _mapper.Map<User>(userRegisterDto);
            user.PasswordHash = passwordHash;
            user.PasswordSalt = passwordSalt;
            user.Status = true;

            await _userRepository.AddAsync(user);
            await _userRepository.SaveAsync();

        }
    }
}
