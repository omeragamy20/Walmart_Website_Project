using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Ecommerce.Models
{
    public class Category: BaseEntity<int>
    {
        //[Key]
        //public int CategoryId { get; set; }

        [Required, MaxLength(100)]
        public string Name_en { get; set; }
        [Required, MaxLength(100)]
        public string Name_ar { get; set; }
        public string? Image { get; set; }

        public ICollection<SubCategory>? SubCategories { get; set; }
    }

}
