using System.ComponentModel.DataAnnotations;
using System;

using System.ComponentModel.DataAnnotations.Schema;
using Microsoft.AspNetCore.Mvc.ModelBinding.Validation;

namespace BulkyWeb.Models
{
    public class OrderCheckOut
    {
        [Key]
        [Required]
        public string Id { get; set; } = Guid.NewGuid().ToString();


        [Required]
        public string? UserId { get; set; }

        [Required]
        public string? CustomerName { get; set; }

        [Required]
        public string? PhoneNumber { get; set; }

        [Required]
        public string? Address { get; set; }

        [Required]
        public string? City { get; set; }

        [Required]
        public string? State { get; set; }

        [Required]
        public string? PostalCode { get; set; }

        [Required]
        public string? Country { get; set; }

        public DateTime? OrderDate { get; set; }

        public DateTime? ShippingDate { get; set; }

        public decimal? CartTotal { get; set; }

        public string? OrderStatus { get; set; }

        public string? PaymentStatus { get; set; }

        public string? TrackingNumber { get; set; }

        public string? Carrier { get; set; }

        public string? AddressId{ get; set; }

        public DateTime? PaymentDate { get; set; }

        public DateTime? PaymentDueDate { get; set; }

        public string? PaymentIntentId { get; set; }

        public string? SessionId { get; set; }
        public string? TransactionId { get; set; }

        public string? OrderId { get; set; }

        [ForeignKey("UserId")]
        public User User { get; set; }
    }

}