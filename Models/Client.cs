using System.ComponentModel;
using System.ComponentModel.DataAnnotations;
using System;

namespace BulkyWeb.Models
{
    public class Client
    {
        [Key]
        [Required(ErrorMessage = "The Id field is required.")]
        public int Id { get; set; }

        [Required(ErrorMessage = "The Name field is required.")]
        [StringLength(100, ErrorMessage = "The Name must be at most 100 characters.")]
        public string? PublicationName { get; set; }

        [Required(ErrorMessage = "The Corporate Identification Number (CIN) is required.")]
        [StringLength(50, ErrorMessage = "The CIN must be at most 50 characters.")]
        public string? CorporateIdentificationNumber { get; set; }

        [Required(ErrorMessage = "The Corporate Email ID is required.")]
        [EmailAddress(ErrorMessage = "The Corporate Email ID is not a valid email address.")]
        public string? CorporateEmailId { get; set; }

        [StringLength(200, ErrorMessage = "The Street Address must be at most 200 characters.")]
        public string? StreetAddress { get; set; }

        [StringLength(100, ErrorMessage = "The City must be at most 100 characters.")]
        public string? City { get; set; }

        [StringLength(100, ErrorMessage = "The State must be at most 100 characters.")]
        public string? State { get; set; }

        [RegularExpression(@"^\d{5}(?:[-\s]\d{4})?$", ErrorMessage = "The Postal Code is not valid.")]
        public string? PostalCode { get; set; }

        [StringLength(100, ErrorMessage = "The Country must be at most 100 characters.")]
        public string? Country { get; set; }

        [Phone(ErrorMessage = "The Phone Number is not valid.")]
        public string? PhoneNumber { get; set; }

        [DataType(DataType.DateTime)]
        public DateTime DateRegistered { get; set; } = DateTime.Now; // Default value

        public bool IsActive { get; set; } = true; // Default value
    }
}
