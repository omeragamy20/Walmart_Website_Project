using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Ecommerce.DTOs.DTOsFavoritandRate
{
    public class GetAllFavoritDTO
    {
        public int? Id { get; set; }
        public int? ProductId { get; set; }
        public string? ProductName_en { get; set; }
        public string? ProductName_ar { get; set; }
        public string? ProductDescription_en { get; set; }
        public string? ProductDescription_ar { get; set; }
        public decimal? Productprice { get; set; }

        public string? CustomerId { get; set; }
    }
}
