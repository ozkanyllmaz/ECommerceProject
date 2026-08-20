using ECommerceProject.Application.Features.Dashboards.Queries.GetDailyRevenue;
using ECommerceProject.Application.Features.Dashboards.Queries.GetDashboardPieChart;
using ECommerceProject.Application.Features.Dashboards.Queries.GetDashboardSummary;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;

namespace ECommerceProject.WebAPI.Controllers
{
    [Route("api/[controller]/[action]")]
    [ApiController]
    public class DashboardsController : CustomBaseController
    {
        [HttpGet]
        public async Task<IActionResult> GetDashboardAnalytic([FromQuery] GetDashboardSummaryQueryRequest request)
            => CreateActionResultInstance(await Mediator.Send(request));

        [HttpGet]
        public async Task<IActionResult> GetPieChartDatas([FromQuery] GetDashboardPieChartQueryRequest request)
            => CreateActionResultInstance(await Mediator.Send(request));

        [HttpGet]
        public async Task<IActionResult> GetBarChartDatas([FromQuery] GetDailyRevenueQueryRequest request)
            => CreateActionResultInstance(await Mediator.Send(request));
    }
}
