using System;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using Microsoft.AspNetCore.Mvc.ModelBinding.Validation;

namespace BulkyWeb.Models
{

    public class OrderHeader
    {
        [Key]
        public int OrderId { get; set; }

        [Required]
        public string? UserId { get; set; }

     

        [Required]
        public DateTime OrderDate { get; set; }

        [Required]
        public string PhoneNumber { get; set; }

        [Required]
        public DateTime ShippingDate { get; set; }

        [StringLength(50)]
        public string OrderStatus { get; set; }

        [StringLength(50)]
        public string PaymentStatus { get; set; }

        [Required]
        public string CustomerName { get; set; }

        [StringLength(1000, ErrorMessage = "Address cannot exceed 1000 characters.")]
        [Required]
        public string Address { get; set; }

        [StringLength(100)]
        [Required]
        public string City { get; set; }

        [StringLength(100)]
        [Required]
        public string State { get; set; }

        [StringLength(20)]
        [Required]
        public string PostalCode { get; set; }

        [StringLength(100)]
        [Required]
        public string Country { get; set; }

        [Required]
        public double TotalAmount { get; set; }  // Added TotalAmount field with data type double

        // Navigation properties
        [ForeignKey("UserId")]
        public User User { get; set; }
    }
}
