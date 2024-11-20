using Microsoft.AspNetCore.Mvc;
using Newtonsoft.Json;
using System.Text.Json;
using System.Text;
using Frontend.Models;

namespace Frontend.Controllers
{
    public class ProductController : Controller
    {
        private readonly HttpClient _httpClient;
        public ProductController(HttpClient httpClient)
        {
            _httpClient = httpClient;
        }
        public async Task<string> GetDatas(string uri)
        {

            var response = await GenericClient.Client.GetAsync(uri);
            if (response.IsSuccessStatusCode)
                return await response.Content.ReadAsStringAsync();
            return null;
        }
        public async Task<IActionResult> Index()
        {
            try
            {
                string productUrl = "api/Product/get-products";
                string modelUrl = "api/Model/get-models";
                string supplierUrl = "api/Supplier/get-suppliers";
                string productTypeUrl = "api/ProductType/get-product-types";
                string jsonResponseProduct = await GetDatas(productUrl);
                string jsonResponseModel = await GetDatas(modelUrl);
                string jsonResponseSupplier = await GetDatas(supplierUrl);
                string jsonResponseProductType = await GetDatas(productTypeUrl);


                if (jsonResponseProduct != null || jsonResponseModel != null)
                {
                    var products= new List<Product>();
                    if (jsonResponseProduct != null)
                    {
                        products= System.Text.Json.JsonSerializer.Deserialize<List<Product>>(jsonResponseProduct, new JsonSerializerOptions
                        {
                            PropertyNameCaseInsensitive = true
                        });
                    }
                    else products = null;
                   
                    var models = System.Text.Json.JsonSerializer.Deserialize<List<Model>>(jsonResponseModel, new JsonSerializerOptions
                    {
                        PropertyNameCaseInsensitive = true
                    });
                    var suppliers = System.Text.Json.JsonSerializer.Deserialize<List<Supplier>>(jsonResponseSupplier, new JsonSerializerOptions
                    {
                        PropertyNameCaseInsensitive = true
                    });
                    var types = System.Text.Json.JsonSerializer.Deserialize<List<ProductType>>(jsonResponseProductType, new JsonSerializerOptions
                    {
                        PropertyNameCaseInsensitive = true
                    });
                    return View("Index", Tuple.Create(products, models,suppliers,types));
                }

                return View();

            }
            catch (Exception ex)
            {

                return View("Error");
            }


        }

        [HttpPost]
        public async Task Create([FromBody] Product product)
        {
            try
            {
                if (ModelState.IsValid)
                {
                    var jsonData = JsonConvert.SerializeObject(product);
                    var content = new StringContent(jsonData, Encoding.UTF8, "application/json");
                    await GenericClient.Client.PostAsync("api/Product/create-product", content);

                }
            }
            catch (Exception ex)
            {

                throw;
            }



        }
        [HttpPost]
        public async Task Update([FromBody] Product product)
        {
            if (ModelState.IsValid)
            {
                var jsonData = JsonConvert.SerializeObject(product);
                var content = new StringContent(jsonData, Encoding.UTF8, "application/json");
                await GenericClient.Client.PostAsync("api/Product/update-product", content);

            }


        }
        [HttpPost]
        public async Task Delete([FromBody] int id)
        {
            //if (ModelState.IsValid)
            //{
            //    var jsonData = JsonConvert.SerializeObject(id);
            //    var content = new StringContent(jsonData, Encoding.UTF8, "application/json");
            //    await _httpClient.PostAsync("https://localhost:7215/api/Product/delete-product", content);
            //}
            if (ModelState.IsValid)
            {
                await GenericClient.Client.DeleteAsync("api/Product/delete-product/" + id);
            }
        }
    }
}
