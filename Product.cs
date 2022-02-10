using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace WorkingWithEFCore
{
    [Table("products")]
    public class Product
    {
        [Column("product_id")]
        public int ProductID { get; set; }
        
        
        [Column("product_name")][Required] [StringLength(40)] public string ProductName { get; set; }

        [Column("unit_price")]
        public decimal? Cost { get; set; }

        [Column("units_in_stock")] public short? Stock { get; set; }
        
        [Column("discontinued")]
        public int Discontinued { get; set; }
        
        [Column("category_id")]
        public int CategoryID { get; set; }
        public virtual Category Category { get; set; }
    }
};