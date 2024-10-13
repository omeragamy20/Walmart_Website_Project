using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations.Schema;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Runtime.CompilerServices;

namespace Ecommerce.DTOs.OrderDTOs
{
    public class CreateOrUpdateOrderDTOs
    {
        public int Id { get; set; }
        public DateTime OrderDate { get; set; }

        [Range(50,double.MaxValue)]
        public decimal TotalPrice { get; set; }
        //???

        [Range(0, 6)]
        public int Status { get; set; } = 0;


        public string? CustomerId { get; set; }

        public int? PaymentId { get; set; }

        public int? ShipmentId { get; set; }

        public ICollection<int>? OrderItemsID { get; set; }
    }
}
