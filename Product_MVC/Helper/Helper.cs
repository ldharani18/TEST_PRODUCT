namespace Product_MVC.Helper
{
    public class ProductAPI
    {
        //Defining Initial method which returns the HttpClient which stores the Url of Product_API
        public HttpClient Initial()
        {
            var client = new HttpClient();
            client.BaseAddress = new Uri("https://localhost:7178/");
            return client;
        }
    }
}
