using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using ProjectDemo.Dtos.ProductDtos;
using ProjectDemo.Models;

namespace ProjectDemo.Mappers
{
    public static class ProductMapper
    {
        public static ProductDto ToProductDto(this Product productModel)
        {
            return new ProductDto
            {
                Id = productModel.Id,
                Name = productModel.Name,
                OriginalPrice = productModel.OriginalPrice,
                ActualPrice = productModel.ActualPrice,
                Description = productModel.Description,
                CreateAt = productModel.CreateAt,
                CategoryId = productModel.CategoryId,
                CategoryName = productModel.Category.Name
            };
        }
    }
}