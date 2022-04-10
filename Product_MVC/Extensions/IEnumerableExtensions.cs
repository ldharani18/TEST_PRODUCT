using Microsoft.AspNetCore.Mvc.Rendering;
using Product_MVC.Models;

namespace Product_MVC.Extensions
{
    public static class IEnumerableExtensions
    {
        public static IEnumerable<SelectListItem> BrandItem(IEnumerable<BrandData> Items)
        {
            List<SelectListItem> List = new List<SelectListItem>();
            SelectListItem sli = new SelectListItem()
            {
                Text = "----SELECT----",
                Value = "0"
            };
            List.Add(sli);
            foreach (var item in Items)
            {
                sli = new SelectListItem
                {
                    Text = item.GetPropertyValue("brand_name"),
                    Value = item.GetPropertyValue("Id")
                };
                List.Add(sli);
            }
            return List;
        }
        public static IEnumerable<SelectListItem> CategoryItem(IEnumerable<CategoryData> Items)
        {
            List<SelectListItem> List = new List<SelectListItem>();
            SelectListItem sli = new SelectListItem()
            {
                Text = "----SELECT----",
                Value = "0"
            };
            List.Add(sli);
            foreach (var item in Items)
            {
                sli = new SelectListItem
                {
                    Text = item.GetPropertyValue("category_name"),
                    Value = item.GetPropertyValue("Id")
                };
                List.Add(sli);
            }
            return List;
        }
        public static IEnumerable<SelectListItem> SupplierItem(IEnumerable<SupplierData> Items)
        {
            List<SelectListItem> List = new List<SelectListItem>();
            SelectListItem sli = new SelectListItem()
            {
                Text = "----SELECT----",
                Value = "0"
            };
            List.Add(sli);
            foreach (var item in Items)
            {
                sli = new SelectListItem
                {
                    Text = item.GetPropertyValue("supplier_name"),
                    Value = item.GetPropertyValue("Id")
                };
                List.Add(sli);
            }
            return List;
        }
        public static IEnumerable<SelectListItem> ProductItem(IEnumerable<ProductData> Items)
        {
            List<SelectListItem> List = new List<SelectListItem>();
            SelectListItem sli = new SelectListItem()
            {
                Text = "----SELECT----",
                Value = "0"
            };
            List.Add(sli);
            foreach (var item in Items)
            {
                sli = new SelectListItem
                {
                    Text = item.GetPropertyValue("product_name"),
                    Value = item.GetPropertyValue("Id")
                };
                List.Add(sli);
            }
            return List;
        }

    }
}
