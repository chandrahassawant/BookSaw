using BulkyWeb.Data;
using BulkyWeb.Models;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Identity;
using Microsoft.EntityFrameworkCore;
using Microsoft.AspNetCore.Authorization;

namespace BulkyWeb.Controllers
{
    public class ProductController : Controller
    {
        private readonly ApplicationDbContext _db;
        private readonly UserManager<User> _userManager;

        public ProductController(ApplicationDbContext db, UserManager<User> userManager)
        {
            _db = db;
            _userManager = userManager;
        }

        private string IsSelected(int categoryId, int selectedCategoryId)
        {
            return categoryId == selectedCategoryId ? "selected" : "";
        }

        public IActionResult Index()
        {
            var objProductList = _db.Products
                .Where(p => p.Title != null && p.IsAvailable == true)
                .OrderBy(c => c.Title)
                .ToList();

            return View(objProductList);
        }

        [Authorize(Roles = "Admin")]
        public IActionResult Create()
        {
            ViewBag.Categories = _db.Categories.ToList();
            return View();
        }

        [Authorize(Roles = "Admin")]
        public IActionResult Edit(int id)
        {
            var product = _db.Products.FirstOrDefault(c => c.ProductId == id);
            if (product == null)
            {
                return NotFound();
            }

            ViewBag.Categories = _db.Categories.ToList();
            return View(product);
        }

        [HttpPost]
        [Authorize(Roles = "Admin")]
        public IActionResult Edit(Product obj)
        {
            if (ModelState.IsValid)
            {
                var existingProduct = _db.Products.FirstOrDefault(p => p.ProductId == obj.ProductId);
                if (existingProduct == null)
                {
                    return NotFound();
                }

                existingProduct.Title = obj.Title;
                existingProduct.ISBN = obj.ISBN;
                existingProduct.Author = obj.Author;
                existingProduct.Description = obj.Description;
                existingProduct.ListPrice = obj.ListPrice;
                existingProduct.Price = obj.Price;
                existingProduct.Price50 = obj.Price50;
                existingProduct.Price100 = obj.Price100;
                existingProduct.ImageUrl = obj.ImageUrl;

                _db.Products.Update(existingProduct);
                _db.SaveChanges();

                TempData["success"] = "Product updated successfully!";
                return RedirectToAction("Index");
            }

            return View(obj);
        }

        [HttpPost]
        [Authorize(Roles = "Admin")]
        public IActionResult DeleteSelected(int CartId)
        {
            // Find the cart item by ID
            var cartItem = _db.Carts.FirstOrDefault(c => c.CartId == CartId);

            // Check if the cart item exists
            if (cartItem == null)
            {
                return Json(new { success = false, message = "Cart item not found." });
            }

            try
            {
                // Remove the cart item
                _db.Carts.Remove(cartItem);
                _db.SaveChanges();

                // Return a success message
                TempData["success"] = "Cart item deleted successfully!";
                return RedirectToAction("ViewCart", "Product");
            }
            catch (Exception ex)
            {
                // Log the error (optional)
                Console.WriteLine($"Error deleting cart item: {ex.Message}");

                // Return an error message
                TempData["error"] = "An error occurred while trying to delete the cart item. Please try again.";
                return RedirectToAction("ViewCart", "Product");
            }
        }


        public IActionResult ProductDetail(int ProductId)
        {
            var product = _db.Products.FirstOrDefault(c => c.ProductId == ProductId);
            if (product == null)
            {
                return NotFound();
            }

            return View(product);
        }
        //direct cart access
        [HttpGet]
        public IActionResult ViewCart()
{
    var userId = _userManager.GetUserId(User);
    if (userId == null)
    {
        return RedirectToPage("/Account/Login", new { area = "Identity" });
    }

    var cartItems = _db.Carts.Include(c => c.Product)
                              .Where(c => c.UserId == userId && c.CartStatus == "open")
                              .ToList();

    var quantity = cartItems.Sum(c => c.Quantity);

    // Calculate subtotal
    var subtotal = cartItems.Sum(item => item.Quantity * item.Product.Price);

    // Calculate discount (20% of subtotal)
    var discount = subtotal * 0.20;

    // Apply discount to subtotal
    var subtotalAfterDiscount = subtotal - discount;

    // Add shipping charges
    var shippingCharges = 50; // Or calculate dynamically if needed

    // Calculate tax as 18% of the discounted subtotal
    var tax = Math.Floor(subtotalAfterDiscount * 0.18);

    // Calculate total (discounted subtotal + shipping charges + tax)
    var total = subtotalAfterDiscount + shippingCharges + tax;

    // Round off the total amount to the nearest lower integer
    var roundedTotal = Math.Floor(total);

    // Store the rounded total in session
    HttpContext.Session.SetString("CartTotal", roundedTotal.ToString());

    // Pass necessary data to the view
    ViewBag.Subtotal = subtotal;
    ViewBag.Discount = discount;
    ViewBag.SubtotalAfterDiscount = subtotalAfterDiscount;
    ViewBag.ShippingCharges = shippingCharges;
    ViewBag.Tax = tax;
    ViewBag.Total = roundedTotal;
    ViewBag.Quantity= quantity;

    // var cartItems = _db.Carts.Include(c => c.Product)
    //                       .Where(c => c.UserId == userId && c.CartStatus == "open")
    //                       .ToList();


    return View(cartItems);

}


