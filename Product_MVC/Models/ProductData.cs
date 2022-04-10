using System.ComponentModel.DataAnnotations;

namespace Product_MVC.Models
{
    public class ProductData
    {
        [Key]
        public int Id { get; set; }
        [Display(Name = "Product Name")]
        public string? product_name { get; set; }

        [Display(Name = "Product Brand")]
        public int BrandId { get; set; }

        //public Brand Brand { get; set; }

        [Display(Name = "Product Supplier")]
        public int SupplierId { get; set; }
        //public Supplier Supplier { get; set; }

        [Display(Name = "Product Category")]
        public int CategoryId { get; set; }
        //public Category Category { get; set; }
        [DataType(DataType.Currency)]
        [Display(Name = "Product Price")]
        public double product_price { get; set; }

        public virtual InventoryData Inventory { get; set; }
    }
}
