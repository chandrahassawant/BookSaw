using BulkyWeb.Models;
using Microsoft.AspNetCore.Mvc;
using Razorpay.Api;
using Microsoft.AspNetCore.Identity;
using BulkyWeb.Data;
using Microsoft.AspNetCore.Authorization;
using Microsoft.EntityFrameworkCore;
using System.Linq;


using System.Linq;
namespace BulkyWeb.Controllers
{
    public class OrderController : Controller
    {
        [BindProperty]
        public OrderEntity _OrderEntity { get; set; }

        private readonly UserManager<User> _userManager;
        private readonly ApplicationDbContext _context;
        private readonly ApplicationDbContext _db;


        public OrderController(UserManager<User> userManager, ApplicationDbContext context, ApplicationDbContext db)
        {
            _userManager = userManager;
            _context = context;
            _OrderEntity = new OrderEntity();  // Initialize _OrderEntity
            _db = db;
        }

        public IActionResult Index()
        {

            return View();
        }

        public IActionResult InitiateOrder()
        {
            if (!ModelState.IsValid)
            {
                TempData["Error"] = "Please correct the form errors.";
                return View("Index", _OrderEntity);
            }


            string key = "rzp_test_LmgaJSqt2yM8G4";
            string secret = "cXTNgW14Ke39k9zGHTk3z5Ux";

            Random _random = new Random();
            string TransactionId = _random.Next(0, 10000).ToString();

            Dictionary<string, object> input = new Dictionary<string, object>();

            var total = HttpContext.Session.GetString("CartTotal");
            if (decimal.TryParse(total, out decimal parsedTotal) && parsedTotal > 0)
            {
                input.Add("amount", (int)(parsedTotal * 100)); // Convert to integer in paise
            }
            else
            {
                TempData["Error"] = "Invalid cart total.";
                return RedirectToAction("Index");
            }



            input.Add("currency", "INR");
            input.Add("receipt", TransactionId);

            RazorpayClient client = new RazorpayClient(key, secret);

            Razorpay.Api.Order order = client.Order.Create(input);

            ViewBag.orderid = order["id"].ToString();
            return View("Payment", _OrderEntity);
        }




       public IActionResult Payment(string razorpay_payment_id, string razorpay_order_id, string razorpay_signature, string orderId, OrderCheckOut orderCheckOut, Cart cart)
{
    var userId = _userManager.GetUserId(User);
    var email = _userManager.GetUserName(User);

    // Ensure that an OrderCheckOut record is created first
    var orderCheckOutRecord = new OrderCheckOut
    {
        UserId = userId,
        CustomerName = _OrderEntity.CustomerName,
       
        OrderDate = DateTime.Now,
        CartTotal = decimal.Parse(HttpContext.Session.GetString("CartTotal")),
        OrderId = razorpay_order_id,
        
    };

    _context.OrderCheckOuts.Add(orderCheckOutRecord);
    _context.SaveChanges();

    

    // Save order details in the Orders table
    var orderdetails = new OrderEntity
    {
        TransactionId = razorpay_payment_id,
        OrderId = razorpay_order_id,
        SessionId = razorpay_signature,
        UserId = userId,
        Email = email,
        OrderDate = DateTime.Now,
        ExpectedDeliveryDate = DateTime.Now.AddDays(4),
        TotalAmount = decimal.Parse(HttpContext.Session.GetString("CartTotal")),
        AddressId =  HttpContext.Session.GetString("AddressId"),
        OrderCheckOutId = orderCheckOutRecord.Id  // Set the foreign key
    };

    _context.Orders.Add(orderdetails);
    _context.SaveChanges();

    return RedirectToAction("PaymentSuccess", new { orderId = orderdetails.OrderId });
}


        public IActionResult PaymentSuccess(string orderId)
        {
            ViewBag.OrderId = orderId;
            return View();
        }

        public async Task AddOrder(OrderEntity order, OrderCheckOut orderCheckOut)
        {
            // Update the OrderCheckOut entity with the new OrderId _context.OrderCheckOuts.Update(orderCheckOut);
            _context.Orders.Add(order);  // Adds the order to the Orders table

            await _context.SaveChangesAsync();

        }

        public IActionResult OrdersReceived()
{
    // Fetch all orders that are not yet shipped
    var orders = _context.Orders.Where(o => o.ItemShipped == false).ToList();
    return View(orders);
}



        public IActionResult OrderDetails()
        {
            var userId = _userManager.GetUserId(User);

            if (userId == null)
            {
                return RedirectToPage("/Account/Login", new { area = "Identity" });
            }

            // Fetch the cart items for the logged-in user
            var cartItems = _context.Carts
                                    .Where(c => c.UserId == userId)
                                    .Select(c => new CartItem
                                    {
                                        ProductId = c.ProductId,
                                        Quantity = c.Quantity
                                    })
                                    .ToList();

            // Fetch product details
            var productIds = cartItems.Select(ci => ci.ProductId).ToList();
            var products = _context.Products
                                   .Where(p => productIds.Contains(p.ProductId))
                                   .ToList();

            // Create the view model
            var viewModel = new OrderDetailsViewModel
            {
                Products = products,
                CartItems = cartItems
            };

            return View(viewModel);
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public IActionResult Ship(string OrderId)
        {
            try
            {
                // Find the order by its ID
                var order = _context.Orders.FirstOrDefault(o => o.OrderId == OrderId);
                if (order == null)
                {
                    return Json(new { success = false, message = "Order not found." });
                }

                // Mark the order as shipped
                order.ItemShipped = true; // Assuming there's an 'ItemShipped' property in the Order model
                order.TrackingNumber= Guid.NewGuid().ToString();
                order.ShippedDate = DateTime.Now; // Optionally set the shipped date
                _context.SaveChanges();

                // Return success message
                return Json(new { success = true, message = "Order marked as shipped." });
            }
            catch (Exception ex)
            {
                // Log the exception (optional)
                Console.WriteLine("Error marking order as shipped: " + ex.Message);

                // Handle any errors during the process
                return Json(new { success = false, message = "An error occurred while processing your request." });
            }
        }

        public IActionResult ShippingDetails()
        {
            var orderDetails = _context.OrderCheckOuts.ToList();
            ViewBag.OrderDetails = orderDetails;
            return View();
        }

        public IActionResult GetOrders()
        {
            var userId = _userManager.GetUserId(User);

            if (userId == null)
            {
                return RedirectToPage("/Account/Login", new { area = "Identity" });
            }

            var orders = _context.Orders
                .Where(o => o.UserId == userId)
                .ToList();
            
            return View(orders);
        }


    }
}
