using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations.Schema;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Ecommerce.DTOs.Product
{
    public class CreateAndUpdateProductDTO
    {
        public int Id { get; set; }
        [MinLength(10)]
        public string Title_en { get; set; }
        [MinLength(10)]
        public string Title_ar { get; set; }

        [MinLength(20)]
        public string Description_en { get; set; }
        [MinLength(20)]
        public string Description_ar { get; set; }
        [Range(10,50)]
        public decimal Price { get; set; }
        public int Stock { get; set; }


    }
}
