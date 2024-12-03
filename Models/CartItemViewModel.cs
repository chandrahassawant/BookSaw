using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations.Schema; // Add this if needed


namespace BulkyWeb.Models
{
    public class CartItemViewModel
    {
        public int ProductId { get; set; }
        public string ProductName { get; set; }
        public int Quantity { get; set; }
        public decimal Price { get; set; }
        public decimal TotalPrice => Quantity * Price; // Dynamically calculated TotalPrice
    }
}
