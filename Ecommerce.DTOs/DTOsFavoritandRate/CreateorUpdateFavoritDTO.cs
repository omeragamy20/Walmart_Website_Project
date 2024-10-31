using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Ecommerce.DTOs.DTOsFavoritandRate
{
    public class CreateorUpdateFavoritDTO
    {
        public int? Id { get; set; }
        public int ProductId { get; set; }
        public string CustomerId { get; set; }

    }
}
