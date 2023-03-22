using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Logging;
using Microsoft.VisualStudio.Web.CodeGeneration;
using PRN211_ShoesStore.Models.DTO;
using PRN211_ShoesStore.Models.Entity;
using PRN211_ShoesStore.Repository.vH;
using PRN211_ShoesStore.Repository.vH.Interface;
using PRN211_ShoesStore.Service.vH.Interface;
using System;
using System.Collections.Generic;
using System.Linq;

namespace PRN211_ShoesStore.Controllers.Admin
{
    [Route("Admin/Category")]
    public class CategoryController : Controller
    {
        private readonly ICategoryService _categoryService;
        private readonly ILogger<HomeController> _logger;
        private readonly IRepository<Category> _categoryRepository;

        public CategoryController(ICategoryService categoryService, ILogger<HomeController> logger, IRepository<Category> categoryRepository)
        {
            _categoryService = categoryService;
            _logger = logger;
            _categoryRepository = categoryRepository;
        }

        public IActionResult Index()
        {
            var categoryList = _categoryRepository.GetAll(includeProperties: "CategoryShoes.shoes.SpecificallyShoes.OrderDetails");
            var totalQuantityInEachCategory = categoryList.Select(c => new CategoryViewDto{ Category = c, 
                TotalSell = c.CategoryShoes.Select(cs => cs.shoes).SelectMany(s => s.SpecificallyShoes).SelectMany(ss => ss.OrderDetails).Sum(od => od.quantity), 
                TotalProduct= c.CategoryShoes.Select(cs => cs.shoes).SelectMany(s => s.SpecificallyShoes).Sum(ss=>ss.quantity)}).ToList();
            return View("/ViewsAdmin/Category/Index.cshtml", totalQuantityInEachCategory);
        }

        [HttpPost("Add")]
        [ValidateAntiForgeryToken]
        public IActionResult AddCategory([Bind("name")] Category category)
        {
            if (ModelState.IsValid)
            {
                Category categoryEntity = new()
                {
                    name = category.name,
                    status = true
                };
                if (_categoryService.GetCategoryByName(categoryEntity.name) == null)
                {
                    _categoryService.AddCategory(categoryEntity);
                    TempData["Message"] = "Category added successfully.";
                }
                else
                {
                    TempData["Error"] = $"Category name: {categoryEntity.name} already existed.";
                }
            }
            else
            {
                TempData["Error"] = "An error occurred while adding the category. Please try again later.";
            }
            return RedirectToAction("Index");
        }
        [HttpPost("Edit")]
        [ValidateAntiForgeryToken]
        public IActionResult EditCategory(Category category)
        {
            if (ModelState.IsValid)
            {
                Category categoryEntity = new()
                {
                    name = category.name,
                    status = category.status,
                    id = category.id
                };
                _categoryService.UpdateCategory(categoryEntity);
                TempData["Message"] = $"Category {categoryEntity.name} edited successfully.";
            }
            else
            {
                TempData["Error"] = "An error occurred while editing the category. Please try again later.";
            }
            return RedirectToAction("Index");
        }

        [HttpPost("Delete")]
        [ValidateAntiForgeryToken]
        public IActionResult DeleteCategory(int id)
        {
            Category category = _categoryService.GetCategoryByCategoryId(id);
            try
            {
                _categoryService.RemoveCategory(category);
                TempData["Message"] = $"Category {category.name} deleted successfully.";
            }
            catch (Exception ex)
            {
                TempData["Message"] = "Cannot delete this category";
            }
            return RedirectToAction("Index");
        }
    }
}
