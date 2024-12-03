using Microsoft.AspNetCore.Mvc;
using BulkyWeb.Data;
using Microsoft.AspNetCore.Identity;
using Microsoft.EntityFrameworkCore;
using BulkyWeb.Models;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Identity;
using BulkyWeb.Data;
using Razorpay.Api;

using Microsoft.AspNetCore.Authorization;


namespace BulkyWeb.Controllers
{
    public class OrderCheckOutController : Controller
    {
        private readonly ApplicationDbContext _context;
        private readonly UserManager<User> _userManager;

        public OrderCheckOutController(ApplicationDbContext context, UserManager<User> userManager)
        {
            _context = context;
            _userManager = userManager;
        }

        public async Task<IActionResult> Index()
        {
            var userId = _userManager.GetUserId(User);

            if (userId == null)
            {
                return Unauthorized(); // Return unauthorized if user ID is not found
            }

            var customerAddresses = await _context.CustomerAddresses
                .Where(address => address.UserId == userId)
                .ToListAsync();

            return View(customerAddresses);
        }
        [HttpPost]
        public IActionResult SubmitAddress(string addressId)
        {
            if (string.IsNullOrEmpty(addressId))
            {
                return BadRequest("Address ID is required.");
            }

            try
            {
                Console.WriteLine($"Received AddressId: {addressId}"); // Log the received address ID

                HttpContext.Session.SetString("AddressId", addressId);

                // var orderCheckOut = new OrderCheckOut
                // {
                //     AddressId = addressId
                // };

                // _context.OrderCheckOuts.Update(orderCheckOut);
                // _context.SaveChanges();
            }
            catch (Exception ex)
            {
                Console.WriteLine(ex.Message);
                return StatusCode(500, "An error occurred while saving the address.");
            }

            return Json(new { redirectUrl = Url.Action("InitiateOrder", "Order", new { id = addressId }) });
        }



    }
}
