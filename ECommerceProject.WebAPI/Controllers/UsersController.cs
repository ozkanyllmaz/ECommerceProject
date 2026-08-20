using ECommerceProject.Application.Features.Users.Commands.UpdateUser;
using ECommerceProject.Application.Features.Users.Commands.UpdateUserRoles;
using ECommerceProject.Application.Features.Users.Commands.UpdateUserStatus;
using ECommerceProject.Application.Features.Users.Queries.GetAllUsers;
using ECommerceProject.Application.Features.Users.Queries.GetLoginUser;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;

namespace ECommerceProject.WebAPI.Controllers
{
    [Route("api/[controller]/[action]")]
    [ApiController]
    public class UsersController : CustomBaseController
    {
        [HttpPut]
        public async Task<IActionResult> UpdateUser([FromBody] UpdateUserCommandRequest request)
            => CreateActionResultInstance(await Mediator.Send(request));

        [HttpGet]
        public async Task<IActionResult> GetLoginUser([FromQuery] GetLoginUserQueryRequest request)
            => CreateActionResultInstance(await Mediator.Send(request));

        [HttpGet]
        public async Task<IActionResult> GetAllUsers([FromQuery] GetAllusersQueryRequest request)
            => CreateActionResultInstance(await Mediator.Send(request));

        [HttpPut]
        public async Task<IActionResult> UpdateStatus([FromQuery] UpdateUserStatusCommandRequest request)
            => CreateActionResultInstance(await Mediator.Send(request));

        [HttpPut]
        public async Task<IActionResult> UpdateUserRoles([FromBody] UpdateUserRolesCommandsRequest request)
            => CreateActionResultInstance(await Mediator.Send(request));
    }
}
