using ECommerceProject.Application.Abstractions;
using ECommerceProject.Application.DTOs.Product;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using System.Net;

namespace ECommerceProject.WebAPI.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class ProductsController : ControllerBase
    {
        private readonly IProductService _productService;

        public ProductsController(IProductService productService)
        {
            _productService = productService;
        }

        [HttpGet]
        public async Task<IActionResult> GetProducts()
        {
            var products = await _productService.GetAllProductListAsync();
            return Ok(products);
        }

        [HttpPost]
        public async Task<IActionResult> CreateProduct([FromBody] ProductCreateDto model)
        {
            var result = await _productService.InsertAsync(model);

            if (result)
            {
                return StatusCode(StatusCodes.Status201Created, "Ürün başarıyla eklendi");
            }
            return BadRequest("Ürün eklenirken bir hata oluştu");
        }
    }
}
