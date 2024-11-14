using Ecommerce.Models;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Ecommerce.DTOs.OrderDTOs
{
   public  class GetCustomerOrders
    {
        public int Id { get; set; }
        public string? OrderDate { get; set; }

        [Range(50, double.MaxValue)]
        public decimal TotalPrice { get; set; }

        [Range(0, 6)]
        public int Status { get; set; } = 0;

        public int? PaymentId { get; set; }
        public string Payment_ar { get; set; }
        public string Payment_en { get; set; }

        
        public int? ShipmentId { get; set; }
        public string? ShipmentAddress { get; set; }

        public ICollection<int>? OrderItems { get; set; }
        public ICollection<int>? OrderItemsQuantity { get; set; }


    }
}
