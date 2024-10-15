using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Ecommerce.DTOs.CustomerDto
{
    public class ResetPasswordDto
    {
       
        [DataType(DataType.Password)]
        public string OldPassword { get; set; }

        [MinLength(8)]
        [Required]
        [DataType(DataType.Password)]

        public string NewPassword { get; set; }


        [MinLength(8)]
        [Required]
        [DataType(DataType.Password)]

        public string ConfirmNewPassword { get; set; }
    }
}
