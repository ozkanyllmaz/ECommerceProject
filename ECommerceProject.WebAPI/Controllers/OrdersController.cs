using ECommerceProject.Application.Features.Orders.Commands.ApproveOrderByAdmin;
using ECommerceProject.Application.Features.Orders.Commands.ApproveOrderByManager;
using ECommerceProject.Application.Features.Orders.Commands.CreateOrder;
using ECommerceProject.Application.Features.Orders.Commands.DeleteOrder;
using ECommerceProject.Application.Features.Orders.Commands.RejectOrderByAdmin;
using ECommerceProject.Application.Features.Orders.Commands.SoftDeleteOrder;
using ECommerceProject.Application.Features.Orders.Queries.ListAllOrder;
using ECommerceProject.Application.Features.Orders.Queries.ListOrder;
using ECommerceProject.Application.Features.Orders.Queries.ListOrderByAdmin;
using ECommerceProject.Application.Features.Orders.Queries.ListOrderByManager;
using ECommerceProject.Application.Features.Orders.Queries.ListOrderDetail;
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

        [HttpPut]
        public async Task<IActionResult> CancelOrder([FromBody] DeleteOrderCommandRequest request)
            => CreateActionResultInstance(await Mediator.Send(request));

        [HttpGet]
        public async Task<IActionResult> ListOrders([FromQuery] ListOrderQueryRequest request)
            => CreateActionResultInstance(await Mediator.Send(request));

        [HttpGet]
        public async Task<IActionResult> ListOrderDetail([FromQuery] ListOrderDetailQueryRequest request)
            => CreateActionResultInstance(await Mediator.Send(request));

        [HttpDelete]
        public async Task<IActionResult> DeleteOrder([FromQuery] SoftDeleteOrderCommandRequest request)
            => CreateActionResultInstance(await Mediator.Send(request));

        [HttpPut]
        public async Task<IActionResult> ApproveOrderByManager([FromQuery] ApproveOrderByManagerCommandRequest request)
            => CreateActionResultInstance(await Mediator.Send(request));

        [HttpGet]
        public async Task<IActionResult> ListOrderByManager([FromQuery] ListOrderByManagerQueryRequest request)
            => CreateActionResultInstance(await Mediator.Send(request));

        [HttpPut]
        public async Task<IActionResult> ApproveOrderByAdmin([FromQuery] ApproveOrderByAdminCommandRequest request)
            => CreateActionResultInstance(await Mediator.Send(request));

        [HttpGet]
        public async Task<IActionResult> ListOrderByAdmin([FromQuery] ListOrderByAdminQueryRequest request)
            => CreateActionResultInstance(await Mediator.Send(request));

        [HttpPut]
        public async Task<IActionResult> RejectOrderByAdmin([FromQuery] RejectOrderByAdminCommandRequest request)
            => CreateActionResultInstance(await Mediator.Send(request));

        [HttpGet]
        public async Task<IActionResult> ListAllOrders([FromQuery] ListAllOrderQueryRequest request)
            => CreateActionResultInstance(await Mediator.Send(request));
    }
}
