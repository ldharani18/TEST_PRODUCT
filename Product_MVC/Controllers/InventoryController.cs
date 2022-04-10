using Microsoft.AspNetCore.Mvc;
using Newtonsoft.Json;
using Product_MVC.Helper;
using Product_MVC.Models;
using Product_MVC.Services;

namespace Product_MVC.Controllers
{
    public class InventoryController : Controller
    {

        private readonly IProductService _productService;
        public InventoryController(IProductService productService)
        {
            _productService = productService;
        }
        private List<ProductViewModel> _productViewModels { get; set; }
        //Creating an object for ProductAPI 
        ProductAPI _api = new ProductAPI();
        public async Task<IActionResult> Index()
        {
            _productViewModels = new List<ProductViewModel>();
            List<InventoryData> inventories = new List<InventoryData>();
            HttpClient client = _api.Initial();
            //Retrieving list of inventories from Database
            HttpResponseMessage res = await client.GetAsync("api/inventories");
            if (res.IsSuccessStatusCode)
            {
                var results = res.Content.ReadAsStringAsync().Result;
                inventories = JsonConvert.DeserializeObject<List<InventoryData>>(results);
            }
            foreach (var i in inventories)
            {
                ProductViewModel viewModel = new ProductViewModel();
                viewModel.ProductData = await _productService.GetProductData(i.ProductId);
                viewModel.InventoryData = i;
                _productViewModels.Add(viewModel);
            }
            return View(_productViewModels);
        }
        //Displays the details of particular inventory whose id matches with the id provided
        public async Task<IActionResult> Details(int id)
        {
            InventoryData inventory = new InventoryData();
            HttpClient client = _api.Initial();
            HttpResponseMessage res = await client.GetAsync($"api/inventories/{id}");
            if (res.IsSuccessStatusCode)
            {
                var results = res.Content.ReadAsStringAsync().Result;
                inventory = JsonConvert.DeserializeObject<InventoryData>(results);
            }
            return View(inventory);
        }
        /*
        public async Task<IActionResult> Create()
        {
            InventoryCreateViewModel model = new InventoryCreateViewModel();
            model.Products = await _productService.GetAllProductData();
            model.InventoryData = new InventoryData();
            return View(model);
        }
        [HttpPost]
        public async Task<IActionResult> Create(InventoryCreateViewModel inventoryCreateView)
        {
            HttpClient client = _api.Initial();
            var postTask = client.PostAsJsonAsync<InventoryData>("api/inventories", inventoryCreateView.InventoryData);
            postTask.Wait();
            var res = postTask.Result;
            if (res.IsSuccessStatusCode)
            {
                return RedirectToAction("Index");
            }
            InventoryCreateViewModel model = new InventoryCreateViewModel();
            model.Products = await _productService.GetAllProductData();
            model.InventoryData = new InventoryData();
            return View(_productViewModels);
        }
       */
        //HttpGet method for Edit
        [HttpGet]
        public async Task<IActionResult> Edit(int id)
        {
            InventoryData inventory = new InventoryData();
            HttpClient client = _api.Initial();
            //Fetches the particular inventory whose details are to be edited
            HttpResponseMessage res = await client.GetAsync($"api/inventories/{id}");
            if (res.IsSuccessStatusCode)
            {
                var results = res.Content.ReadAsStringAsync().Result;
                inventory = JsonConvert.DeserializeObject<InventoryData>(results);
            }
            return View(inventory);
        }
        //HttpPost method for Edit
        [HttpPost]
        public async Task<IActionResult> Edit(InventoryData inventory)
        {
            HttpClient client = _api.Initial();
            //Put method enables editing of particular inventory based on id
            var postTask = client.PutAsJsonAsync<InventoryData>($"api/inventories/{inventory.Id}", inventory);
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
            InventoryData inventory = new InventoryData();
            HttpClient client = _api.Initial();
            //Delete method deleted particular brand based on id
            HttpResponseMessage res = await client.DeleteAsync($"api/inventories/{id}");
            return RedirectToAction("Index");
        }
    }
}
