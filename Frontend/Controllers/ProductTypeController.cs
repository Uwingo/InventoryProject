using Microsoft.AspNetCore.Mvc;
using Newtonsoft.Json;
using System.Text.Json;
using System.Text;
using Frontend.Models;

namespace Frontend.Controllers
{
    public class ProductTypeController : Controller
    {
        private readonly HttpClient _httpClient;
        public ProductTypeController(HttpClient httpClient)
        {

            _httpClient = httpClient;

        }
        public async Task<string> GetDatas(string uri)
        {
            string brandUrl = uri;
            var response = await GenericClient.Client.GetAsync(brandUrl);
            if (response.IsSuccessStatusCode)
                return await response.Content.ReadAsStringAsync();
            return null;
        }
        public async Task<IActionResult> Index()
        {
            try
            {
                string brandUrl = "api/ProductType/get-product-types";
                string modelurl = "api/MeasurementUnit/get-measurement-units";
                string jsonResponseBrand = await GetDatas(brandUrl);
                string jsonResponseModel = await GetDatas(modelurl);
                if (jsonResponseBrand != null || jsonResponseModel != null)
                {
                    var productTypes = System.Text.Json.JsonSerializer.Deserialize<List<ProductType>>(jsonResponseBrand, new JsonSerializerOptions
                    {
                        PropertyNameCaseInsensitive = true
                    });
                    var units = System.Text.Json.JsonSerializer.Deserialize<List<MeasurementUnit>>(jsonResponseModel, new JsonSerializerOptions
                    {
                        PropertyNameCaseInsensitive = true
                    });
                    return View("Index", Tuple.Create(productTypes, units));
                }

                return View();

            }
            catch (Exception ex)
            {

                return View("Error");
            }


        }
        [HttpPost]
        public async Task Create([FromBody] ProductType productType)
        {
            if (ModelState.IsValid)
            {
                var jsonData = JsonConvert.SerializeObject(productType);
                var content = new StringContent(jsonData, Encoding.UTF8, "application/json");
                await GenericClient.Client.PostAsync("api/ProductType/create-product-type", content);

            }


        }
        [HttpPost]
        public async Task Update([FromBody] ProductType productType)
        {
            if (ModelState.IsValid)
            {
                var jsonData = JsonConvert.SerializeObject(productType);
                var content = new StringContent(jsonData, Encoding.UTF8, "application/json");
                await GenericClient.Client.PostAsync("api/ProductType/update-product-type", content);

            }


        }
        [HttpPost]
        public async Task Delete([FromBody] int id)
        {
            if (ModelState.IsValid)
            {
                await GenericClient.Client.DeleteAsync("api/ProductType/delete-product-type/" + id);
            }
        }
    }
}
