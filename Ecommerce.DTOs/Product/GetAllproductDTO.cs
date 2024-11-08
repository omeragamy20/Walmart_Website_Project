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
        public List<int>? SubCategoryIds { get; set; }
        public List<string>? SubCategoryNames { get; set; } = new List<string>();
        public List<string>? Facilities { get; set; } = new List<string>();
        public List<string>? Facilities_Ar { get; set; } = new List<string>();
        public List<string>? Values { get; set; } = new List<string>();
        public List<string>? Values_Ar { get; set; } = new List<string>();
        public  double? Rate { get; set; }
        public int? TotalRate { get; set; }
        public List<int>? Rates { get; set; }
        public List<string>? SubCategoryNamesAr { get; set; } = new List<string>();
        public List<string>? ImageUrls { get; set; } = new List<string>();
    }
}
