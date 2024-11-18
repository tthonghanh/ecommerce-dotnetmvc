using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Query.SqlExpressions;
using ProjectDemo.Data;
using ProjectDemo.Dtos.ProductDtos;
using ProjectDemo.Interfaces;
using ProjectDemo.Mappers;
using ProjectDemo.Models;
using ProjectDemo.ViewModel;

namespace ProjectDemo.Repositories
{
    public class ProductRepository : IProductRepository
    {
        private readonly ApplicationDbContext _context;
        public ProductRepository(ApplicationDbContext context) {
            _context = context;
        }

        public async Task<Product> CreateProductAsync(CreateProductViewModel productViewModel)
        {
            var product = new Product {
                Name = productViewModel.Name,
                OriginalPrice = productViewModel.OriginalPrice,
                ActualPrice = productViewModel.ActualPrice,
                CategoryId = productViewModel.CategoryId,
                Description = productViewModel.Description
            };
            await _context.Products.AddAsync(product);
            await _context.SaveChangesAsync();
            return product;
        }

        public async Task<Product?> DeleteProductAsync(string id)
        {
            var productModel = await _context.Products.FirstOrDefaultAsync(product => product.Id == id);

            if (productModel == null) return null;

            _context.Products.Remove(productModel);
            await _context.SaveChangesAsync();

            return productModel;
        }

        public async Task<List<ProductDto>> GetAllProducts(string categoryId, string ProductSearchName)
        {
            var products = _context.Products
                                    .Include(p => p.Category)
                                    .AsQueryable();

            if (!string.IsNullOrEmpty(categoryId)) {
                products = products.Where(p => p.CategoryId == categoryId);
            }

            if (!string.IsNullOrEmpty(ProductSearchName)) {
                products = products.Where(s => s.Name.ToLower().Contains(ProductSearchName.ToLower()));
            }

            return await products.Select(products => products.ToProductDto()).ToListAsync();
        }

        public async Task<ProductDto?> GetProductByIdAsync(string id)
        {
            var product = await _context.Products
                                        .Include(p => p.Category)
                                        .FirstOrDefaultAsync(product => product.Id == id);

            if (product == null) return null;

            return product.ToProductDto();
        }

        public async Task<bool> ProductExist(string id)
        {
            return await _context.Products.AnyAsync(p => p.Id == id);
        }

        public async Task<Product?> UpdateProductAsync(string id, UpdateProductViewModel product)
        {
            var existingProduct = await _context.Products.FirstOrDefaultAsync(product => product.Id == id);
            if (existingProduct == null) return null;

            existingProduct.Name = product.Name;
            existingProduct.OriginalPrice = product.OriginalPrice;
            existingProduct.ActualPrice = product.ActualPrice;
            existingProduct.CategoryId = product.CategoryId;
            existingProduct.Description = product.Description;

            await _context.SaveChangesAsync();
            return existingProduct;
        }
    }
}