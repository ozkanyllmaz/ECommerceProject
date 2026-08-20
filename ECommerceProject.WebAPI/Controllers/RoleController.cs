using ECommerceProject.Application.Features.Roles.Command.CreateRole;
using ECommerceProject.Application.Features.Roles.Queries.ListRole;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;

namespace ECommerceProject.WebAPI.Controllers
{
    [Route("api/[controller]/[action]")]
    [ApiController]
    public class RoleController : CustomBaseController
    {
        [HttpPost]
        public async Task<IActionResult> CreateRole([FromBody] CreateRoleCommandRequest request) 
            => CreateActionResultInstance(await Mediator.Send(request));

        [HttpGet]
        public async Task<IActionResult> ListRole([FromQuery] ListRoleQueryRequest request)
            => CreateActionResultInstance(await Mediator.Send(request));

    }
}
