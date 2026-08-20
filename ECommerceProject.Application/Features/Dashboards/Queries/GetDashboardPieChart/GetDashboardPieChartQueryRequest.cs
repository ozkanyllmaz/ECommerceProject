using ECommerceProject.Application.Abstractions;
using ECommerceProject.Application.DTOs.Common;
using MediatR;
using System;
using System.Collections.Generic;
using System.Text;

namespace ECommerceProject.Application.Features.Dashboards.Queries.GetDashboardPieChart
{
    public class GetDashboardPieChartQueryRequest : IRequest<CustomResponseDto<GetDashboardPieChartQueryResponse>>, ISecuredRequest
    {
        public string[] Roles => ["Admin", "Manager"];
    }
}
