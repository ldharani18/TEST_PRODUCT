namespace Product_MVC.Models
{
    public class ProductCreateViewModel
    { 
        public ProductData ProductData { get; set; }
        public IEnumerable<BrandData> Brands { get; set; }
        public IEnumerable<CategoryData> Categories { get; set; }
        public IEnumerable<SupplierData> Suppliers { get; set; }
    
    }
}
