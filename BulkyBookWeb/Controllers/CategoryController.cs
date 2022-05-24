using BulkyBook.DataAccess;
using BulkyBook.Models;
using Microsoft.AspNetCore.Mvc;

namespace BulkyBookWeb.Controllers
{
    public class CategoryController : Controller
    {
        private readonly ApplicationDbContext applicationDbContext;

        public CategoryController(ApplicationDbContext _applicationDbContext)
        {
            applicationDbContext = _applicationDbContext;
        }
        public IActionResult Index()
        {
            IEnumerable<Category> objList = applicationDbContext.categories;
            return View(objList);
        }
        public IActionResult Create()
        {
            return View();
        }
        [HttpPost]
        [ValidateAntiForgeryToken]
        public IActionResult Create(Category newCategory)
        {
            if(ModelState.IsValid)
            {
                applicationDbContext.categories.Add(newCategory);
                applicationDbContext.SaveChanges();
                TempData["success"] = "Catergory Create Successfully";
                return RedirectToAction("Index");
            }
            return View(newCategory);
        }
        [HttpGet]
        public IActionResult Edit(int? id)
        {
            if(id == null)
            {
                return NotFound();
            }
            var category = applicationDbContext.categories.Find(id);
            return View(category);
        }

        [HttpPost]
        public IActionResult Edit(Category obj)
        {
            if(ModelState.IsValid)
            {
                applicationDbContext.categories.Update(obj);
                applicationDbContext.SaveChanges();
                TempData["success"] = "Catergory Edited Successfully";
                return RedirectToAction("Index");
            }
            return View(obj);
        }

        [HttpGet]
        public IActionResult Delete(int? id)
        {
            if(id==null)
            {
                return NotFound();
            }
            var category = applicationDbContext.categories.Find(id);
            return View(category);
        }
        
        [HttpPost]
        public IActionResult Delete(Category obj)
        {
            if(obj==null)
            {
                return BadRequest();
            }
            var removeCatergory = applicationDbContext.categories.FirstOrDefault( x=> x.Id == obj.Id);
            if(removeCatergory!=null)
            {
                applicationDbContext.categories.Remove(removeCatergory);
                applicationDbContext.SaveChanges();
                TempData["success"] = "Category Deleted Successfully";
                return RedirectToAction("Index");
            }
            return View(obj);
        }
    }
}
