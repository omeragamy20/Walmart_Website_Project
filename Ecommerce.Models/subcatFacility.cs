using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.ComponentModel.DataAnnotations.Schema;
namespace Ecommerce.Models
{
    public class subCatFacility: BaseEntity<int>
    {
        [ForeignKey("SubCategory")]
        public int? SubcategoryId { get; set; }
        public SubCategory? SubCategory { get; set; }

        [ForeignKey("Facility")]
        public int? FacilityId { get; set; }
        public Facility? Facility { get; set; }
    }
}
