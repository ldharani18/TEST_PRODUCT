using Microsoft.AspNetCore.Mvc;
using Newtonsoft.Json;
using Product_MVC.Helper;
using Product_MVC.Models;

namespace Product_MVC.Controllers
{
    public class SupplierController : Controller
    {
        //Creating an object for ProductAPI 
        ProductAPI _api = new ProductAPI();
        public async Task<IActionResult> Index(string searchString)
        {
            List<SupplierData> suppliers = new List<SupplierData>();
            HttpClient client = _api.Initial();
            //Retrieving list of suppliers from Database
            HttpResponseMessage res = await client.GetAsync("api/suppliers");
            if (res.IsSuccessStatusCode)
            {
                var results = res.Content.ReadAsStringAsync().Result;
                suppliers = JsonConvert.DeserializeObject<List<SupplierData>>(results);
            }
            //Checks whether the searchstring is empty or not
            if (!String.IsNullOrEmpty(searchString))
            {
                //Checks whether searchstring is contained in the list of supplier names and then filter
                suppliers = suppliers.Where(s => s.supplier_name.ToLower().Contains(searchString)).ToList();
            }
            return View(suppliers);
        }
        //Displays the details of particular supplier whose id matches with the id provided
        public async Task<IActionResult> Details(int id)
        {
            SupplierData supplier = new SupplierData();
            HttpClient client = _api.Initial();
            HttpResponseMessage res = await client.GetAsync($"api/suppliers/{id}");
            if (res.IsSuccessStatusCode)
            {
                var results = res.Content.ReadAsStringAsync().Result;
                supplier = JsonConvert.DeserializeObject<SupplierData>(results);
            }
            return View(supplier);
        }
        //HttpGet method for Create
        public IActionResult Create()
        {
            return View();
        }
        //HttpPost method for Create
        [HttpPost]
        public IActionResult Create(SupplierData supplierData)
        {
            supplierData.Products = new List<ProductData>();
            HttpClient client = _api.Initial();
            //Post method adds new supplier to Database
            var postTask = client.PostAsJsonAsync<SupplierData>("api/suppliers", supplierData);
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
            SupplierData supplier = new SupplierData();
            HttpClient client = _api.Initial();
            //Fetches the particular supplier whose details are to be edited
            HttpResponseMessage res = await client.GetAsync($"api/suppliers/{id}");
            if (res.IsSuccessStatusCode)
            {
                var results = res.Content.ReadAsStringAsync().Result;
                supplier = JsonConvert.DeserializeObject<SupplierData>(results);
            }
            return View(supplier);
        }
        //HttpPost method for Edit
        [HttpPost]
        public async Task<IActionResult> Edit(SupplierData supplier)
        {
            supplier.Products = new List<ProductData>();
            HttpClient client = _api.Initial();
            //Put method enables editing of particular supplier based on id
            var postTask =client.PutAsJsonAsync<SupplierData>($"api/suppliers/{supplier.Id}", supplier);
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
            SupplierData supplier = new SupplierData();
            HttpClient client = _api.Initial();
            //Delete method deleted particular supplier based on id
            HttpResponseMessage res = await client.DeleteAsync($"api/suppliers/{id}");
            return RedirectToAction("Index");
        }
    }

}
