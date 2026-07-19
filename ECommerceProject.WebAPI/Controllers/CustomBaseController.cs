using ECommerceProject.Application.DTOs.Common;
using MediatR;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;

namespace ECommerceProject.WebAPI.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class CustomBaseController : ControllerBase
    {
        private IMediator? _mediator;
        protected IMediator? Mediator => _mediator ??= HttpContext.RequestServices.GetService<IMediator>();
        public IActionResult CreateActionResultInstance<T>(CustomResponseDto<T> response)
        {
            // Http 204 (No Content) ise geri boş response döndür.
            if(response.StatusCode == 204)
            {
                return new ObjectResult(null)
                {
                    StatusCode = response.StatusCode,
                };
            }
            // Diğer tüm durumlar için (200, 400, 404, 500) için veriyi ve kodu paketle dön.
            return new ObjectResult(response)
            {
                StatusCode = response.StatusCode,
            };
        }
    }
}
