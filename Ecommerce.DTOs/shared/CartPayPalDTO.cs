using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Ecommerce.DTOs.shared
{
    public class CartPayPalDTO
    {
        public int? id { get; set; }
        public string? name { get; set; }
        public int? quantity { get; set; }
        public int? amount { get; set; }

    }
}
