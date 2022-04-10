namespace Product_MVC.Models
{
    public class InventoryCreateViewModel
    {
        public InventoryData InventoryData { get; set; }
        public IEnumerable<ProductData> Products { get; set; }
    }
}
