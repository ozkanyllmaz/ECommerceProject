using ECommerceProject.Domain.Entities.Common;
using System;
using System.Collections.Generic;
using System.Text;

namespace ECommerceProject.Domain.Entities
{
    public class OrderItem : BaseEntity
    {
        public Guid OrderId { get; set; }
        public Guid ProductId { get; set; }
        public string ProductName { get; set; } = null!;
        public decimal UnitPrice { get; set; }
        public int Quantity { get; set; }
        public decimal TotalPrice { get; set; }

        public Order Order { get; set; } = null!;
    }
}
