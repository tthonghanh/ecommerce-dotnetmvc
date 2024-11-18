using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.EntityFrameworkCore;

namespace ProjectDemo.Models
{
    public class Product
    {
        public string Id { get; set; } = Guid.NewGuid().ToString();
        public string Name { get; set; } = null!;
        [Display(Name = "Original Price"), Column(TypeName = "decimal(18, 2)"), DataType(DataType.Currency)]
        public decimal OriginalPrice { get; set; }
        [Display(Name = "Actual Price"), Column(TypeName = "decimal(18, 2)"), DataType(DataType.Currency)]
        public decimal ActualPrice { get; set; }
        public string Description { get; set; } = null!;
        [Display(Name = "Created Date"), DataType(DataType.Date)]
        public DateTime CreateAt { get; set; } = DateTime.Now;
        public string? CategoryId { get; set; }
        [DeleteBehavior(DeleteBehavior.Cascade)]
        public Category Category { get; set; } = null!;
    }
}