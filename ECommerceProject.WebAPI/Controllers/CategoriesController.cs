using ECommerceProject.Application.Features.Categories.Commands.CreateCategory;
using ECommerceProject.Application.Features.Categories.Commands.DeleteCategory;
using ECommerceProject.Application.Features.Categories.Commands.RestoreCategory;
using ECommerceProject.Application.Features.Categories.Commands.UpdateCategory;
using ECommerceProject.Application.Features.Categories.Queries.ListCategory;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;

namespace ECommerceProject.WebAPI.Controllers
{
    [Route("api/[controller]/[action]")]
    [ApiController]
    public class CategoriesController : CustomBaseController
    {
        [HttpPost]
        public async Task<IActionResult> CreateCategory([FromBody] CreateCategoryCommandRequest request)
            => CreateActionResultInstance(await Mediator.Send(request));

        [HttpDelete]
        public async Task<IActionResult> DeleteCategory([FromQuery] DeleteCategoryCommandRequest request)
            => CreateActionResultInstance(await Mediator.Send(request));

        [HttpPut]
        public async Task<IActionResult> UpdateCategory([FromBody] UpdateCategoryCommandRequest request)
            => CreateActionResultInstance(await Mediator.Send(request));

        [AllowAnonymous]
        [HttpGet]
        public async Task<IActionResult> ListCategory([FromQuery] ListCategoryQueryRequest request)
            => CreateActionResultInstance(await Mediator.Send(request));

        [HttpPut]
        public async Task<IActionResult> RestoreCategory([FromQuery] RestoreCategoryCommandRequest request)
            => CreateActionResultInstance(await Mediator.Send(request));
    }
}
