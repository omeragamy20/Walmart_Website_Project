using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Ecommerce.Models
{
    public class Facility: BaseEntity<int>
    {
        
        [Required,MaxLength(100)]
        public string Name_en { get; set; }
        
        [Required,MaxLength(100)]
        public string Name_ar { get; set; }


        public ICollection<ProductFacility>? ProductFacilities { get; set; }
        public ICollection<subCatFacility>? subCatFacility { get; set; }
        //[ForeignKey("product")]
        //public int? ProductId { get; set; } 

        //public Product? product { get; set; }
    }
}
