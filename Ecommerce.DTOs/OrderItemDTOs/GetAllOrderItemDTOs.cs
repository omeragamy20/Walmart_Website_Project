using Ecommerce.DTOs.Product;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Ecommerce.DTOs.OrderItemDTOs
{
    public class GetAllOrderItemDTOs
    {

        public int Id { get; set; }

        [Range(1, double.MaxValue)]
        public int Quantity { get; set; }

        [Range(50, double.MaxValue)]
        public decimal Price { get; set; }

        public decimal TotalPrice { get; set; }


        public string PrdName { get; set; }
        public string PrdDesc { get; set; }
        public decimal PrdPrice { get; set; }
        public List<string> PrdImages{ get; set; }
        public string PrdImage { get; set; }

        public decimal TotalPricee => PrdPrice * Quantity;

    }
}

