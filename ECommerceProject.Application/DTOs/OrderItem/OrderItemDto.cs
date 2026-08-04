using System;
using System.Collections.Generic;
using System.Text;

namespace ECommerceProject.Application.DTOs.OrderItem
{
    public class OrderItemDto
    {
        public Guid ProductId { get; set; }
        public string ProductName { get; set; } = null!;
        public decimal UnitPrice { get; set; }
        public int Quantity { get; set; }
        public decimal TotalPrice { get; set; }
    }
}
