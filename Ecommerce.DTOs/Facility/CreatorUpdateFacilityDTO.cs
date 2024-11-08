using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Ecommerce.DTOs.Facility
{
    public class CreatorUpdateFacilityDTO 
    {
        public int Id { get; set; }

        [Required, MaxLength(100)]
        public string Name_en { get; set; }

        [Required, MaxLength(100)]
        public string Name_ar { get; set; }

        public List<int>? subcategoriesId { get; set; }

    }


}
