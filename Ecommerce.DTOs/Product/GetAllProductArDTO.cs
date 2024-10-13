using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Ecommerce.DTOs.Product
{
    public class GetAllProductArDTO
    {
        public int Id { get; set; }
        public string Title_ar { get; set; }
        public string Description_ar { get; set; }
        public decimal Price { get; set; }
        public int Stock { get; set; }
    }
}
