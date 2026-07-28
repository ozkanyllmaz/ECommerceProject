using ECommerceProject.Application.DTOs.Common;
using MediatR;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;

namespace ECommerceProject.WebAPI.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    [Authorize]
    public class CustomBaseController : ControllerBase
    {
        //private IMediator? _mediator;
        protected IMediator Mediator => HttpContext.RequestServices.GetService<IMediator>();

        [NonAction]
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

        [NonAction]
        public IActionResult CreateActionResultInstance(CustomResponseDto response)
        {
            // Http 204 (No Content) ise geri boş response döndür.
            if (response.StatusCode == 204)
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
