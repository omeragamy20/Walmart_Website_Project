using Ecommerce.DTOs.Facility;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Ecommerce.DTOs.Product
{
    public class PrdFacilitySubCategory
    {
       public int ProductId { get; set; }
       public List<int>? SubcatsIDS { get; set; } = new List<int>();
       public List<CreatorUpdateFacilityDTO>? FacilityDTO { get; set; }
        public List<int>? FacilityIds { get; set; }
        public List<string>? Values_En { get; set; } = new List<string>();
        public List<string>? Values_Ar { get; set; } = new List<string>();    

    }
}
