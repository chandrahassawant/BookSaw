using BulkyWeb.Data;
using BulkyWeb.Models;
using Microsoft.AspNetCore.Mvc;

namespace BulkyWeb.Controllers
{
    public class CategoryController : Controller
    {
        private readonly ApplicationDbContext _db;
        public CategoryController(ApplicationDbContext db) 
        {
            _db = db;
        }
        public IActionResult Index()
        {
            List<Category> objCategoryList = _db.Categories
        .OrderBy(c => c.DisplayOrder)
        .ToList();

            return View(objCategoryList);
        }

        public IActionResult Create()
        {
            return View();
        }

        [HttpPost]
        public IActionResult Create(Category obj)
        {
            if (ModelState.IsValid)
            {
                _db.Categories.Add(obj);
                _db.SaveChanges();
                TempData["success"] = "Category created successfully!";

                return RedirectToAction("Index");
            }
            else
            {
                TempData["ErrorMessage"] = "Failed to create category. Please check the form and try again.";
                return View(obj);
            }
            
        }

        public IActionResult Edit(int id)
        {
            
            var category = _db.Categories.FirstOrDefault(c => c.CategoryId == id);
            if (category == null) return NotFound();
            return View(category);
        }

        [HttpPost]
        public IActionResult Edit(Category obj)
        {
            if (ModelState.IsValid)
            {
                _db.Categories.Update(obj);
                _db.SaveChanges();
                TempData["success"] = "Category Updated Sucessfully";
                return RedirectToAction("Index");
            }
           else
            {
                return View(obj);
            }
        }

        [HttpPost]
        public IActionResult DeleteSelected(List<int> selectedIds)
        {
            if (selectedIds != null && selectedIds.Count > 0)
            {
                var categoriesToDelete = _db.Categories.Where(c => selectedIds.Contains(c.CategoryId)).ToList();
                _db.Categories.RemoveRange(categoriesToDelete);
                _db.SaveChanges();
                TempData["SuccessMessage"] = "Selected categories deleted successfully!";
            }

            return RedirectToAction("Index");
        }

    }
}
