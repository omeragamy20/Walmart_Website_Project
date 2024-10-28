using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations.Schema;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Ecommerce.Models
{
    public class Payment : BaseEntity<int>
    {
        //[Key]
        //public int PaymentId { get; set; }

        public DateTime? PaymentDate { get; set; }

        [Required, MaxLength(100)]
        public string? PaymentMethod_en { get; set; }
        [Required, MaxLength(100)]
        public string? PaymentMethod_ar { get; set; }

        [Column(TypeName = "money")]
        public decimal? Amount { get; set; }

        [ForeignKey("Customer")]
        public string? CustomerId { get; set; }
        public Customer? Customer { get; set; }
        public ICollection<Order>? Orders { get; set; }

    }


}
