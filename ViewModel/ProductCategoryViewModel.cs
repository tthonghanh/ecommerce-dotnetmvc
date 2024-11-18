using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc.Rendering;
using ProjectDemo.Dtos.ProductDtos;

namespace ProjectDemo.ViewModel
{
    public class ProductCategoryViewModel
    {
        public List<ProductDto>? ProductList { get; set; } = new List<ProductDto>();
        public SelectList? CategoryList { get; set; }
        public string? CategoryId { get; set; }
        public string? ProductSearchName { get; set; }
    }
}