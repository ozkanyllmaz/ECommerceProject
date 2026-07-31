using ECommerceProject.Application.Features.Orders.Commands.CreateOrder;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;

namespace ECommerceProject.WebAPI.Controllers
{
    [Route("api/[controller]/[action]")]
    [ApiController]
    public class OrdersController : CustomBaseController
    {
        [HttpPost]
        public async Task<IActionResult> CreateOrder([FromBody] CreateOrderCommandRequest request)
            => CreateActionResultInstance(await Mediator.Send(request));
    }
}
