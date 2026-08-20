using ECommerceProject.Application.Abstractions;
using ECommerceProject.Application.DTOs.Common;
using MediatR;
using System;
using System.Collections.Generic;
using System.Text;

namespace ECommerceProject.Application.Features.Dashboards.Queries.GetDashboardSummary
{
    public class GetDashboardSummaryQueryRequest : IRequest<CustomResponseDto<GetDashboardSummaryQueryResponse>>, ISecuredRequest
    {
        public string[] Roles => ["Admin", "Manager"];
    }
}
