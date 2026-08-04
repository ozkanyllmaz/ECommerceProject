using ECommerceProject.Application.Features.Users.Commands.UpdateUser;
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
    }
}
