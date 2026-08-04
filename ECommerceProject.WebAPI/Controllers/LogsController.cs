using ECommerceProject.Application.Features.LogDb.Queries.AuditLogs;
using ECommerceProject.Application.Features.LogDb.Queries.ExceptionLogs;
using ECommerceProject.Application.Features.LogDb.Queries.RequestLogs;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;

namespace ECommerceProject.WebAPI.Controllers
{
    [Route("api/[controller]/[action]")]
    [ApiController]
    public class LogsController : CustomBaseController
    {
        [HttpGet]
        public async Task<IActionResult> ListExceptionLogs([FromQuery] ExceptionLogsQueryRequest request)
            => CreateActionResultInstance(await Mediator.Send(request));

        [HttpGet]
        public async Task<IActionResult> ListRequestLogs([FromQuery] RequestLogsQueryRequest request)
            => CreateActionResultInstance(await Mediator.Send(request));

        [HttpGet]
        public async Task<IActionResult> ListAuditLogs([FromQuery] AuditLogsQueryRequest request)
            => CreateActionResultInstance(await Mediator.Send(request));
    }
}
