using Microsoft.AspNetCore.Identity;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Ecommerce.Models
{
    public class Customer: IdentityUser
    {
        //[Key]
        //public int CustomerId { get; set; }

        [Required, MaxLength(100)]
        public string FirstName { get; set; }

        [MaxLength(100)]
        public string LastName { get; set; }

        [Required, MaxLength(250)]
        public string Address { get; set; }
        public string? Image { get; set; }

        public ICollection<Order>? Orders { get; set; }
        public ICollection<Shipment>? Shipments { get; set; }
        public ICollection<Payment>? Payments { get; set; }
        public ICollection<Rate>? Rates { get; set; }
        public ICollection<Favorite>? Favorites { get; set; }

    }


}
