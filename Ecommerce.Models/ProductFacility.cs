using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Ecommerce.Models
{
    public class ProductFacility : BaseEntity<int>
    {

        [Required, MaxLength(100)]
        public string Value_en { get; set; }

        [Required, MaxLength(100)]
        public string Value_ar { get; set; }
        public string ? Details { get; set; }
        public string ? Details_ar { get; set; }

        [ForeignKey("product")]
        public int ProductID { get; set; }
        public Product product { get; set; }

        [ForeignKey("facility")]
        public int?  facilityID { get; set; }
        public Facility?  facility { get; set; }
    }
}
