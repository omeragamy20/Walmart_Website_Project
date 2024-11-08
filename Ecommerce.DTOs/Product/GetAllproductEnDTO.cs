using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Ecommerce.DTOs.Product
{
    public class GetAllproductEnDTO
    {
        public int? Id { get; set; }
        public string? Title_en { get; set; }
        public string? Description_en { get; set; }
        public decimal? Price { get; set; }
        public int? Stock { get; set; }
        public string? ImageUrls { get; set; }

    }
}
