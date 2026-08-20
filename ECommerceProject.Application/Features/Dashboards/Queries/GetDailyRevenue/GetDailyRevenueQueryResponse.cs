using ECommerceProject.Application.DTOs.Dashboard;
using System;
using System.Collections.Generic;
using System.Text;

namespace ECommerceProject.Application.Features.Dashboards.Queries.GetDailyRevenue
{
    public class GetDailyRevenueQueryResponse
    {
        public string DayName { get; set; } = null!;
        public decimal TotalRevenue { get; set; }
    }
}
