using Microsoft.AspNetCore.Mvc;
using Newtonsoft.Json;
using Product_MVC.Helper;
using Product_MVC.Models;

namespace Product_MVC.Controllers
{
    public class CategoryController : Controller
    {
        //Creating an object for ProductAPI 
        ProductAPI _api = new ProductAPI();
        public async Task<IActionResult> Index(string SearchString)
        {
            ViewBag.CurrentFilter=SearchString;
            List<CategoryData> categories = new List<CategoryData>();
            HttpClient client = _api.Initial();
            //Retrieving list of categories from Database
            HttpResponseMessage res = await client.GetAsync("api/categories");
            if (res.IsSuccessStatusCode)
            {
                var results = res.Content.ReadAsStringAsync().Result;
                categories = JsonConvert.DeserializeObject<List<CategoryData>>(results);
            }
            //Checks whether the searchstring is empty or not
            if (!String.IsNullOrEmpty(SearchString))
            {
                //Checks whether searchstring is contained in the list of brand names and then filter
                categories = categories.Where(c=>c.category_name.ToLower().Contains(SearchString)).ToList();
            }
            return View(categories);
        }
        //Displays the details of particular category whose id matches with the id provided
        public async Task<IActionResult> Details(int id)
        {
            CategoryData category = new CategoryData();
            HttpClient client = _api.Initial();
            HttpResponseMessage res = await client.GetAsync($"api/categories/{id}");
            if (res.IsSuccessStatusCode)
            {
                var results = res.Content.ReadAsStringAsync().Result;
                category = JsonConvert.DeserializeObject<CategoryData>(results);
            }
            return View(category);
        }
        //HttpGet method for Create
        public IActionResult Create()
        {
            return View();
        }
        //HttpPost method for Create
        [HttpPost]
        public IActionResult Create(CategoryData categoryData)
        {
            categoryData.Products= new List<ProductData>();
            HttpClient client = _api.Initial();
            //Post method adds new category to Database
            var postTask = client.PostAsJsonAsync<CategoryData>("api/categories", categoryData);
            postTask.Wait();
            var res = postTask.Result;
            if (res.IsSuccessStatusCode)
            {
                return RedirectToAction("Index");
            }
            return View();
        }
        
        [HttpGet]
        public async Task<IActionResult> Edit(int id)
        {

            CategoryData category = new CategoryData();
            HttpClient client = _api.Initial();
            //Fetches the particular category whose details are to be edited
            HttpResponseMessage res = await client.GetAsync($"api/categories/{id}");
            if (res.IsSuccessStatusCode)
            {
                var results = res.Content.ReadAsStringAsync().Result;
                category = JsonConvert.DeserializeObject<CategoryData>(results);
            }
            return View(category);
        }
        [HttpPost]
        public async Task<IActionResult> Edit(CategoryData categoryData)
        {
            categoryData.Products=new List<ProductData>();
            HttpClient client = _api.Initial();
            //Put method enables editing of particular category based on id
            var postTask = client.PutAsJsonAsync<CategoryData>($"api/categories/{categoryData.Id}", categoryData);
            postTask.Wait();
            var res = postTask.Result;
            if (res.IsSuccessStatusCode)
            {
                return RedirectToAction("Index");
            }
            return View();
        }
        public async Task<IActionResult> Delete(int id)
        {
            CategoryData product = new CategoryData();
            HttpClient client = _api.Initial();
            //Delete method deleted particular brand based on id
            HttpResponseMessage res = await client.DeleteAsync($"api/categories/{id}");
            return RedirectToAction("Index");
        }
    }
}
