using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Ecommerce.DTOs.CustomerDto
{
    public class CustomerLoginDto
    {
        public string Username { get; set; }
        [DataType(DataType.Password)]
        public string Paswword { get; set; }
        public bool RememberMe { get; set; }
    }
}
