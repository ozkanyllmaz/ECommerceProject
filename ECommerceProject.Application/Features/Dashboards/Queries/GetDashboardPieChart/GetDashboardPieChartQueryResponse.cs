using ECommerceProject.Application.DTOs.Dashboard;
using ECommerceProject.Domain.Entities;
using System;
using System.Collections.Generic;
using System.Text;

namespace ECommerceProject.Application.Features.Dashboards.Queries.GetDashboardPieChart
{
    public class GetDashboardPieChartQueryResponse
    {
        public List<ProductSalesResult> ProductSalesResults { get; set; } = new List<ProductSalesResult>();
    }
}
