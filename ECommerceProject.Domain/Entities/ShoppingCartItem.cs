using ECommerceProject.Domain.Entities.Common;
using System;
using System.Collections.Generic;
using System.Security.Cryptography.X509Certificates;
using System.Text;

namespace ECommerceProject.Domain.Entities
{
    public class ShoppingCartItem : BaseEntity
    {
        public Guid ShoppingCartId { get; set; }
        public Guid ProductId { get; set; } 
        public int Quantity { get; set; }

        public Product? Product { get; set; } = null!;
        public ShoppingCart ShoppingCart { get; set; } = null!;
    }
}
