using ECommerceProject.Application.Features.ShoppingCarts.Commands.AddItemToShoppingCart;
using ECommerceProject.Application.Features.ShoppingCarts.Commands.ClearShoppingCart;
using ECommerceProject.Application.Features.ShoppingCarts.Commands.DeleteItemInShoppingCart;
using ECommerceProject.Application.Features.ShoppingCarts.Commands.UpdateItemInShoppingCart;
using ECommerceProject.Application.Features.ShoppingCarts.Queries.GetShoppingCartSummary;
using ECommerceProject.Application.Features.ShoppingCarts.Queries.ListItemInShoppingCart;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;

namespace ECommerceProject.WebAPI.Controllers
{
    [Route("api/[controller]/[action]")]
    [AllowAnonymous]
    [ApiController]
    public class ShoppingCartsController : CustomBaseController
    {
        [HttpPost]
        public async Task<IActionResult> AddItemInCart([FromBody] AddItemToShoppingCartCommandRequest request)
            => CreateActionResultInstance(await Mediator.Send(request));

        [HttpDelete]
        public async Task<IActionResult> DeleteItemInCart([FromQuery] DeleteItemInShoppingCartCommandRequest request)
            => CreateActionResultInstance(await Mediator.Send(request));

        [HttpGet]
        public async Task<IActionResult> ListCartItem([FromQuery] ListItemInShoppingCartQueryRequest request)
            => CreateActionResultInstance(await Mediator.Send(request));

        [HttpPut]
        public async Task<IActionResult> UpdateQuantityItem([FromBody] UpdateShoppingCartItemQuantityCommandRequest request)
            => CreateActionResultInstance(await Mediator.Send(request));

        [HttpPost]
        public async Task<IActionResult> ClearCart(ClearShoppingCartCommandRequest request)
            => CreateActionResultInstance(await Mediator.Send(request));

        [HttpGet]
        public async Task<IActionResult> GetCartSummary([FromQuery] GetShoppingCartSummaryQueryRequest request)
            => CreateActionResultInstance(await Mediator.Send(request));
    }
}
