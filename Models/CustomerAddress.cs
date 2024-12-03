using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using Microsoft.AspNetCore.Identity;

namespace BulkyWeb.Models
{
    public class CustomerAddress
    {
        [Key]
        [Required]
        public string Id { get; set; } = Guid.NewGuid().ToString();

        [Required]
        [ForeignKey("Id")]
        public string CustomerId { get; set; }

        [Required(ErrorMessage = "Name is required.")]
        public string Name { get; set; }

        
        [Required(ErrorMessage = "PhoneNumber is required.")]
        public string PhoneNumber { get; set; }

        [Required(ErrorMessage = "Street Address is required.")]
        [Display(Name = "Street Address")]
        public string StreetAddress { get; set; }

        [Required(ErrorMessage = "City is required.")]
        public string City { get; set; }

        [Required(ErrorMessage = "State is required.")]
        public string State { get; set; }

        [Required(ErrorMessage = "Postal Code is required.")]
        [RegularExpression(@"^\d{6}(-\d{4})?$", ErrorMessage = "Invalid Postal Code format.Postal Code must be 6 digits")]
        public string PostalCode { get; set; }

        [Required(ErrorMessage = "Country is required.")]
        public string Country { get; set; }

        [Required] 
        public string UserId { get; set; }

        public virtual User User { get; set; }
    }
}
