using System.ComponentModel.DataAnnotations;

namespace Product_MVC.Models
{
    public class BrandData
    {
        [Key]
        public int Id { get; set; }
        [Display(Name ="Brand Name")]
        public string brand_name { get; set; }

        public virtual ICollection<ProductData> Products { get; set; }
    }
}
