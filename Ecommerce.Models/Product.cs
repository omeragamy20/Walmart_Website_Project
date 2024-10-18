using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations.Schema;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using static System.Net.Mime.MediaTypeNames;

namespace Ecommerce.Models
{
    public class Product : BaseEntity<int>
    {
        [Required, MaxLength(100)]
        public string Title_en { get; set; }
        [Required, MaxLength(100)]
        public string Title_ar { get; set; }

        [MaxLength(500)]
        public string Description_en { get; set; }
        [MaxLength(500)]
        public string Description_ar { get; set; }

        [Column(TypeName = "money")]
        public decimal Price { get; set; }
        public int Stock { get; set; }
        public ICollection<ProductSubCategory>? productSubCategory { get; set; }
        //[ForeignKey("SubCategory")]
        //public int? SubCategoryId { get; set; }
        //public SubCategory? SubCategory { get; set; }

        public ICollection<ProductFacility>? ProductFacilities { get; set; } // = new List<ProductFacility>();
        public ICollection<Rate>? Rates { get; set; }
        
        public ICollection<Images>? Images { get; set; }
        public ICollection<OrderItem>? OrderItems { get; set; }
        public ICollection<Favorite>? Favorites { get; set; }
    }


}
