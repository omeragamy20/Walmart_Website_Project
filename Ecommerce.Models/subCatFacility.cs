using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Ecommerce.Models
{
    public class subCatFacility : BaseEntity<int>
    {

        [ForeignKey("facility")]
        public int facilityId { get; set; }
        public Facility facility {  get; set; }

        [ForeignKey("subCategory")]
        public int SubCategoryID { get; set; }

        public SubCategory subCategory { get; set; }
    }
}
