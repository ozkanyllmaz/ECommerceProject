using ECommerceProject.Application.Abstractions;
using ECommerceProject.Application.DTOs.Common;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using System.Net;
using Microsoft.AspNetCore.Http.HttpResults;
using ECommerceProject.Domain.Entities;
using MediatR;
using ECommerceProject.Application.Features.Products.Commands.CreateProduct;
using ECommerceProject.Application.Features.Products.Queries.GetAllProducts;
using ECommerceProject.Application.Features.Products.Queries.GetProductById;
using ECommerceProject.Application.Features.Products.Commands.DeleteProduct;
using ECommerceProject.Application.Features.Products.Commands.RestoreProduct;
using ECommerceProject.Application.Features.Products.Commands.UpdateProduct;
using Microsoft.AspNetCore.Authorization;
using ECommerceProject.Application.Features.Products.Queries.GetUpdatedProduct;

namespace ECommerceProject.WebAPI.Controllers
{
    [Route("api/[controller]/[action]")]
    [ApiController]
    public class ProductsController : CustomBaseController
    {
        [Authorize]
        [HttpGet]
        public async Task<IActionResult> GetAllProduct([FromQuery] GetAllProductsQueryRequest request)
            => CreateActionResultInstance(await Mediator.Send(request));

        [HttpGet]
        public async Task<IActionResult> GetProductById([FromRoute] GetProductByIdQueryRequest request)
            => CreateActionResultInstance(await Mediator.Send(request));

        [Authorize]
        [HttpPost]
        public async Task<IActionResult> CreateProduct([FromBody] CreateProductCommandRequest request)
            => CreateActionResultInstance(await Mediator.Send(request));

        [HttpDelete]
        public async Task<IActionResult> DeleteProduct([FromRoute] DeleteProductCommandRequest request)
            => CreateActionResultInstance(await Mediator.Send(request));

        [HttpPut("{Id}/restore")]
        public async Task<IActionResult> RestoreProduct([FromRoute] RestoreProductCommandRequest request)
            => CreateActionResultInstance(await Mediator.Send(request));

        [HttpPut]
        [AllowAnonymous]
        public async Task<IActionResult> UpdateProduct([FromBody] UpdateProductCommandRequest request)
            => CreateActionResultInstance(await Mediator.Send(request));

        [HttpGet]
        public async Task<IActionResult> GetUpdatedProducts([FromQuery] GetUpdatedProductQueryRequest request)
            => CreateActionResultInstance(await Mediator.Send(request));
    }
}
