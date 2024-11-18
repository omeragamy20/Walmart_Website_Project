using Ecommerce.DTOs.OrderItemDTOs;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Ecommerce.DTOs.Dashboard
{
    public class TopSellingProductDTO
    {
        public string ProductName { get; set; }
        public decimal price { get; set; }

        public int TotalQuantity { get; set; }
        public decimal TotalCost { get; set; }  
        public string image { get; set; }  
        public List<GetAllOrderItemDTOs> Items { get; set; }


        public TopSellingProductDTO()
        {
            this.TotalCost = price * TotalQuantity;
        }
    }
}
