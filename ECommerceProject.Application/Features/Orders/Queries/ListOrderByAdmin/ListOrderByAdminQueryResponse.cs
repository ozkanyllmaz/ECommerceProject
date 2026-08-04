using ECommerceProject.Domain.Entities.Enums;
using System;
using System.Collections.Generic;
using System.Text;

namespace ECommerceProject.Application.Features.Orders.Queries.ListOrderByAdmin
{
    public class ListOrderByAdminQueryResponse
    {
        public Guid OrderId { get; set; }
        public string OrderNumber { get; set; } = null!;
        public DateTime OrderDate { get; set; }
        public OrderStatus Status { get; set; }
        public decimal TotalAmount { get; set; }
        public int TotalItemCount { get; set; }
    }
}
