using Newtonsoft.Json;
using Product_MVC.Helper;
using Product_MVC.Models;

namespace Product_MVC.Services
{
    public interface IProductService
    {
        public Task<ProductData> GetProductData(int id);
        public Task<BrandData> GetBrandData(int id);
        public Task<CategoryData> GetCategoryData(int id);
        public Task<SupplierData> GetSupplierData(int id);
        public Task<List<ProductData>> GetAllProductData();

        public Task<List<CategoryData>> GetAllCategoryData();

        public Task<List<BrandData>> GetAllBrandData();
        public Task<List<SupplierData>> GetAllSupplierData();

    }
    public class ProductService : IProductService
    {
        ProductAPI _productAPI = new ProductAPI();
        
        //Gets all Brands from the Database 
        public async Task<List<BrandData>> GetAllBrandData()
        {
            List<BrandData> brand = new List<BrandData>();
            HttpClient client = _productAPI.Initial();
            //Gets all the brands from the Url:"~/api/brands"
            HttpResponseMessage res = await client.GetAsync("api/brands");
            if (res.IsSuccessStatusCode)
            {
                var result = res.Content.ReadAsStringAsync().Result;
                brand = JsonConvert.DeserializeObject<List<BrandData>>(result);
            }
            return brand;
        }
        //Gets all Categories from the Database 
        public async Task<List<CategoryData>> GetAllCategoryData()
        {
            List<CategoryData> category = new List<CategoryData>();
            HttpClient client = _productAPI.Initial();
            //Gets all the categories from the Url:"~/api/categories"
            HttpResponseMessage res = await client.GetAsync("api/categories");
            if (res.IsSuccessStatusCode)
            {
                var result = res.Content.ReadAsStringAsync().Result;
                category = JsonConvert.DeserializeObject<List<CategoryData>>(result);
            }
            return category;
        }
        //Gets all Suppliers from the Database 
        public async Task<List<SupplierData>> GetAllSupplierData()
        {
            List<SupplierData> suppliers = new List<SupplierData>();
            HttpClient client = _productAPI.Initial();
            //Gets all the suppliers from the Url:"~/api/suppliers"
            HttpResponseMessage res = await client.GetAsync("api/suppliers");
            if (res.IsSuccessStatusCode)
            {
                var result = res.Content.ReadAsStringAsync().Result;
                suppliers = JsonConvert.DeserializeObject<List<SupplierData>>(result);
            }
            return suppliers;
        }
        //Gets particular brand whose id matches with the id provided
        public async Task<BrandData> GetBrandData(int id)
        {
            BrandData brand = new BrandData();
            HttpClient client = _productAPI.Initial();
            //Gets particular brand from the Url:"~/api/brands/{id}"
            HttpResponseMessage res = await client.GetAsync($"api/brands/{id}");
            if (res.IsSuccessStatusCode)
            {
                var result = res.Content.ReadAsStringAsync().Result;
                brand = JsonConvert.DeserializeObject<BrandData>(result);
            }
            return brand;
        }
        //Gets particular category whose id matches with the id provided
        public async Task<CategoryData> GetCategoryData(int id)
        {
            CategoryData category = new CategoryData();
            HttpClient client = _productAPI.Initial();
            //Gets particular category from the Url:"~/api/categories/{id}"
            HttpResponseMessage res = await client.GetAsync($"api/categories/{id}");
            if (res.IsSuccessStatusCode)
            {
                var result = res.Content.ReadAsStringAsync().Result;
                category = JsonConvert.DeserializeObject<CategoryData>(result);
            }
            return category;
        }
        //Gets particular supplier whose id matches with the id provided
        public async Task<SupplierData> GetSupplierData(int id)
        {
            SupplierData supplier = new SupplierData();
            HttpClient client = _productAPI.Initial();
            //Gets particular supplier from the Url:"~/api/suppliers/{id}"
            HttpResponseMessage res = await client.GetAsync($"api/suppliers/{id}");
            if (res.IsSuccessStatusCode)
            {
                var result = res.Content.ReadAsStringAsync().Result;
                supplier = JsonConvert.DeserializeObject<SupplierData>(result);
            }
            return supplier;
        }
        //Gets particular product whose id matches with the id provided
        public async Task<ProductData> GetProductData(int id)
        {
            ProductData product = new ProductData();
            HttpClient client = _productAPI.Initial();
            //Gets particular product from the Url:"~/api/products/{id}"
            HttpResponseMessage res = await client.GetAsync($"api/products/{id}");
            if (res.IsSuccessStatusCode)
            {
                var result = res.Content.ReadAsStringAsync().Result;
                product = JsonConvert.DeserializeObject<ProductData>(result);
            }
            return product;
        }
        //Gets all the products from the Database
        public async Task<List<ProductData>> GetAllProductData()
        {
            List<ProductData> product = new List<ProductData>();
            HttpClient client = _productAPI.Initial();
            //Gets all the products from the Url:"~/api/products"
            HttpResponseMessage res = await client.GetAsync("api/products");
            if (res.IsSuccessStatusCode)
            {
                var result = res.Content.ReadAsStringAsync().Result;
                product = JsonConvert.DeserializeObject<List<ProductData>>(result);
            }
            return product;
        }

    }
    }
