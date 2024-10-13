using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations.Schema;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;


namespace Ecommerce.Models
{
    public class Shipment : BaseEntity<int>
    {
        //[Key]
        //public int ShipmentId { get; set; }

        public DateTime ShipmentDate { get; set; }

        [Required, MaxLength(150)]
        public string Address { get; set; }

        [MaxLength(100)]
        public string City { get; set; }

        [MaxLength(50)]
        public string Country { get; set; }

        [MaxLength(10)]
        public string ZipCode { get; set; }

        [ForeignKey("Customer")]
        public string? CustomerId { get; set; }
        public Customer? Customer { get; set; }

        public ICollection<Order>? Orders { get; set; }

    }


}
