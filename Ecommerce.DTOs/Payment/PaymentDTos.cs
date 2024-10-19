using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations.Schema;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Ecommerce.DTOs.Payment
{
    public class PaymentDTos
    {
        public string Id { get; set; }

        [Key]
        public int PaymentId { get; set; }

        public DateTime PaymentDate { get; set; }

        [Required, MaxLength(100)]
        public string PaymentMethod_en { get; set; }
        [MaxLength(100)]
        public string? PaymentMethod_ar { get; set; }

        [Column(TypeName = "money")]
        public decimal Amount { get; set; }


    }
}
