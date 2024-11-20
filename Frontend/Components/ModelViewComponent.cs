using Frontend.Models;
using Microsoft.AspNetCore.Mvc;
using System.Text.Json;

namespace Frontend.ViewComponents
{
    public class ModelViewComponent : ViewComponent
    {
        private readonly HttpClient _httpClient;

        public ModelViewComponent(HttpClient httpClient)
        {
            _httpClient = httpClient;
        }

        public async Task<IViewComponentResult> InvokeAsync()
        {
            try
            {
                string brandUrl = "https://localhost:7215/api/Brand/get-brands";
                string modelUrl = "https://localhost:7215/api/Model/get-models";

                var brands = await GetData<List<Brand>>(brandUrl);
                var models = await GetData<List<Model>>(modelUrl);

                return View(Tuple.Create(brands, models));
            }
            catch (Exception ex)
            {
                return View(Tuple.Create(new List<Brand>(), new List<Model>()));
            }
        }

        private async Task<T> GetData<T>(string uri)
        {
            var response = await _httpClient.GetAsync(uri);
            if (response.IsSuccessStatusCode)
            {
                var jsonResponse = await response.Content.ReadAsStringAsync();
                return JsonSerializer.Deserialize<T>(jsonResponse, new JsonSerializerOptions
                {
                    PropertyNameCaseInsensitive = true
                });
            }
            return default(T);
        }
    }
}
