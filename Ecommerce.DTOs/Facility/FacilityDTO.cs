using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.ComponentModel.DataAnnotations;
namespace Ecommerce.DTOs.Facility
{
    public class FacilityDTO
    {
        public int? Id { get; set; }

        [ MinLength(3)]
        public string Name_en { get; set; }

        [MinLength(3)]
        public string Name_ar { get; set; }
        public List<string>? Values_En { get; set; } = new List<string>();
        public List<string>? Values_Ar { get; set; } = new List<string>();

        public List<int>? SubCategoryIds { get; set; }
        public List<string>? SubCategoryNames { get; set; } = new List<string>();
        public List<string>? SubCategoryNamesAr { get; set; } = new List<string>();

    }
}
