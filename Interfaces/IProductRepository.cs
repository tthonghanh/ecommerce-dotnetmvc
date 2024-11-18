using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using ProjectDemo.Dtos.ProductDtos;
using ProjectDemo.Models;
using ProjectDemo.ViewModel;

namespace ProjectDemo.Interfaces
{
    public interface IProductRepository
    {
        Task<List<ProductDto>> GetAllProducts(string category, string ProductSearchName);
        Task<Product> CreateProductAsync(CreateProductViewModel product);
        Task<ProductDto?> GetProductByIdAsync(string id);
        Task<Product?> UpdateProductAsync(string id, UpdateProductViewModel productDto);
        Task<Product?> DeleteProductAsync(string id);
        Task<bool> ProductExist(string id);
    }
}