using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations.Schema;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
﻿using System.ComponentModel.DataAnnotations.Schema;

namespace Ecommerce.Models
{
    public class OrderItem : BaseEntity<int>
    {
        //[Key]
        //public int OrderItemId { get; set; }

        public int Quantity { get; set; }

        [Column(TypeName = "money")]
        public decimal Price { get; set; }

        [ForeignKey("Product")]
        public int? ProductId { get; set; }
        public Product? Product { get; set; }

        [ForeignKey("Order")]
        public int? OrderId { get; set; }
        public Order? Order { get; set; }
    }

}
