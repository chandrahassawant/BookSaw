using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using Microsoft.AspNetCore.Identity;

namespace BulkyWeb.Models
{
    public class User : IdentityUser
    {
        [Required]
        [StringLength(100, ErrorMessage = "First name cannot exceed 100 characters.")]
        public string? FirstName { get; set; }

        [Required]
        [StringLength(100, ErrorMessage = "Last name cannot exceed 100 characters.")]
        public string? LastName { get; set; }

        public string? UserType { get; set; }

        public string? Avatar{get;set;}

        [Required]
        [EmailAddress]
        [StringLength(255, ErrorMessage = "Email cannot exceed 255 characters.")]
        public override string Email { get; set; } // Unique field


        [Phone]
        [StringLength(15, ErrorMessage = "Phone number cannot exceed 15 characters.")]
        public override string? PhoneNumber { get; set; }


        [StringLength(15, ErrorMessage = "Phone number cannot exceed 15 characters.")]

        public string? ContactNumber { get; set; }


        [StringLength(1000, ErrorMessage = "Address cannot exceed 1000 characters.")]
        public string? Address { get; set; }

        [StringLength(100)]
        public string? City { get; set; }

        [StringLength(100)]
        public string? State { get; set; }

        [StringLength(20)]
        public string? PostalCode { get; set; }

        [StringLength(100)]
        public string? Country { get; set; }


        [DataType(DataType.DateTime)]
        public DateTime DateRegistered { get; set; } = DateTime.Now; // Default value

        public bool IsActive { get; set; } = true; // Default value

        [DataType(DataType.DateTime)]
        public DateTime? LastLogin { get; set; }
    }
}
