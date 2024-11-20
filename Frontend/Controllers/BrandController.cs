using Frontend.Models;
using Microsoft.AspNetCore.Mvc;
using Newtonsoft.Json;
using System.Text;
using System.Text.Json;


namespace Frontend.Controllers
{
    public class BrandController : Controller
    {

        private readonly HttpClient _httpClient;
        public BrandController(HttpClient httpClient)
        {

            _httpClient = httpClient;

        }
        public async Task<IActionResult> Index()
        {
            try
            {
                var response = await GenericClient.Client.GetAsync("api/Brand/get-brands");
                if (response.IsSuccessStatusCode)
                {
                    var jsonResponse = await response.Content.ReadAsStringAsync();
                    var model = System.Text.Json.JsonSerializer.Deserialize<List<Brand>>(jsonResponse, new JsonSerializerOptions
                    {
                        PropertyNameCaseInsensitive = true
                    });
                    return View(model);
                }
                else
                {
                    ViewBag.ErrorMessage = "There was an error fetching data from the API.";
                    return View(new List<Brand>());
                }

            }
            catch (Exception ex)
            {

                return View("Error");
            }


        }
        [HttpPost]
        public async Task Create([FromBody] Brand brand)
        {
            if (ModelState.IsValid)
            {
                var jsonData = JsonConvert.SerializeObject(brand);
                var content = new StringContent(jsonData, Encoding.UTF8, "application/json");
                await GenericClient.Client.PostAsync("api/Brand/create-brand", content);

            }


        }
        [HttpPost]
        public async  Task Update([FromBody] Brand brand)
        {
            if (ModelState.IsValid)
            {
                var jsonData = JsonConvert.SerializeObject(brand);
                var content = new StringContent(jsonData, Encoding.UTF8, "application/json");
                await GenericClient.Client.PostAsync("api/Brand/update-brand", content);
              
            }

            
        }
        [HttpPost]
        public async Task Delete([FromBody] int id)
        {
            if (ModelState.IsValid)
            {
                await GenericClient.Client.DeleteAsync("api/Brand/delete-brand/"+id);
            }
        }
    }
}
