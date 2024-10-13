//using NetSohag.Models;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Ecommerce.Models
{
    public class SubCategory : BaseEntity<int>
    {
        //[Key]
        //public int SubcategoryId { get; set; }

        [Required, MaxLength(100)]
        public string Name { get; set; }

        [ForeignKey("Category")]
        public int? CategoryId { get; set; }

        public Category? Category { get; set; }

        public ICollection<ProductSubCategory>? productSubCategory { get; set; }
    }


}
