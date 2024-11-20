using Microsoft.AspNetCore.Mvc;
using Newtonsoft.Json;
using System.Text.Json;
using System.Text;
using Frontend.Models;

namespace Frontend.Controllers
{
    public class ConsumableController : Controller
    {
        private readonly HttpClient _httpClient;
        public ConsumableController(HttpClient httpClient)
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
                string consumableUrl = "api/Consumable/get-consumables";
                string producturl = "api/Product/get-products";

                string jsonResponseConsumable = await GetDatas(consumableUrl);
                string jsonResponseProduct= await GetDatas(producturl);

                if (jsonResponseConsumable != null || jsonResponseProduct != null)
                {
                     var consumables = new List<Consumable>();
                    if (jsonResponseConsumable != null)
                    {
                         consumables = System.Text.Json.JsonSerializer.Deserialize<List<Consumable>>(jsonResponseConsumable, new JsonSerializerOptions
                        {
                            PropertyNameCaseInsensitive = true
                        });
                    }
                    else consumables = null;
                   
                    var products = System.Text.Json.JsonSerializer.Deserialize<List<Product>>(jsonResponseProduct, new JsonSerializerOptions
                    {
                        PropertyNameCaseInsensitive = true
                    });
                    return View("Index", Tuple.Create(consumables, products));
                }

                return View();

            }
            catch (Exception ex)
            {

                return View("Error");
            }


        }
        [HttpPost]
        public async Task Create([FromBody] Consumable consumable)
        {
            if (ModelState.IsValid)
            {
                var jsonData = JsonConvert.SerializeObject(consumable);
                var content = new StringContent(jsonData, Encoding.UTF8, "application/json");
                await GenericClient.Client.PostAsync("api/Consumable/create-consumable", content);

            }


        }
        [HttpPost]
        public async Task Update([FromBody] Consumable consumable)
        {
            if (ModelState.IsValid)
            {
                var jsonData = JsonConvert.SerializeObject(consumable);
                var content = new StringContent(jsonData, Encoding.UTF8, "application/json");
                await GenericClient.Client.PostAsync("api/Consumable/update-consumable", content);

            }


        }
        [HttpPost]
        public async Task Delete([FromBody] int id)
        {
            if (ModelState.IsValid)
            {
                await GenericClient.Client.DeleteAsync("api/Consumable/delete-consumable/" + id);
            }
        }
    }
}
