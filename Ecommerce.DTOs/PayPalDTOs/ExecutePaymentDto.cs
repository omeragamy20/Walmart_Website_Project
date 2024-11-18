using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Ecommerce.DTOs.PayPalDTOs
{
    public class ExecutePaymentDto
    {
        public string PayerId { get; set; }
        public string PaymentId { get; set; }
    }
}
