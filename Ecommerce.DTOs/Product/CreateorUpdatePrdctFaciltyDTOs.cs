using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations.Schema;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Ecommerce.DTOs.Product
{
    public class CreateorUpdatePrdctFaciltyDTOs
    {
        public int Id { get; set; }
        [Required, MaxLength(100)]
        public string Value_en { get; set; }

        [Required, MaxLength(100)]

        public string Value_ar { get; set; }
        [ForeignKey("product")]
        public int ProductID { get; set; }

        [ForeignKey("facility")]
        public int? facilityID { get; set; }

    }
}
