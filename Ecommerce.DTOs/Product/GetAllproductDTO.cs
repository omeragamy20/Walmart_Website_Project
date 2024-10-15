using Ecommerce.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Ecommerce.DTOs.Product
{
    public class GetAllproductDTO
    {
        public int Id { get; set; }
        public string Title_en { get; set; }
        public string Title_ar { get; set; }
        public string Description_ar { get; set; }
        public string Description_en { get; set; }
        public decimal Price { get; set; }
        public int Stock { get; set; }
        public ICollection<ProductSubCategory>? productSubCategory { get; set; }
    }
    public class GetAllSubCategoryDTOs
    {
        public int? Id { get; set; }
        public string? Name_en { get; set; }
        public string? Name_ar { get; set; }
        public string? CategryName_en { get; set; }
        public string? CategryName_ar { get; set; }
    }
}
