using ECommerceProject.Domain.Entities.Common;
using ECommerceProject.Domain.Entities.Enums;
using ECommerceProject.Domain.Entities.ValueObjects;
using System;
using System.Collections.Generic;
using System.Text;

namespace ECommerceProject.Domain.Entities
{
    public class Order : BaseEntity
    {
        public Guid UserId { get; set; }
        public string OrderNumber { get; set; } = null!;
        public DateTime OrderDate { get; set; }
        public OrderStatus Status { get; set; } = OrderStatus.SiparisAlindi;
        public decimal TotalAmount { get; set; }

        public Address ShippingAddress { get; set; } = null!;
        public Address BillingAddress { get; set; } = null!;

        public string? CancellationReason { get; set; }
        public DateTime? CancelledDate { get; set; }


        public ICollection<OrderItem> OrderItems { get; set; } = new HashSet<OrderItem>();

    }

}
