using ECommerceProject.Application.Abstractions;
using ECommerceProject.Application.DTOs.Product;
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

namespace ECommerceProject.WebAPI.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class ProductsController : CustomBaseController
    {
        [HttpGet]
        public async Task<IActionResult> GetAllProduct([FromQuery] GetAllProductsQueryRequest request)
            => CreateActionResultInstance(await Mediator.Send(request));

        [HttpGet("{Id}")]
        public async Task<IActionResult> GetProductById([FromRoute] GetProductByIdQueryRequest request)
            => CreateActionResultInstance(await Mediator.Send(request));

        [HttpPost]
        public async Task<IActionResult> CreateProduct([FromBody] CreateProductCommandRequest request)
            => CreateActionResultInstance(await Mediator.Send(request));

        [HttpDelete("{Id}")]
        public async Task<IActionResult> DeleteProduct([FromRoute] DeleteProductCommandRequest request)
            => CreateActionResultInstance(await Mediator.Send(request));

        [HttpPut("{Id}/restore")]
        public async Task<IActionResult> RestoreProduct([FromRoute] RestoreProductCommandRequest request)
            => CreateActionResultInstance(await Mediator.Send(request));

        [HttpPut]
        public async Task<IActionResult> UpdateProduct([FromBody] UpdateProductCommandRequest request)
            => CreateActionResultInstance(await Mediator.Send(request));
    }
}
