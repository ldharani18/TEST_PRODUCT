using System.ComponentModel.DataAnnotations;

namespace Product_MVC.Extensions
{
    public class YearRangeTillDateAttribute : RangeAttribute
    {
        public YearRangeTillDateAttribute(int StartYear) : base(StartYear, DateTime.Today.Year)
        {

        }
    }
}
