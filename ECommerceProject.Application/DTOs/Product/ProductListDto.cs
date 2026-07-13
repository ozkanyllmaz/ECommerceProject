using System;
using System.Collections.Generic;
using System.Text;

namespace ECommerceProject.Application.DTOs.Product
{
    public class ProductListDto
    {
        public Guid Id { get; set; }
        public string Name { get; set; }
        public decimal Price { get; set; }
        public int Stock { get; set; }
        public string? ImageUrl { get; set; }
    }
}
