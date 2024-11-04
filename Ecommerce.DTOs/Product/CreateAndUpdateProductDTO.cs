using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations.Schema;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Ecommerce.Models;
using Microsoft.AspNetCore.Http;

namespace Ecommerce.DTOs.Product
{
    public class CreateAndUpdateProductDTO
    {
        public int Id { get; set; }
        [MinLength(3)]
        public string Title_en { get; set; }
        [MinLength(3)]
        public string Title_ar { get; set; }

        [MinLength(10)]
        public string Description_en { get; set; }
        [MinLength(10)]
        public string Description_ar { get; set; }
        [Range(100,1000000)]
        public decimal Price { get; set; }
        public int Stock { get; set; }
        //public IFormFile? ImageFile { get; set; }
        //public ICollection<Images>? Images { get; set; }
        public List<string>? Facilities { get; set; } = new List<string>();
        public List<string>? Facilities_Ar { get; set; } = new List<string>();
        
        public List<int>? SubCategoryIds { get; set; }
        public List<string>? ImagesUrl { get; set; }
        public List<IFormFile>? ImagesFromFile { get; set; } = new List<IFormFile>();
        public ICollection<ProductSubCategory>? productSubCategory { get; set; }

    }
}
