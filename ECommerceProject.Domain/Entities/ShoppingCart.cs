using ECommerceProject.Domain.Entities.Common;
using System;
using System.Collections.Generic;
using System.Text;

namespace ECommerceProject.Domain.Entities
{
    public class ShoppingCart : BaseEntity
    {
        public Guid UserId { get; set; }
        public bool IsActive { get; set; }

        public ICollection<ShoppingCartItem> ShoppingCartItems { get; set; } = new HashSet<ShoppingCartItem>(); 
    }
}
