using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using ProjectDemo.Interfaces;
using ProjectDemo.Models;
using ProjectDemo.ViewModel;

namespace ProjectDemo.Controllers
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
            var categories = new CategoryViewModel
            {
                Categories = await _categoryRepository.GetAllCategoryAsync()
            };

            return View(categories);
        }

        public IActionResult Create()
        {
            return View();
        }

        [HttpPost]
        public async Task<IActionResult> Create(Category categoryModel)
        {
            if (ModelState.IsValid)
            {
                await _categoryRepository.CreateCategoryAsync(categoryModel);
                return RedirectToAction(nameof(Index));
            }

            return View(categoryModel);
        }

        public async Task<IActionResult> Edit(string? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var category = await _categoryRepository.GetCategoryByIdAsync(id);
            if (category == null)
            {
                return NotFound();
            }
            return View(category);
        }

        [HttpPost]
        public async Task<IActionResult> Edit(string id, Category category)
        {
            if (!ModelState.IsValid) return BadRequest(ModelState);
            try
            {
                await _categoryRepository.UpdateCategoryAsync(id, category);
            }
            catch (DbUpdateConcurrencyException)
            {
                if (await _categoryRepository.CategoryExist(id) == false)
                {
                    return NotFound();
                }
                else throw;
            }
            return RedirectToAction(nameof(Index));
        }

        public async Task<IActionResult> Delete(string? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var category = await _categoryRepository.GetCategoryByIdAsync(id);
            if (category == null)
            {
                return NotFound();
            }

            return View(category);
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Delete(string id, bool notUsed)
        {
            var category = await _categoryRepository.DeleteCategoryAsync(id);
            return RedirectToAction(nameof(Index));
        }
    }
}