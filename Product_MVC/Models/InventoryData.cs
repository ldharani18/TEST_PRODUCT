using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace Product_MVC.Models
{
    public class InventoryData
    {
        [Key]
        public int Id { get; set; }
        [ForeignKey("Product")]
        public int ProductId { get; set; }
        [Display(Name = "Stock Product Quantity")]
        public int stock_product_quantity { get; set; }
    }
}
