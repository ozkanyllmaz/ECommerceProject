using System;
using System.Collections.Generic;
using System.Text;

namespace ECommerceProject.Application.DTOs.Product
{
    public class ProductCreateDto
    {
        public string Name { get; set; }
        public string Description { get; set; }
        public decimal Price { get; set; }
        public int Stock {  get; set; }
        public string? ImageUrl { get; set; }
    }
}
