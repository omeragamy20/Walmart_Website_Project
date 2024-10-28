using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations.Schema;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Ecommerce.Models
{
    public class Order : BaseEntity<int>
    {
        //[Key]
        //public int OrderId { get; set; }

        public DateTime OrderDate { get; set; }

        [Column(TypeName = "money")]
        public decimal TotalPrice { get; set; }
        //???

        [Range(0, 6)]
        public int Status { get; set; } = 0;


        [ForeignKey("Customer")]
        public string? CustomerId { get; set; }
        public Customer? Customer { get; set; }

        [ForeignKey("Payment")]
        public int? PaymentId { get; set; }
        public Payment? Payment { get; set; }

        [ForeignKey("Shipment")]
        public int? ShipmentId { get; set; }
        public Shipment? Shipment { get; set; }

        public ICollection<OrderItem>? OrderItems { get; set; }
    }


}
