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

namespace ECommerceProject.WebAPI.Controllers
{
    [Route("api/[controller]/[action]")]
    [ApiController]
    public class ProductsController : CustomBaseController
    {
        private readonly IProductService _productService;

        public ProductsController(IProductService productService)
        {
            _productService = productService;
        }


        [HttpGet]
        public async Task<IActionResult> GetAllProduct([FromQuery] GetAllProductsQueryRequest request)
            => CreateActionResultInstance(await Mediator.Send(request));


        //[HttpGet("{id}")]
        //public async Task<IActionResult> GetProductById([FromRoute] Guid id)
        //{
        //    var productDto = await _productService.GetProductById(id);
        //    if (productDto == null)
        //    {
        //        return CreateActionResultInstance(CustomResponseDto<ProductDetailDto>.Fail(404, "Aradığınız ürün bulunamadı"));
        //    }
        //    return CreateActionResultInstance(CustomResponseDto<ProductDetailDto>.Success(200, productDto, "Ürün getirildi"));
        //}

        [HttpGet]
        public async Task<IActionResult> GetProductById([FromQuery] GetProductByIdQueryRequest request)
            => CreateActionResultInstance(await Mediator.Send(request));

        [HttpPost]
        public async Task<IActionResult> CreateProduct([FromBody] CreateProductCommandRequest request)
            => CreateActionResultInstance(await Mediator.Send(request));

        [HttpDelete("{id}")]
        public async Task<IActionResult> DeleteProduct([FromRoute] Guid id)
        {
            var result = await _productService.DeleteProductAsync(id);
            if (!result)
            {
                //return NotFound("Silinmek istenen ürün bulunamadı.");
                return CreateActionResultInstance(CustomResponseDto<NoContent>.Fail(404, "Silinecek ürün bulunamadı"));
            }
            //return Ok(new { message = "Ürün başarıyla silindi (Arşivlendi)" });
            return CreateActionResultInstance(CustomResponseDto<NoContent>.Success(204, "Ürün başarıyla silindi (Arşivlendi)"));
        }

        [HttpPost("{id}/restore")]
        public async Task<IActionResult> RestoreProduct([FromRoute] Guid id)
        {
            var result = await _productService.RestoreProductAsync(id);
            if (!result)
            {
                //return NotFound("Geri getirilmek istenen ürün bulunamadı");
                return CreateActionResultInstance(CustomResponseDto<NoContent>.Fail(404, "Geri getirilmek istenen ürün bulunamadı"));
            }
            //return Ok(new { message = "IsDeleted ürün geri getirildi. (Aktifleştirildi)" });
            return CreateActionResultInstance(CustomResponseDto<NoContent>.Success(204, "IsDeleted ürün geri getirildi. (Aktifleştirildi)"));
        }

        [HttpPut("{id}")]
        public async Task<IActionResult> UpdateProduct([FromRoute] Guid id, [FromBody] ProductUpdateDto model)
        {
            var result = await _productService.UpdateProductAsync(id, model);
            if (!result)
            {
                //return NotFound("Güncellenecek ürün bulunamadı.");
                return CreateActionResultInstance(CustomResponseDto<NoContent>.Fail(404, "Güncellenecek ürün bulunamadı."));
            }
            //return Ok(new { message = "Ürün başarılıyla güncellendi" });
            return CreateActionResultInstance(CustomResponseDto<NoContent>.Success(204, "Ürün başarılıyla güncellendi"));
        }
    }
}
