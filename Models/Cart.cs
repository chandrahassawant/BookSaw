using System;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace BulkyWeb.Models
{
    public class Cart
    {
        [Key]
        public int CartId { get; set; } // Primary Key

        [Required]
        public string? UserId { get; set; } // Foreign Key to User (string type to match IdentityUser Id)

        [Required]
        public int ProductId { get; set; } // Foreign Key to Product

        [Required]
        public int Quantity { get; set; } // Quantity of the product in the cart


        

        [Required]
        [Column(TypeName = "decimal(18,2)")] // Price of the product when added to the cart
        public decimal Price { get; set; }



        public DateTime AddedDate { get; set; } = DateTime.Now; // Date when the product was added to the cart

        public bool IsPurchased { get; set; } // Flag to indicate if the item has been purchased

        [StringLength(50)]
        public string? CartStatus { get; set; } // Cart status (e.g., "open", "completed", "abandoned")

        [Column(TypeName = "decimal(18,2)")]
        public decimal TotalPrice => Quantity * Price; // Dynamically calculated TotalPrice for the cart

        // Navigation properties
        [ForeignKey("UserId")]
        public User User { get; set; }

        [ForeignKey("ProductId")]
        public Product Product { get; set; }

        public List<CartItem> Items { get; set; }
    }
}
