using ECommerceProject.Domain.Entities.Common;
using System;
using System.Collections.Generic;
using System.Text;

namespace ECommerceProject.Domain.Entities
{
    public class Category : BaseEntity
    {
        public string Name { get; set; } = null!;
        public ICollection<Product>? Products { get; set; } = new HashSet<Product>();
    }
}
