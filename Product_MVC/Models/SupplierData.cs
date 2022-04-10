using System.ComponentModel.DataAnnotations;

namespace Product_MVC.Models
{
    public class SupplierData
    {
        [Key]
        public int Id { get; set; }
        [Display(Name = "Supplier Name")]
        public string? supplier_name { get; set; }
        [Display(Name = "Supplier Bulstat")]
        public string? supplier_bulstat { get; set; }
        [Display(Name = "Supplier Address")]
        public string? supplier_address { get; set; }
        [DataType(DataType.EmailAddress)]
        [Display(Name = "Supplier Email")]
        public string? supplier_email { get; set; }
        [DataType(DataType.PhoneNumber)]
        [Display(Name = "Phone Number")]
        public string? supplier_phone { get; set; }
        public virtual ICollection<ProductData> Products { get; set; }
    }
}
