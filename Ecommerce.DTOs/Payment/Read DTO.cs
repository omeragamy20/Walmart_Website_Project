using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Ecommerce.DTOs.Payment
{
    public class Read_DTO
    {
       
        public int PaymentId { get; set; }
        public DateTime PaymentDate { get; set; }

        public string PaymentMethod_en { get; set; }
        public string? PaymentMethod_ar { get; set; }

        public decimal Amount { get; set; }
        public string? CustomerId { get; set; }

    }
}
