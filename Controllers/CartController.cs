using BulkyWeb.Data;
using BulkyWeb.Models;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Identity;
using Microsoft.EntityFrameworkCore;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Authorization;

namespace BulkyWeb.Controllers
{
    public class CartController : Controller
    {
        // Only one instance of ApplicationDbContext and UserManager
        private readonly ApplicationDbContext _db;
        private readonly UserManager<User> _userManager;

        // Inject ApplicationDbContext and UserManager into the constructor
        public CartController(ApplicationDbContext db, UserManager<User> userManager)
        {
            _db = db;
            _userManager = userManager;
        }

        // Helper method to determine selected category
        private string IsSelected(int categoryId, int selectedCategoryId)
        {
            return categoryId == selectedCategoryId ? "selected" : "";
        }

        // Action to display the current cart items
        public IActionResult Cart()
        {
            // Get the currently logged-in user's Id
            var userId = _userManager.GetUserId(User);

            if (userId == null)
            {
                return RedirectToPage("/Account/Login", new { area = "Identity" });
            }

            // Get the cart items for the logged-in user
            var cartItems = _db.Carts
                .Where(c => c.UserId == userId && c.CartStatus == "open")
                .Include(c => c.Product) // Include Product details
                .ToList();

            return View(cartItems);
        }

        // Action to add a product to the cart
        public IActionResult AddToCart(int ProductId, int quantity)
{
    var product = _db.Products.Find(ProductId);
    if (product == null)
    {
        TempData["Error"] = "Product not found.";
        return RedirectToAction("Index", "Product");
    }

    var userId = _userManager.GetUserId(User);
    if (userId == null)
    {
        return RedirectToPage("/Account/Login", new { area = "Identity" });
    }

    var cartItem = _db.Carts.FirstOrDefault(c => c.ProductId == ProductId && c.UserId == userId && c.CartStatus == "open");

    if (cartItem != null)
    {
        cartItem.Quantity += quantity;
        _db.Carts.Update(cartItem);
    }
    else
    {
        _db.Carts.Add(new Cart
        {
            UserId = userId,
            ProductId = ProductId,
            Quantity = quantity,
            Price = (decimal)product.Price,
            CartStatus = "open"
        });
    }

    _db.SaveChanges();
    TempData["Success"] = "Product added to the cart successfully!";
    return RedirectToAction("Index", "Product");
}


        // Action to update the quantity of a product in the cart
        [HttpPost]
        public IActionResult UpdateQuantity(int cartId, int quantity)
        {
            if (quantity <= 0)
            {
                ModelState.AddModelError("", "Quantity must be greater than 0");
                return RedirectToAction("Cart", "Cart");
            }

            var cartItem = _db.Carts.FirstOrDefault(c => c.CartId == cartId);
            if (cartItem != null)
            {
                // Update the quantity
                cartItem.Quantity = quantity;

                // Save changes to the database
                _db.SaveChanges();
            }

            return RedirectToAction("Cart", "Cart");
        }
    }
}
