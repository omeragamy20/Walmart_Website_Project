using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations.Schema;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
namespace Ecommerce.Models
{
    public class ProductSubCategory : BaseEntity<int>
    {
        
        [ForeignKey("SubCategory")]
        public int? SubcategoryId { get; set; }
        public SubCategory? SubCategory { get; set; }

        [ForeignKey("Product")]
        public int? ProductId { get; set; }
        public Product? Product { get; set; }
    }
}
