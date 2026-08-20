using System;
using System.Collections.Generic;
using System.Text;

namespace ECommerceProject.Application.DTOs.Dashboard
{
    public class WeeklyRevenueDto
    {
        public string DayName { get; set; } = null!;
        public decimal TotalRevenue { get; set; }
    }
}
