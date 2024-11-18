using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Threading.Tasks;

namespace ProjectDemo.Dtos.ProductDtos
{
    public class ProductDto
    {
        public string Id { get; set; } = null!;
        public string Name { get; set; } = null!;
        [Display(Name = "Original Price"), Column(TypeName = "decimal(18, 2)"), DataType(DataType.Currency)]
        public decimal OriginalPrice { get; set; }
        [Display(Name = "Actual Price"), Column(TypeName = "decimal(18, 2)"), DataType(DataType.Currency)]
        public decimal ActualPrice { get; set; }
        public string Description { get; set; } = null!;
        [Display(Name = "Created Date"), DataType(DataType.Date)]
        public DateTime CreateAt { get; set; }
        public string? CategoryId { get; set; }
        [Display(Name = "Category Name")]
        public string? CategoryName { get; set; }
    }
}