using Frontend.Models;
using Microsoft.AspNetCore.Mvc;
using System.Text.Json;

namespace Frontend.Components
{
    public class MeasurementUnitViewComponent : ViewComponent
    {
        private readonly HttpClient _httpClient;

        public MeasurementUnitViewComponent(HttpClient httpClient)
        {
            _httpClient = httpClient;
        }

        public async Task<IViewComponentResult> InvokeAsync()
        {
            int brandCount = 0;

            try
            {
                string apiUrl = "api/MeasurementUnit/get-measurement-units";
                var response = await GenericClient.Client.GetAsync(apiUrl);
                if (response.IsSuccessStatusCode)
                {
                    var jsonResponse = await response.Content.ReadAsStringAsync();
                    var brandList = JsonSerializer.Deserialize<List<MeasurementUnit>>(jsonResponse, new JsonSerializerOptions
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
