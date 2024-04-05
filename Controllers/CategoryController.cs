using FinalProject.Interface;
using FinalProject.Models;
using FinalProject.ViewModels;
using Microsoft.AspNetCore.Mvc;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace FinalProject.Controllers
{
    public class CategoryController : Controller
    {
        private readonly ICategoryRepository _categoryRepository;

        public CategoryController(ICategoryRepository categoryRepository)
        {
            _categoryRepository = categoryRepository;
        }

        public async Task<IActionResult> Index()
        {
            IEnumerable<Category> categories = await _categoryRepository.GetAll();
            return View(categories);
        }

        public async Task<IActionResult> Detail(int id)
        {
            Category category = await _categoryRepository.GetByIdAsync(id);
            if (category == null)
            {
                return NotFound();
            }
            return View(category);
        }

        [HttpGet]
        public IActionResult Create()
        {
            var createCategoryViewModel = new CategoryCreateEditViewModel();
            return View("CreateEdit", createCategoryViewModel);
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Create(CategoryCreateEditViewModel model)
        {
            if (ModelState.IsValid)
            {
                var category = new Category
                {
                    Name = model.Name
                };
                _categoryRepository.Add(category);
                return RedirectToAction("Index");
            }
            else
            {
                ModelState.AddModelError("", "Failed to add category.");
            }

            return View(model);
        }

        [HttpGet]
        public async Task<IActionResult> Edit(int id)
        {
            var category = await _categoryRepository.GetByIdAsync(id);
            if (category == null)
            {
                return NotFound();
            }

            var editCategoryViewModel = new CategoryCreateEditViewModel
            {
                CategoryId = category.CategoryId,
                Name = category.Name
            };

            return View("CreateEdit", editCategoryViewModel);
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Edit(int id, CategoryCreateEditViewModel model)
        {
            if (id != model.CategoryId)
            {
                return BadRequest();
            }

            if (ModelState.IsValid)
            {
                var category = await _categoryRepository.GetByIdAsync(id);
                if (category == null)
                {
                    return NotFound();
                }

                category.Name = model.Name;

                _categoryRepository.Update(category);
                return RedirectToAction("Index");
            }

            return View(model);
        }

        public async Task<IActionResult> Delete(int id)
        {
            var categoryDetails = await _categoryRepository.GetByIdAsync(id);
            if (categoryDetails == null) return View("Error");
            return View(categoryDetails);
        }

        [HttpPost, ActionName("Delete")]
        public async Task<IActionResult> DeleteCategory(int id)
        {
            var categoryDetails = await _categoryRepository.GetByIdAsync(id);
            if (categoryDetails == null) return View("Error");

            _categoryRepository.Delete(categoryDetails);
            return RedirectToAction("Index");
        }
    }
}
