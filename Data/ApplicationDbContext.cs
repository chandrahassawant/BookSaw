using BulkyWeb.Models;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Identity.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore;

namespace BulkyWeb.Data
{
    public class ApplicationDbContext : IdentityDbContext<IdentityUser>
    {
        public ApplicationDbContext(DbContextOptions<ApplicationDbContext> options) : base(options)
        {
        }

        public DbSet<Category> Categories { get; set; }
        public DbSet<Product> Products { get; set; }
        public DbSet<User> UserType { get; set; }
        public DbSet<User> Users { get; set; }
        public DbSet<Cart> Carts { get; set; }
        public DbSet<Client> Clients { get; set; }
        public DbSet<OrderDetail>? OrderDetails { get; set; }

        public DbSet<Demo> Demos { get; set; }

        public DbSet<OrderHeader> OrderHeaders { get; set; }

        public DbSet<OrderCheckOut> OrderCheckOuts { get; set; }

        public DbSet<OrderEntity> Orders { get; set; }

        public DbSet<CartItem> CartItems { get; set; }

        public DbSet<CustomerAddress>CustomerAddresses { get; set; }

        protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
        {
            base.OnConfiguring(optionsBuilder);
            optionsBuilder.ConfigureWarnings(warnings =>
              warnings.Ignore(Microsoft.EntityFrameworkCore.Diagnostics.RelationalEventId.PendingModelChangesWarning));
        }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {

            // modelBuilder.Entity<CartItemViewModel>().HasNoKey();

            base.OnModelCreating(modelBuilder);

            modelBuilder.Entity<OrderEntity>() .HasOne(o => o.OrderCheckOut) .WithMany() .HasForeignKey(o => o.OrderCheckOutId);

            // Seed Categories
            modelBuilder.Entity<Category>().HasData(
        new Category { CategoryId = 1, Name = "Action", DisplayOrder = 1 },
        new Category { CategoryId = 2, Name = "SciFi", DisplayOrder = 2 },
        new Category { CategoryId = 3, Name = "History", DisplayOrder = 3 }
      );

            // Seed Products
            modelBuilder.Entity<Product>().HasData(
        new Product
        {
            ProductId = 1,
            Title = "Eternal Bharat: Truth, Meaning & Beauty",
            Author = "Subhash Kak",
            Description = "Eternal Bhārat describes India's civilizational engagement with the eternal...",
            ISBN = "979-8885751537",
            ListPrice = 105,
            Price = 95,
            Price50 = 90,
            Price100 = 85,
            ImageUrl = "/product/Eternal Bharat Truth, Meaning & Beauty/front.jpg",
            CategoryId = 3
        },
        new Product
        {
            ProductId = 2,
            Title = "Savarkar: Echoes from a Forgotten Past",
            Author = "Vikram Sampath",
            Description = "As the intellectual fountainhead of the ideology of Hindutva...",
            ISBN = "978-0670090303",
            ListPrice = 45,
            Price = 40,
            Price50 = 35,
            Price100 = 30,
            ImageUrl = "/product/Savarkar Echoes from a Forgotten Past/front.jpg",
            CategoryId = 3
        },
        new Product
        {
            ProductId = 3,
            Title = "Breaking India: Western Interventions",
            Author = "Rajiv Malhotra",
            Description = "India's integrity is being undermined by three global networks...",
            ISBN = "B004WF4K5K",
            ListPrice = 60,
            Price = 55,
            Price50 = 50,
            Price100 = 45,
            ImageUrl = "/product/Breaking India Western Interventions/front.jpg",
            CategoryId = 1
        }
      );

            // Seed Clients
            modelBuilder.Entity<Client>().HasData(
        new Client
        {
            Id = 1,
            PublicationName = "Tech Innovators Inc.",
            CorporateIdentificationNumber = "CIN123456789",
            CorporateEmailId = "info@techinnovators.com",
            StreetAddress = "123 Innovation Street",
            City = "San Francisco",
            State = "California",
            PostalCode = "94105",
            Country = "USA",
            PhoneNumber = "+1-800-555-1234"
        },
        new Client
        {
            Id = 2,
            PublicationName = "Global Solutions Ltd.",
            CorporateIdentificationNumber = "CIN987654321",
            CorporateEmailId = "contact@globalsolutions.com",
            StreetAddress = "456 Enterprise Avenue",
            City = "New York",
            State = "New York",
            PostalCode = "10001",
            Country = "USA",
            PhoneNumber = "+1-800-555-6789"
        }
      );
        }
    }
}
