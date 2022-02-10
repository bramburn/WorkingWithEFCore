using System.ComponentModel.DataAnnotations.Schema;

namespace WorkingWithEFCore
{
    [Table("categories")]
    public class Category
    {
        [Column("category_id")]
        public int CategoryID { get; set; }
        [Column("category_name")]
        public string CategoryName { get; set; }


        [Column("description")] public string Description { get; set; }

        public virtual ICollection<Product> Products { get; set; }

        public Category()
        {
            this.Products = new HashSet<Product>();
        }
    }
}