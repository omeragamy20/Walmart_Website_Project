namespace Ecommerce.Models
{
    public class BaseEntity<T>
    {
        public T Id { get; set; }
        public int? CreatedBy { get; set; }
        public DateTime? Created { get; set; }
        public int? UpdatedBy { get; set; }
        public DateTime? Updated { get; set; }
        public bool? IsDeleted { get; set; }
    }
}
