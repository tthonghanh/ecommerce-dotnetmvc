using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using ProjectDemo.Models;

namespace ProjectDemo.Interfaces
{
    public interface ICategoryRepository
    {
        Task<List<Category>> GetAllCategoryAsync();
        Task<Category> CreateCategoryAsync(Category categoryModel);
        Task<Category?> GetCategoryByIdAsync(string id);
        Task<Category?> UpdateCategoryAsync(string id, Category category);
        Task<Category?> DeleteCategoryAsync(string id);
        Task<bool> CategoryExist(string id);
    }
}