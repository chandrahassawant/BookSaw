using BulkyWeb.Data;
using Microsoft.AspNetCore.Mvc;
using System.Linq;

namespace BulkyWeb.Controllers
{
    public class ClientController : Controller
    {
        private readonly ApplicationDbContext _db;

        // Constructor Injection
        public ClientController(ApplicationDbContext db)
        {
            _db = db;
        }

        public IActionResult Index()
        {
            var objClientList = _db.Clients
                .OrderBy(c => c.Id)
                .ToList();

            if (objClientList == null || !objClientList.Any())
            {
                return View("NoProducts");
            }

            return View(objClientList);
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public IActionResult Delete(int id)
        {
            // Find the client by ID
            var client = _db.Clients.FirstOrDefault(u => u.Id == id);

            if (client == null)
            {
                return Json(new { success = false, message = "Client not found." });
            }

            // Set the IsActive flag to false (inactive)
            client.IsActive = false;
            _db.Clients.Update(client);
            _db.SaveChanges();

            // Return success message
            return Json(new { success = true, message = "Client marked as inactive successfully!" });
        }

         [HttpPost]
        [ValidateAntiForgeryToken]
        public IActionResult Enable(int id)
        {
            // Find the user by ID
            var client = _db.Clients.FirstOrDefault(u => u.Id == id);

            if (client == null)
            {
                return Json(new { success = false, message = "User not found." });
            }

            // Set the IsActive flag to 1 (active)
            client.IsActive = true;
            _db.Clients.Update(client);
            _db.SaveChanges();

            // Return success message
            return Json(new { success = true, message = "User reactivated successfully!" });
        }
    }
}
