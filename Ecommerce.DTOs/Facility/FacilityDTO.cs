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
        public int Id { get; set; }

        [ MinLength(20)]
        public string Name_en { get; set; }

        [MinLength(20)]
        public string Name_ar { get; set; }

    }
}
