using Frontend.Models;
using Microsoft.AspNetCore.Mvc;
using System.Text.Json;

namespace Frontend.Components
{
    [ViewComponent]
    public class InventoryCardViewComponent : ViewComponent

    {
        private readonly HttpClient _httpClient;

        public InventoryCardViewComponent(HttpClient httpClient)
        {
            _httpClient = httpClient;
        }

        public async Task<IViewComponentResult> InvokeAsync()
        {
            string productUrl = "api/Product/get-products";
            string inventoryUrl = "api/InventoryStock/get-inventories";
            string warehouseUrl = "api/Warehouse/get-warehouses";

            var jsonResponseInventory = await GetDatas(inventoryUrl);
            var jsonResponseProduct = await GetDatas(productUrl);
            var jsonResponseWarehouse = await GetDatas(warehouseUrl);

            var inventories = System.Text.Json.JsonSerializer.Deserialize<List<Inventory>>(jsonResponseInventory, new JsonSerializerOptions
            {
                PropertyNameCaseInsensitive = true
            }) ?? new List<Inventory>();

            var products = System.Text.Json.JsonSerializer.Deserialize<List<Product>>(jsonResponseProduct, new JsonSerializerOptions
            {
                PropertyNameCaseInsensitive = true
            }) ?? new List<Product>();

            var warehouses = System.Text.Json.JsonSerializer.Deserialize<List<Warehouse>>(jsonResponseWarehouse, new JsonSerializerOptions
            {
                PropertyNameCaseInsensitive = true
            }) ?? new List<Warehouse>();

            return View(Tuple.Create(inventories, products, warehouses));
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
