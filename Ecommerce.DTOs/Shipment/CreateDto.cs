﻿using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Ecommerce.DTOs.Shipment
{
  public  class CreateDto
    {
       

        
        public DateTime ShipmentDate { get; set; }

        [Required, MaxLength(150)]
        public string Address { get; set; }

        [MaxLength(100)]
        public string City { get; set; }

        [MaxLength(50)]
        public string Country { get; set; }

        [MaxLength(10)]
        public string ZipCode { get; set; }

        public string? CustomerId { get; set; }

      //  public List<OrderDto>? Orders { get; set; } // Assuming you also have an OrderDto
    }
}
