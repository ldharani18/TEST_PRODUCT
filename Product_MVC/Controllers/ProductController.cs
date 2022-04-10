using Microsoft.AspNetCore.Mvc;
using Newtonsoft.Json;
using Product_MVC.Helper;
using Product_MVC.Models;
using Product_MVC.Controllers;
using Product_MVC.Services;

namespace Product_MVC.Controllers
{
    public class ProductController : Controller
    {
        private readonly IProductService _productService;
        public ProductController(IProductService productService)
        {
            _productService = productService;
        }
        private List<ProductViewModel> _productViewModels { get; set; }
        //Creating an object for ProductAPI 
        ProductAPI _api = new ProductAPI();
        public async Task<IActionResult> Index(string searchString)
        {
            _productViewModels = new List<ProductViewModel>();
            List<ProductData> products = new List<ProductData>();
            HttpClient client = _api.Initial();
            //Retrieving list of products from Database
            HttpResponseMessage res = await client.GetAsync("api/products");
            if (res.IsSuccessStatusCode)
            {
                var results = res.Content.ReadAsStringAsync().Result;
                products = JsonConvert.DeserializeObject<List<ProductData>>(results);
            }
            foreach (var p in products)
            {
                ProductViewModel viewModel = new ProductViewModel();
                viewModel.ProductData = p;
                viewModel.SupplierData = await _productService.GetSupplierData(p.SupplierId);
                viewModel.BrandData = await _productService.GetBrandData(p.BrandId);
                viewModel.CategoryData = await _productService.GetCategoryData(p.CategoryId);
                _productViewModels.Add(viewModel);
            }
            _productViewModels = await GetProductViewModel(searchString);
            return View(_productViewModels);
        }
        //Displays the details of particular product whose id matches with the id provided
        public async Task<IActionResult> Details(int id)
        {
            ProductData product = new ProductData();
            HttpClient client = _api.Initial();
            HttpResponseMessage res = await client.GetAsync($"api/products/{id}");
            if (res.IsSuccessStatusCode)
            {
                var results = res.Content.ReadAsStringAsync().Result;
                product = JsonConvert.DeserializeObject<ProductData>(results);
            }
            return View(product);
        }
        //HttpGet method for Create
        [HttpGet]
        public async Task<IActionResult> Create()
        {
            ProductCreateViewModel model = new ProductCreateViewModel();
            model.ProductData = new ProductData();
            model.Brands = await _productService.GetAllBrandData();
            model.Categories = await _productService.GetAllCategoryData();
            model.Suppliers = await _productService.GetAllSupplierData();
            return View(model);
        }
        //HttpPost method for Create
        [HttpPost]
        public async Task<IActionResult> Create(ProductCreateViewModel productCreateView)
        {
            
            HttpClient client = _api.Initial();
            //Post method adds new product to Database
            var postTask = client.PostAsJsonAsync<ProductData>("api/products", productCreateView.ProductData);
            postTask.Wait();
            var res = postTask.Result;
            if (res.IsSuccessStatusCode)
            {
                return RedirectToAction("Index");
            }
            ProductCreateViewModel model = new ProductCreateViewModel();
            model.ProductData = new ProductData();
            model.Brands = await _productService.GetAllBrandData();
            model.Categories = await _productService.GetAllCategoryData();
            model.Suppliers = await _productService.GetAllSupplierData();
            return View(_productViewModels);
        }
        //HttpGet method for Edit
        [HttpGet]
        public async Task<IActionResult> Edit(int id)
        {
            ProductCreateViewModel model = new ProductCreateViewModel();
            model.ProductData = await _productService.GetProductData(id);
            model.Brands = await _productService.GetAllBrandData();
            model.Categories = await _productService.GetAllCategoryData();
            model.Suppliers = await _productService.GetAllSupplierData();
           /* ProductData product = new ProductData();
            HttpClient client = _api.Initial();
            HttpResponseMessage res = await client.GetAsync($"api/products/{id}");
            if (res.IsSuccessStatusCode)
            {
                var results = res.Content.ReadAsStringAsync().Result;
                product = JsonConvert.DeserializeObject<ProductData>(results);
            }*/
            return View(model);
        }
        //HttpPost method for Edit
        [HttpPost]
        public async Task<IActionResult> Edit(ProductCreateViewModel productCreateView)
        {
            HttpClient client = _api.Initial();
            //Put method enables editing of particular product based on id
            var postTask = client.PutAsJsonAsync<ProductData>($"api/products/{productCreateView.ProductData.Id}", productCreateView.ProductData);
            postTask.Wait();
            var res = postTask.Result;
            if (res.IsSuccessStatusCode)
            {
                return RedirectToAction("Index");
            }
            ProductCreateViewModel model = new ProductCreateViewModel();
            model.ProductData = await _productService.GetProductData(productCreateView.ProductData.Id);
            model.Brands = await _productService.GetAllBrandData();
            model.Categories = await _productService.GetAllCategoryData();
            model.Suppliers = await _productService.GetAllSupplierData();
            return View(model);
        }
        public async Task<IActionResult> Delete(int id)
        {
            ProductData product = new ProductData();
            HttpClient client = _api.Initial();
            //Delete method deleted particular product based on id
            HttpResponseMessage res = await client.DeleteAsync($"api/products/{id}");
            return RedirectToAction("Index");
        }
        public async Task<List<ProductViewModel>> GetProductViewModel(string SearchString)
        {
            _productViewModels = new List<ProductViewModel>();
            List<ProductData> product = new List<ProductData>();
            product = await _productService.GetAllProductData();
            //Checks whether the searchstring is empty or not
            if (!String.IsNullOrEmpty(SearchString))
            {
                //Checks whether searchstring is contained in the list of brand names and then filter
                product = product.Where(p => p.product_name!.ToLower().Contains(SearchString)).ToList();
            }
            foreach (var p in product)
            {
                ProductViewModel pvModel = new ProductViewModel();
                pvModel.ProductData = p;
                pvModel.SupplierData = await _productService.GetSupplierData(p.SupplierId);
                pvModel.BrandData = await _productService.GetBrandData(p.BrandId);
                pvModel.CategoryData = await _productService.GetCategoryData(p.CategoryId);
                _productViewModels.Add(pvModel);
            }
            return _productViewModels;

        }
    }
}
