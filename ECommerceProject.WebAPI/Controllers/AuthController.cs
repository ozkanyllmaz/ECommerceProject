using ECommerceProject.Application.Abstractions;
using ECommerceProject.Application.DTOs.Auth;
using ECommerceProject.Application.DTOs.Common;
using ECommerceProject.Application.Features.Auth.Commands.Register;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;

namespace ECommerceProject.WebAPI.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class AuthController : CustomBaseController
    {
        private readonly IAuthService _authService;

        public AuthController(IAuthService authService)
        {
            _authService = authService;
        }

        [HttpPost("register")]
        public async Task<IActionResult> Register([FromBody] RegisterCommandRequest request)
            => CreateActionResultInstance(await Mediator.Send(request));



        [HttpPost("login")]
        public async Task<IActionResult> Login([FromBody] UserLoginDto userLoginDto)
        {
            try
            {
                var tokenDto = await _authService.LoginAsync(userLoginDto);
                return CreateActionResultInstance(CustomResponseDto<TokenDto>.Success(200, "Giriş başarılı"));
            }
            catch(InvalidOperationException ex)
            {
                return CreateActionResultInstance(CustomResponseDto<TokenDto>.Fail(400, ex.Message));
            }
            catch (Exception ex)
            {
                return CreateActionResultInstance(CustomResponseDto<TokenDto>.Fail(500, ex.Message));
            }
        }

        [HttpPost("refresh-token")]
        public async Task<IActionResult> RefreshToken([FromQuery] string token)
        {
            try
            {
                var tokenDto = await _authService.CreateTokenByRefreshTokenAsync(token);
                return CreateActionResultInstance(CustomResponseDto<TokenDto>.Success(200, tokenDto, "Token başarıyla yenilendi"));
            }
            catch(KeyNotFoundException ex)
            {
                return CreateActionResultInstance(CustomResponseDto<TokenDto>.Fail(404, ex.Message));
            }
            catch(InvalidOperationException ex)
            {
                // süresi dolmuş token vb
                return CreateActionResultInstance(CustomResponseDto<TokenDto>.Fail(400, ex.Message));
            }
            catch (Exception)
            {
                return CreateActionResultInstance(CustomResponseDto<TokenDto>.Fail(500, "İşlem sırasında sistemsel bir hata oluştu."));
            }
        }
    }
}
