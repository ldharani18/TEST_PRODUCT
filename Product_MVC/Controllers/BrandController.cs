using Microsoft.AspNetCore.Mvc;
using Newtonsoft.Json;
using Product_MVC.Helper;
using Product_MVC.Models;

namespace Product_MVC.Controllers
{
    public class BrandController : Controller
    {
        //Creating an object for ProductAPI 
        ProductAPI _api = new ProductAPI();

        
        public async Task<IActionResult> Index(string searchString)
        {
            List<BrandData> brands = new List<BrandData>();
            HttpClient client = _api.Initial();
            //Retrieving list of brands from Database
            HttpResponseMessage res = await client.GetAsync("api/brands");
            if (res.IsSuccessStatusCode)
            {
                var results = res.Content.ReadAsStringAsync().Result;
                brands = JsonConvert.DeserializeObject<List<BrandData>>(results);
            }
            //Checks whether the searchstring is empty or not
            if(!String.IsNullOrEmpty(searchString))
            {
                //Checks whether searchstring is contained in the list of brand names and then filter
                brands=brands.Where(b=>b.brand_name.ToLower().Contains(searchString)).ToList();
            }
            return View(brands);
        }
        //Displays the details of particular brand whose id matches with the id provided
        public async Task<IActionResult> Details(int id)
        {
            BrandData brand = new BrandData();
            HttpClient client = _api.Initial();
            
            HttpResponseMessage res = await client.GetAsync($"api/brands/{id}");
            if (res.IsSuccessStatusCode)
            {
                var results = res.Content.ReadAsStringAsync().Result;
                brand = JsonConvert.DeserializeObject<BrandData>(results);
            }
            return View(brand);
        }
        //HttpGet method for Create
        public IActionResult Create()
        {
            return View();
        }
        //HttpPost method for Create
        [HttpPost]
        public IActionResult Create(BrandData brandData)
        {
            brandData.Products = new List<ProductData>();
            HttpClient client = _api.Initial();
            //Post method adds new brand to Database
            var postTask = client.PostAsJsonAsync<BrandData>("api/brands", brandData);
            postTask.Wait();
            var res = postTask.Result;
            if (res.IsSuccessStatusCode)
            {
                return RedirectToAction("Index");
            }
            return View();
        }
        //HttpGet method for Edit
        [HttpGet]
        public async Task<IActionResult> Edit(int id)
        {

            BrandData brand = new BrandData();
            HttpClient client = _api.Initial();
            //Fetches the particular brand whose details are to be edited
            HttpResponseMessage res = await client.GetAsync($"api/brands/{id}");
            if (res.IsSuccessStatusCode)
            {
                var results = res.Content.ReadAsStringAsync().Result;
                brand= JsonConvert.DeserializeObject<BrandData>(results);
            }
            return View(brand);
        }
        //HttpPost method for Edit
        [HttpPost]
        public async Task<IActionResult> Edit(BrandData brand)
        {
            brand.Products = new List<ProductData>();
            HttpClient client = _api.Initial();
            //Put method enables editing of particular brand based on id
            var postTask = client.PutAsJsonAsync<BrandData>($"api/brands/{brand.Id}", brand);
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
            BrandData brand = new BrandData();
            HttpClient client = _api.Initial();
            //Delete method deleted particular brand based on id
            HttpResponseMessage res = await client.DeleteAsync($"api/brands/{id}");
            return RedirectToAction("Index");
        }
    }
}
