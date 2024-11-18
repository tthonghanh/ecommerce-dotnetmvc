using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using Microsoft.EntityFrameworkCore;
using ProjectDemo.Data;
using ProjectDemo.Interfaces;
using ProjectDemo.Models;
using ProjectDemo.ViewModel;

namespace ProjectDemo.Controllers
{
    public class ProductController : Controller
    {
        private readonly IProductRepository _productRepository;
        private readonly ICategoryRepository _categoryRepository;
        public ProductController(IProductRepository productRepository, ICategoryRepository categoryRepository)
        {
            _productRepository = productRepository;
            _categoryRepository = categoryRepository;
        }
        public async Task<IActionResult> Index(string categoryId, string ProductSearchName) {
            var products = await _productRepository.GetAllProducts(categoryId, ProductSearchName);
            var categories = await _categoryRepository.GetAllCategoryAsync();

            var productCategoryVM = new ProductCategoryViewModel {
                ProductList = products,
                CategoryList = new SelectList(categories, "Id", "Name")
            };

            return View(productCategoryVM);
        }

        public async Task<IActionResult> Details(string? id)
        {
            if (id == null) {
                return NotFound();
            }

            var product = await _productRepository.GetProductByIdAsync(id);
            if (product == null) {
                return NotFound();
            }

            return View(product);
        }

        public async Task<IActionResult> Create() {
            var categories = await _categoryRepository.GetAllCategoryAsync();
            var createProductVM = new CreateProductViewModel {
                CategoryList = new SelectList(categories, "Id", "Name")
            };
            return View(createProductVM);
        }

        [HttpPost]
        public async Task<IActionResult> Create(CreateProductViewModel productModel) {
            if (ModelState.IsValid) {
                await _productRepository.CreateProductAsync(productModel);
                return RedirectToAction(nameof(Index));
            }

            return View(productModel);
        }

        public async Task<IActionResult> Edit(string? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var product = await _productRepository.GetProductByIdAsync(id);

            if (product == null){
                return NotFound();
            }

            var categories = await _categoryRepository.GetAllCategoryAsync();

            var updateProductVM = new UpdateProductViewModel {
                Id = id,
                Name = product.Name,
                OriginalPrice = product.OriginalPrice,
                ActualPrice = product.ActualPrice,
                CategoryId = product.CategoryId,
                Description = product.Description,
                CategoryList = new SelectList(categories, "Id", "Name")
            };

            return View(updateProductVM);
        }

        [HttpPost]
        public async Task<IActionResult> Edit(string id, UpdateProductViewModel updateProduct) {
            if (id != updateProduct.Id) return NotFound();

            if (!ModelState.IsValid) return BadRequest(ModelState);

            try
            {
                await _productRepository.UpdateProductAsync(id, updateProduct);
            }
            catch (DbUpdateConcurrencyException)
            {
                if (await _productRepository.ProductExist(id) == false) {
                    return NotFound();
                } else throw;
            }
            return RedirectToAction(nameof(Index));
        }

        public async Task<IActionResult> Delete(string? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var product =  await _productRepository.GetProductByIdAsync(id);
            if (product == null)
            {
                return NotFound();
            }

            return View(product);
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Delete(string id, bool notUsed)
        {
            var product = await _productRepository.DeleteProductAsync(id);
            return RedirectToAction(nameof(Index));
        }
    }
}