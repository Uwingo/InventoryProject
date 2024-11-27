using Frontend.Models;
using Microsoft.AspNetCore.Mvc;
using System.Text.Json;

namespace Frontend.Components
{
    public class SupplierViewComponent : ViewComponent
    {
        private readonly HttpClient _httpClient;

        public SupplierViewComponent(HttpClient httpClient)
        {
            _httpClient = httpClient;
        }

        public async Task<IViewComponentResult> InvokeAsync()
        {
            int supplierCount = 0;

            try
            {
                string apiUrl = "api/Supplier/get-suppliers";
                var response = await GenericClient.Client.GetAsync(apiUrl);
                if (response.IsSuccessStatusCode)
                {
                    var jsonResponse = await response.Content.ReadAsStringAsync();
                    var supplierList = JsonSerializer.Deserialize<List<Supplier>>(jsonResponse, new JsonSerializerOptions
                    {
                        PropertyNameCaseInsensitive = true
                    });

                    supplierCount = supplierList?.Count ?? 0;
                }
            }
            catch (Exception ex)
            {
                supplierCount = 0;
            }

            return View("Default", supplierCount);
        }
    }
}
