using ECommerceProject.Application.Features.ShoppingCarts.Queries.ListItemInShoppingCart;
using System;
using System.Collections.Generic;
using System.Text;

namespace ECommerceProject.Application.Features.ShoppingCarts.Queries.GetShoppingCartSummary
{
    public class GetShoppingCartSummaryQueryResponse
    {
        public List<ListItemInShoppingCartQueryResponse> CartItems { get; set; } = new();

        public decimal SubTotal { get; set; } // Vergisiz toplam
        public decimal TaxAmount { get; set; } 
        public decimal ShippingCost { get; set; } 
        public decimal TotalPrice { get; set; } 
    }
}
