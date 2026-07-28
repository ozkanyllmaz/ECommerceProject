using ECommerceProject.Application.Features.Roles.Command.CreateRole;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;

namespace ECommerceProject.WebAPI.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class RoleController : CustomBaseController
    {
        [HttpPost]
        [AllowAnonymous]
        public async Task<IActionResult> CreateRole([FromBody] CreateRoleCommandRequest request) 
            => CreateActionResultInstance(await Mediator.Send(request));
    }
}
