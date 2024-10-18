using Microsoft.AspNetCore.Http;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Ecommerce.DTOs.CustomerDto
{
    public class CreateAdminDto
    {
        public string Username {  get; set; }
        public string FirstName { get; set; }
        public string LastName { get; set; }

        [MinLength(8)]
        [Required]
        
        [DataType(DataType.Password)]
        public string Password { get; set; }

        [Compare("Password")]
        [DataType(DataType.Password)]
        public string ConfirmPassword { get; set; }

        [DataType(DataType.EmailAddress)]
        public string Email { get; set; }
        public string? Image { get; set; }

        public IFormFile? ImageData { get; set; }
        public string Address { get; set; }
       
         [DataType(DataType.PhoneNumber)]
        public string PhoneNumber { get; set; }
    }
}
