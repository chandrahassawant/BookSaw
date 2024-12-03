using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace BulkyWeb.Models
{
    public class Product
    {
        [Key]  // Marks the Id as the primary key
        public int ProductId { get; set; }

        [Required]  // Ensures the Title is required
        public string? Title { get; set; }

        [Required]  // ISBN is required
        public string? ISBN { get; set; }

        [Required]  // Author is required
        public string? Author { get; set; }

       [MaxLength]
        public string? Description { get; set; }

        [Display(Name = "List Price")]  // Display name for UI
        [Range(1, 1000)]  // Ensures the price is within a specific range
        public double ListPrice { get; set; }

        [Display(Name = "Price for 1-50")]
        [Range(1, 1000)]
        public double Price { get; set; }

        [Display(Name = "Price for 50+")]
        [Range(1, 1000)]
        public double Price50 { get; set; }

        [Display(Name = "Price for 100+")]
        [Range(1, 1000)]
        public double Price100 { get; set; }

        public string ImageUrl { get; set; }



        [Display(Name = "Category")]
        public int CategoryId { get; set; }


        [ForeignKey("CategoryId")]  // Specifies that CategoryId is the foreign key
        public Category? Category { get; set; }  // Navigation to the related Category entity


        [DataType(DataType.Date)]
        public DateTime CreatedDate { get; set; } = DateTime.Today; // Default value with only the date


        public bool IsAvailable { get; set; } = true; // Default value

        public int Stock { get; set;}
    }
}