        [HttpPost]
        public IActionResult UpdateCart(int cartId, int quantity)
        {
            var cartItem = _db.Carts.Include(c => c.Product).FirstOrDefault(c => c.CartId == cartId);
            if (cartItem != null && quantity > 0)
            {
                cartItem.Quantity = quantity;
                _db.Carts.Update(cartItem);
                _db.SaveChanges();
            }
            return RedirectToAction("ViewCart");
        }

        [HttpPost]
        public IActionResult ViewCart(int ProductId, int quantity)
        {
            if (quantity <= 0)
            {
                return BadRequest("Invalid quantity specified.");
            }

            var product = _db.Products.Find(ProductId);
            if (product == null)
            {
                return NotFound();
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
                cartItem = new Cart
                {
                    UserId = userId,
                    ProductId = ProductId,
                    Quantity = quantity,
                    Price = (decimal)product.Price,
                    CartStatus = "open"
                };
                _db.Carts.Add(cartItem);
            }

            _db.SaveChanges();
            return RedirectToAction("Cart", "Cart");
        }

        [Authorize(Roles = "Admin,Seller")]
        public IActionResult GetStock()
        {
            var objProductList = _db.Products.OrderBy(p => p.ProductId).ToList();

            if (objProductList == null || !objProductList.Any())
            {
                return View("NoProducts");
            }

            return View(objProductList);
        }

        [Authorize(Roles = "Admin,Seller")]
        [HttpPost]
        [ValidateAntiForgeryToken]
        public IActionResult Delete(int ProductId)
        {
            var product = _db.Products.FirstOrDefault(p => p.ProductId == ProductId);

            if (product == null)
            {
                return Json(new { success = false, message = "Product not found." });
            }

            product.IsAvailable = false;

            _db.Products.Update(product);
            _db.SaveChanges();

            return Json(new { success = true, message = "Product marked as inactive successfully!" });
        }

        [Authorize(Roles = "Admin,Seller")]
        [HttpPost]
        [ValidateAntiForgeryToken]
        public IActionResult Enable(int ProductId)
        {
            var product = _db.Products.FirstOrDefault(u => u.ProductId == ProductId);

            if (product == null)
            {
                return Json(new { success = false, message = "Product not found." });
            }

            product.IsAvailable = true;
            _db.Products.Update(product);
            _db.SaveChanges();

            return Json(new { success = true, message = "Product enabled successfully!" });
        }

        [Authorize(Roles = "Admin,Seller")]
        public IActionResult EditProduct(int id)
        {
            var product = _db.Products.FirstOrDefault(p => p.ProductId == id);
            if (product == null)
            {
                return NotFound();
            }
            ViewBag.Categories = _db.Categories.ToList();
            return View(product);
        }

        [Authorize(Roles = "Admin,Seller")]
        [HttpPost]
        [ValidateAntiForgeryToken]
        public IActionResult SaveEditChanges(Product model, int ProductId)
        {
            if (!ModelState.IsValid)
            {
                ViewBag.Categories = _db.Categories.ToList();
                return View("EditProduct", model);
            }

            var product = _db.Products.FirstOrDefault(p => p.ProductId == model.ProductId);

            if (product == null)
            {
                return NotFound();
            }

            product.Title = model.Title;
            product.ISBN = model.ISBN;
            product.Author = model.Author;
            product.Description = model.Description;
            product.ListPrice = model.ListPrice;
            product.Price = model.Price;
            product.Price50 = model.Price50;
            product.Price100 = model.Price100;
            product.ImageUrl = model.ImageUrl;
            product.CategoryId = model.CategoryId;
            product.Stock = model.Stock;

            _db.Products.Update(product);
            _db.SaveChanges();

            TempData["Success"] = "Product updated successfully!";
            return RedirectToAction("GetStock");
        }

        [Authorize(Roles = "Admin,Seller")]
        [HttpGet]
        public IActionResult AddNewBook()
        {
            ViewBag.Categories = _db.Categories.ToList();
            return View();
        }

        [Authorize(Roles = "Admin,Seller")]
        [HttpPost]
        [ValidateAntiForgeryToken]
        public IActionResult AddNewBook(Product model)
        {
            if (!ModelState.IsValid)
            {
                ViewBag.Categories = _db.Categories.ToList();
                return View(model);
            }
            _db.Products.Add(model);
            _db.SaveChanges();
            TempData["Success"] = "Book added successfully!";
            return RedirectToAction("GetStock");
        }
    }
}
