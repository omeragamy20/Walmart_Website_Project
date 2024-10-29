using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Ecommerce.DTOs.DTOsCategories
{
    public class GetAllSubCategoryDTOs
    {
        public int? Id { get; set; }
        public string? Name_en { get; set; }
        public string? Name_ar { get; set; }
        public string? CategryName_en { get; set; }
        public string? CategryName_ar { get; set; }
        public int? CategoryId { get; set; }
        public string? Image { get; set; }


    }
}
