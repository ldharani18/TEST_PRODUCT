using System.ComponentModel.DataAnnotations;

namespace Product_MVC.Models
{
    public class CategoryData
    {
        [Key]
        public int Id { get; set; }
        [Display(Name = "Category Name")]
        public string category_name { get; set; }
        public virtual ICollection<ProductData> Products { get; set; }
    }
}
