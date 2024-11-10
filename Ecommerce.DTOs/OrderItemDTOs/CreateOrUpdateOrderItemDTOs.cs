using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Ecommerce.DTOs.OrderItemDTOs
{
    public class CreateOrUpdateOrderItemDTOs
    {
        public int Id { get; set; } = 0;

        [Range(1, double.MaxValue)]
        public int Quantity { get; set; }

        [Range(50,double.MaxValue)]
        public decimal Price { get; set; }

        public int? ProductId { get; set; }

        public int? OrderId { get; set; }
    }
}
