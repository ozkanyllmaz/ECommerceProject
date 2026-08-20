using System;
using System.Collections.Generic;
using System.Text;

namespace ECommerceProject.Application.Features.Dashboards.Queries.GetDashboardSummary
{
    public class GetDashboardSummaryQueryResponse
    {
        public string TotalOrders { get; set; } = null!;
        public decimal TotalRevenue { get; set; }

        public string TotalUsers { get; set; } = null!;
    }
}
