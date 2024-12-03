using System.ComponentModel.DataAnnotations.Schema;
using System.ComponentModel.DataAnnotations;
using Newtonsoft.Json;

namespace BulkyWeb.Models
{
    public class OrderEntity
    {


        public string? Id { get; set; } = Guid.NewGuid().ToString();



        [ForeignKey("OrderCheckOut")]
        public string? OrderCheckOutId { get; set; }

        public string? CustomerName { get; set; }

        public string? Email { get; set; }


        public DateTime? OrderDate { get; set; }

        public DateTime? ShippedDate { get; set; }

        public DateTime? ExpectedDeliveryDate { get; set; }

        public DateTime? DeliveredDate { get; set; }

        public decimal? TotalAmount { get; set; }

        public string? OrderId { get; set; }

        public string? SessionId { get; set; }

        public string? TransactionId { get; set; }

        public string? AddressId{ get; set; }


        public string? TrackingNumber { get; set; }


        [ForeignKey("UserId")] // Specifies UserId as the foreign key for the User entity
        public string? UserId { get; set; }



        public bool? ItemShipped { get; set; } = false;

        public User User { get; set; }

        public OrderCheckOut OrderCheckOut { get; set; }



    }
}
