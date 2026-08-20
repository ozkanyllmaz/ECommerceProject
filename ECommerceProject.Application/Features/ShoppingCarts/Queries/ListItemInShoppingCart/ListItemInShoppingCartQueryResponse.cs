using ECommerceProject.Domain.Entities;
using System;
using System.Collections.Generic;
using System.Text;

namespace ECommerceProject.Application.Features.ShoppingCarts.Queries.ListItemInShoppingCart
{
    public class ListItemInShoppingCartQueryResponse
    {
        public string Name { get; set; } = null!;
        public int Quantity { get; set; }
        public decimal Price { get; set; }
        public int Stock { get; set; }
        public string? ImageUrl { get; set; }
        public string ProductId { get; set; } = null!;
        public string CartItemId { get; set; } = null!;
    }
}
