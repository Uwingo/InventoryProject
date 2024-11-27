using Frontend.Models;
using Microsoft.AspNetCore.Mvc;
using System.Text.Json;

namespace Frontend.Components
{
    public class WarehouseViewComponent : ViewComponent
    {
        private readonly HttpClient _httpClient;

        public WarehouseViewComponent(HttpClient httpClient)
        {
            _httpClient = httpClient;
        }

        public async Task<IViewComponentResult> InvokeAsync()
        {
            int brandCount = 0;

            try
            {
                string apiUrl = "api/Warehouse/get-warehouses";
                var response = await GenericClient.Client.GetAsync(apiUrl);
                if (response.IsSuccessStatusCode)
                {
                    var jsonResponse = await response.Content.ReadAsStringAsync();
                    var brandList = JsonSerializer.Deserialize<List<Warehouse>>(jsonResponse, new JsonSerializerOptions
                    {
                        PropertyNameCaseInsensitive = true
                    });

                    brandCount = brandList?.Count ?? 0;
                }
            }
            catch (Exception ex)
            {
                brandCount = 0;
            }

            return View(brandCount);
        }
    }
}
