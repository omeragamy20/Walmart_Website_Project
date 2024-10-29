using Microsoft.EntityFrameworkCore.Metadata.Internal;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace Ecommerce.Models
{
    public class Favorite : BaseEntity<int>
    {
        //[Key]
        //public int FavoriteId { get; set; }

        // Foreign keys
        [ForeignKey("Customer")]
        public string? CustomerId { get; set; }
        [ForeignKey("Product")]
        public int? ProductId { get; set; }

        public Customer? Customer { get; set; }
        public Product? Product { get; set; }
    }
}
