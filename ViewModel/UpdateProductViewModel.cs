using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc.Rendering;

namespace ProjectDemo.ViewModel
{
    public class UpdateProductViewModel
    {
        public string Id { get; set; } = null!;
        public string Name { get; set; } = null!;
        [Display(Name = "Original Price"), Column(TypeName = "decimal(18, 2)")]
        public decimal OriginalPrice { get; set; }
        [Display(Name = "Actual Price"), Column(TypeName = "decimal(18, 2)")]
        public decimal ActualPrice { get; set; }
        public string Description { get; set; } = null!;
        public string? CategoryId { get; set; }
        public SelectList? CategoryList { get; set; }
    }
}