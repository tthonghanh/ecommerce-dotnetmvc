using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.EntityFrameworkCore;
using ProjectDemo.Data;
using ProjectDemo.Interfaces;
using ProjectDemo.Models;

namespace ProjectDemo.Repositories
{
    public class CategoryRepository : ICategoryRepository
    {
        private readonly ApplicationDbContext _context;
        public CategoryRepository(ApplicationDbContext context) {
            _context = context;
        }

        public async Task<bool> CategoryExist(string id)
        {
            return await _context.Categories.AnyAsync(category => category.Id == id);
        }

        public async Task<Category> CreateCategoryAsync(Category categoryModel)
        {
            await _context.Categories.AddAsync(categoryModel);
            await _context.SaveChangesAsync();
            return categoryModel;
        }

        public async Task<Category?> DeleteCategoryAsync(string id)
        {
            var categoryModel = await _context.Categories.FirstOrDefaultAsync(c => c.Id == id);

            if (categoryModel == null) return null;

            _context.Categories.Remove(categoryModel);
            await _context.SaveChangesAsync();

            return categoryModel;
        }

        public async Task<List<Category>> GetAllCategoryAsync()
        {
            return await _context.Categories.ToListAsync();
        }

        public async Task<Category?> GetCategoryByIdAsync(string id)
        {
            return await _context.Categories.FirstOrDefaultAsync(c => c.Id == id);
        }

        public async Task<Category?> UpdateCategoryAsync(string id, Category category)
        {
            var existingCategory = await _context.Categories.FirstOrDefaultAsync(c => c.Id == id);
            if (existingCategory == null) return null;

            existingCategory.Name = category.Name;

            await _context.SaveChangesAsync();
            return existingCategory;
        }
    }
}