using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Ecommerce.DTOs.DTOsFavoritandRate
{
    public class CreateorUpdateRateDTO
    {
        public int? Id { get; set; }
        [Range(1, 5)] // Assuming rating is between 1 and 5
        public int? Rating { get; set; }
        public string? CustomerId { get; set; }
        public int? ProductId { get; set; }
    }
}
