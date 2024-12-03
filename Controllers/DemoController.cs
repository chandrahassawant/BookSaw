using Microsoft.AspNetCore.Mvc;
using BulkyWeb.Data; // Ensure this namespace is included
using BulkyWeb.Models; // Ensure the Demo model is correctly referenced
using Microsoft.EntityFrameworkCore;

namespace BulkyWeb.Controllers
{
    public class DemoController : Controller
    {
        private readonly ApplicationDbContext _context;

        // Constructor to inject ApplicationDbContext
        public DemoController(ApplicationDbContext context)
        {
            _context = context;
        }

        // GET: Demo/Index
        public IActionResult Index()
        {
            return View();
        }

        // POST: Demo/Index
        [HttpPost]
        public ActionResult Index(Demo obj)
        {
            if (ModelState.IsValid)
            {
                _context.Demos.Add(obj); // Use the Demos DbSet from ApplicationDbContext
                _context.SaveChanges(); // Save the changes to the database
                return RedirectToAction("Index");
            }

            return View(obj);
        }
    }
}
