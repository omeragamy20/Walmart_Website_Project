using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Ecommerce.DTOs.OrderDTOs
{
    public class GetAllOrderDTOs
    {
        public int Id { get; set; }
        public DateTime OrderDate { get; set; }

        [Range(50, double.MaxValue)]
        public decimal TotalPrice { get; set; }
        //???

        [Range(0, 6)]
        public int Status { get; set; } = 0;


        public string? CustomerId { get; set; }
        public string? CustomerName { get; set;}
        public string? CustomerEmail { get; set;}
        public int? PaymentId { get; set; }
        public int? ShipmentId { get; set; }


    }
}
