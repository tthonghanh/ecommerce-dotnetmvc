using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc.Rendering;
using ProjectDemo.Models;

namespace ProjectDemo.ViewModel
{
    public class CreateProductViewModel
    {
        public string Name { get; set; } = null!;
        [Display(Name = "Original Price"), Column(TypeName = "decimal(18, 2)")]
        public decimal OriginalPrice { get; set; }
        [Display(Name = "Actual Price"), Column(TypeName = "decimal(18, 2)")]
        public decimal ActualPrice { get; set; }
        public string Description { get; set; } = null!;
        [Display(Name = "Created Date")]
        public DateTime CreateAt { get; set; } = DateTime.Now;
        public string? CategoryId { get; set; }
        public SelectList? CategoryList { get; set; }
    }
}