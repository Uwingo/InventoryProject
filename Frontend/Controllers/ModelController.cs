using Frontend.Models;
using MessagePack.Formatters;
using Microsoft.AspNetCore.Mvc;
using Newtonsoft.Json;
using System.Reflection.Metadata;
using System.Text;
using System.Text.Json;

namespace Frontend.Controllers
{
    public class ModelController : Controller
    {
        private readonly HttpClient _httpClient;
        public ModelController(HttpClient httpClient)
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
                string brandUrl = "api/Brand/get-brands";
                string modelurl = "api/Model/get-models";
                string jsonResponseBrand = await GetDatas(brandUrl);
                string jsonResponseModel = await GetDatas(modelurl);
                if (jsonResponseBrand != null || jsonResponseModel!= null)
                {
                    var brands = System.Text.Json.JsonSerializer.Deserialize<List<Brand>>(jsonResponseBrand, new JsonSerializerOptions
                    {
                        PropertyNameCaseInsensitive = true
                    });
                    var models = System.Text.Json.JsonSerializer.Deserialize<List<Model>>(jsonResponseModel, new JsonSerializerOptions
                    {
                        PropertyNameCaseInsensitive = true
                    });
                    return View("Index", Tuple.Create(brands, models));
                }

                return View();

            }
            catch (Exception ex)
            {

                return View("Error");
            }


        }
        [HttpPost]
        public async Task Create([FromBody] Model model)
        {
            if (ModelState.IsValid)
            {
                var jsonData = JsonConvert.SerializeObject(model);
                var content = new StringContent(jsonData, Encoding.UTF8, "application/json");
                await GenericClient.Client.PostAsync("api/Model/create-model", content);

            }


        }
        [HttpPost]
        public async Task Update([FromBody] Model model)
        {
            if (ModelState.IsValid)
            {
                var jsonData = JsonConvert.SerializeObject(model);
                var content = new StringContent(jsonData, Encoding.UTF8, "application/json");
                await GenericClient.Client.PostAsync("api/Model/update-model", content);

            }


        }
        [HttpPost]
        public async Task Delete([FromBody] int id)
        {
            if (ModelState.IsValid)
            {
                await GenericClient.Client.DeleteAsync("api/Model/delete-model/" + id);
            }
        }
    }
}
