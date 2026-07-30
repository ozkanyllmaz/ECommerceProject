using ECommerceProject.Application.Abstractions;
using ECommerceProject.Application.DTOs.Common;
using ECommerceProject.Application.Features.Auth.Commands.Login;
using ECommerceProject.Application.Features.Auth.Commands.Logout;
using ECommerceProject.Application.Features.Auth.Commands.RefreshTokens;
using ECommerceProject.Application.Features.Auth.Commands.Register;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;

namespace ECommerceProject.WebAPI.Controllers
{
    [Route("api/[controller]/[action]")]
    [ApiController]
    public class AuthController : CustomBaseController
    {
        [AllowAnonymous]
        [HttpPost]
        public async Task<IActionResult> Register([FromBody] RegisterCommandRequest request)
            => CreateActionResultInstance(await Mediator.Send(request));

        [AllowAnonymous]
        [HttpPost]
        public async Task<IActionResult> Login([FromBody] LoginCommandRequest request)
            => CreateActionResultInstance(await Mediator.Send(request));

        [AllowAnonymous]
        [HttpPost]
        public async Task<IActionResult> RefreshToken([FromBody] RefreshTokensCommandRequest request)
            => CreateActionResultInstance(await Mediator.Send(request));

        [HttpPost]
        public async Task<IActionResult> Logout(LogoutCommandRequest request)
            => CreateActionResultInstance(await Mediator.Send(request));

    }
}
