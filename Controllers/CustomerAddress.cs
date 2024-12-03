using System;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using BulkyWeb.Models;
using Microsoft.EntityFrameworkCore;
using BulkyWeb.Data;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Identity;

namespace BulkyWeb.Controllers
{
    [Authorize]
    [Route("[controller]/[action]")]
    public class CustomerAddressController : Controller
    {
        private readonly ApplicationDbContext _context;
        private readonly UserManager<User> _userManager;

        public CustomerAddressController(UserManager<User> userManager, ApplicationDbContext context)
        {
            _userManager = userManager;
            _context = context;
        }

        // GET: CustomerAddress/Index
       public async Task<IActionResult> Index()
        {
            // Get the currently logged-in user's ID
            var userId = _userManager.GetUserId(User);

            if (userId == null)
            {
                return Unauthorized(); // Return unauthorized if user ID is not found
            }

            // Fetch addresses associated with the user
            var customerAddresses = await _context.CustomerAddresses
                .Where(address => address.UserId == userId)
                .ToListAsync();

            return View(customerAddresses);
        }

        // GET: CustomerAddress/Create
       public async Task<IActionResult> Create()
{
    var user = await _userManager.GetUserAsync(User);

    if (user == null)
    {
        // Redirect to login if the user is not authenticated
        return RedirectToAction("Login", "Account");
    }

    var customerAddress = new CustomerAddress
    {
        CustomerId = user.Id,
        UserId = user.Id,  // Set the UserId property
        User = user  // Set the User navigation property
    };

    return View(customerAddress);
}


        [HttpPost]
[ValidateAntiForgeryToken]
public async Task<IActionResult> Create([Bind("CustomerId,Name,StreetAddress,City,State,PostalCode,Country,PhoneNumber,UserId")] CustomerAddress customerAddress)
{
    var user = await _userManager.GetUserAsync(User);
    if (user == null)
    {
        return RedirectToAction("Login", "Account");
    }

    customerAddress.User = user;  // Explicitly set the User property

    if (!ModelState.IsValid)
    {
        _context.Add(customerAddress);
        await _context.SaveChangesAsync();
        return RedirectToAction("Index");
    }

    // Log ModelState errors
    var errors = ModelState.Values.SelectMany(v => v.Errors);
    foreach (var error in errors)
    {
        Console.WriteLine(error.ErrorMessage);
    }

    return View(customerAddress);
}



    }
}