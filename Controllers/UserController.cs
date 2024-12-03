using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Identity;
using System.IO;
using System;
using System.Linq;
using System.Threading.Tasks;
using BulkyWeb.Models;
using BulkyWeb.Data;
using Microsoft.EntityFrameworkCore;


namespace BulkyWeb.Controllers
{
    public class UserController : Controller
    {
        private readonly UserManager<User> _userManager;
        private readonly ApplicationDbContext _context;

        public UserController(UserManager<User> userManager, ApplicationDbContext context)
        {
            _userManager = userManager;
            _context = context;
        }

        // Get current user's profile
        public async Task<IActionResult> Profile()
        {
            // Get the currently logged-in user
            var user = await _userManager.GetUserAsync(User);
            if (user == null)
            {
                return RedirectToAction("Login", "Account"); // Redirect to login if user is not authenticated
            }

            // Pass the avatar status (whether the user already has an avatar) to the view
            ViewBag.HasAvatar = !string.IsNullOrEmpty(user.Avatar);
            return View(user);
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> UploadAvatar(IFormFile avatarFile)
        {
            if (avatarFile != null && avatarFile.Length > 0)
            {
                // Validate file size (max 5 MB)
                if (avatarFile.Length > 5 * 1024 * 1024)
                {
                    TempData["Error"] = "File size cannot exceed 5 MB.";
                    return RedirectToAction("Profile");
                }

                // Validate file type
                var allowedExtensions = new[] { ".jpg", ".jpeg", ".png" };
                var fileExtension = Path.GetExtension(avatarFile.FileName).ToLower();
                if (!allowedExtensions.Contains(fileExtension))
                {
                    TempData["Error"] = "Only JPG and PNG files are allowed.";
                    return RedirectToAction("Profile");
                }

                // Generate a unique file name and save the file
                var fileName = $"{Guid.NewGuid()}{fileExtension}";
                var uploadDirectory = Path.Combine(Directory.GetCurrentDirectory(), "wwwroot/images/avatars");

                // Ensure the directory exists
                if (!Directory.Exists(uploadDirectory))
                {
                    Directory.CreateDirectory(uploadDirectory);
                }

                var uploadPath = Path.Combine(uploadDirectory, fileName);
                using (var stream = new FileStream(uploadPath, FileMode.Create))
                {
                    await avatarFile.CopyToAsync(stream);
                }

                // Update the user's Avatar property
                var user = await _userManager.GetUserAsync(User);
                if (user == null)
                {
                    return RedirectToAction("Login", "Account");
                }

                user.Avatar = $"/images/avatars/{fileName}";
                var result = await _userManager.UpdateAsync(user);
                if (result.Succeeded)
                {
                    TempData["Success"] = "Profile picture updated successfully!";
                }
                else
                {
                    TempData["Error"] = "An error occurred while updating your profile picture.";
                }
            }
            else
            {
                TempData["Error"] = "Please select a valid file to upload.";
            }

            return RedirectToAction("Profile");
        }

        // POST: Save changes to user's profile
        [HttpPost]
        public async Task<IActionResult> SaveChanges(User model)
        {
            if (!ModelState.IsValid)
            {
                var user = await _userManager.GetUserAsync(User);
                if (user == null)
                {
                    return RedirectToAction("Login", "Account");
                }

                user.FirstName = model.FirstName;
                user.LastName = model.LastName;
                user.Email = model.Email;
                user.ContactNumber = model.ContactNumber;
                user.Address = model.Address;
                user.City = model.City;
                user.State = model.State;
                user.Country = model.Country;
                user.PostalCode = model.PostalCode;

                var result = await _userManager.UpdateAsync(user);

                if (result.Succeeded)
                {
                    TempData["Success"] = "Profile updated successfully!";
                }
                else
                {
                    TempData["Error"] = "Failed to update profile.";
                }

                return RedirectToAction("Profile");
            }

            TempData["Error"] = "Please correct the errors below.";
            return View("Profile", model);
        }

        //--------------------------------------------------------------

        // GET: User
        public async Task<IActionResult> GetAllUsers()
        {
            // Fetch all active users from the database
            var users = await _context.Users.Where(u => u.IsActive).ToListAsync();
            return View(users);
        }


        //----------------------------------------------------------

        // DELETE: /User/Delete/5
        [HttpPost]
        [ValidateAntiForgeryToken]
        public IActionResult Delete(string id)
        {
            // Find the user by ID
            var user = _context.Users.FirstOrDefault(u => u.Id == id);

            if (user == null)
            {
                return Json(new { success = false, message = "User not found." });
            }

            // Set the IsActive flag to 0 (inactive)
            user.IsActive = false;
            _context.Users.Update(user);
            _context.SaveChanges();

            // Return success message
            return Json(new { success = true, message = "User marked as inactive successfully!" });
        }


        [HttpPost]
        [ValidateAntiForgeryToken]
        public IActionResult Enable(string id)
        {
            // Find the user by ID
            var user = _context.Users.FirstOrDefault(u => u.Id == id);

            if (user == null)
            {
                return Json(new { success = false, message = "User not found." });
            }

            // Set the IsActive flag to 1 (active)
            user.IsActive = true;
            _context.Users.Update(user);
            _context.SaveChanges();

            // Return success message
            return Json(new { success = true, message = "User reactivated successfully!" });
        }



    }

}
