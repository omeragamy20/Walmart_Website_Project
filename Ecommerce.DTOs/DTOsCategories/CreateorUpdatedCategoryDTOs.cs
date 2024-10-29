using Microsoft.AspNetCore.Http;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Ecommerce.DTOs.DTOsCategories
{
    public class CreateorUpdatedCategoryDTOs
    {

        public int? Id { get; set; } 
        [Required, MaxLength(100)]
        public string Name_en { get; set; }
        [Required, MaxLength(100)]
        public string Name_ar { get; set; }
        public string? Image { get; set; }

        public IFormFile? ImageData { get; set; }
    }
}
