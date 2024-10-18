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


        public GetAllProductArDTO Product
        {
            get; set;
        }
        public decimal TotalPricee => Product.Price * Quantity;

    }
}

