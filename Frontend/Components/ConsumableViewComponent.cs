using Microsoft.AspNetCore.Mvc;
using System.Net.Http;
using System.Text.Json;
using System.Threading.Tasks;
using Frontend.Models;
using System.Collections.Generic;

namespace Frontend.Components
{
    public class ConsumableViewComponent : ViewComponent
    {
        private readonly HttpClient _httpClient;

        public ConsumableViewComponent(HttpClient httpClient)
        {
            _httpClient = httpClient;
        }

        public async Task<IViewComponentResult> InvokeAsync()
        {
            string consumableUrl = "api/Consumable/get-consumables";
            string jsonResponse = await GetDatas(consumableUrl);
            var consumables = new List<Consumable>();

            if (jsonResponse != null)
            {
                consumables = JsonSerializer.Deserialize<List<Consumable>>(jsonResponse, new JsonSerializerOptions
                {
                    PropertyNameCaseInsensitive = true
                });
            }

            int consumableCount = consumables.Count > 0 ? consumables.Count : 0;

            return View(consumableCount);
        }

        private async Task<string> GetDatas(string uri)
        {
            var response = await GenericClient.Client.GetAsync(uri);
            if (response.IsSuccessStatusCode)
                return await response.Content.ReadAsStringAsync();
            return null;
        }
    }
}
