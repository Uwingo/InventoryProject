using Microsoft.AspNetCore.Mvc;
using Newtonsoft.Json;
using System.Text.Json;
using System.Text;
using Frontend.Models;
using Humanizer;

namespace Frontend.Controllers
{
    public class InventoryController : Controller
    {
        private readonly HttpClient _httpClient;
        public InventoryController(HttpClient httpClient)
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
                string productUrl = "api/Product/get-products";
                string inventoryUrl = "api/InventoryStock/get-inventories";
                string warehouseUrl = "api/Warehouse/get-warehouses";

                string jsonResponseInventory = await GetDatas(inventoryUrl);
                string jsonResponseProduct = await GetDatas(productUrl);
                string jsonResponseWarehouse = await GetDatas(warehouseUrl);


                if (jsonResponseProduct != null || jsonResponseProduct != null)
                {
                    var inventories = new List<Inventory>();
                    if (jsonResponseProduct != null)
                    {
                        inventories = System.Text.Json.JsonSerializer.Deserialize<List<Inventory>>(jsonResponseInventory, new JsonSerializerOptions
                        {
                            PropertyNameCaseInsensitive = true
                        });
                    }
                    else inventories = null;

                    var products = System.Text.Json.JsonSerializer.Deserialize<List<Product>>(jsonResponseProduct, new JsonSerializerOptions
                    {
                        PropertyNameCaseInsensitive = true
                    });
                    var warehouses = System.Text.Json.JsonSerializer.Deserialize<List<Warehouse>>(jsonResponseWarehouse, new JsonSerializerOptions
                    {
                        PropertyNameCaseInsensitive = true
                    });

                    return View("Index", Tuple.Create(inventories, products, warehouses));
                }

                return View();

            }
            catch (Exception ex)
            {

                return View("Error");
            }


        }

        [HttpPost]
        public async Task Create([FromBody] InventoryConsumable inventoryConsumable)
        { 
            try
            {
                if (ModelState.IsValid)
                {
                    Inventory invDto = new Inventory();
                    invDto.Stock = inventoryConsumable.Stock;
                    invDto.EndingWarrantyDate = inventoryConsumable.EndingWarrantyDate;
                    invDto.InventoryStockId = inventoryConsumable.InventoryId;
                    invDto.ProductId_FK = inventoryConsumable.ProductId_FK;
                    //invDto.SerialNumber = inventoryConsumable.SerialNumber;
                    invDto.BeginningWarrantyDate = inventoryConsumable.BeginningWarrantyDate;
                    invDto.InventoryDescription = inventoryConsumable.InventoryDescription;
                    invDto.WarehouseId_FK = inventoryConsumable.WarehouseId_FK;
                    var jsonData = JsonConvert.SerializeObject(invDto);
                    var content = new StringContent(jsonData, Encoding.UTF8, "application/json");
                    await GenericClient.Client.PostAsync("api/InventoryStock/create-inventory", content);
                   
                    if (inventoryConsumable.ConsumableDescription is not null)
                    {
                        //En son kaydedilen envanteri getir
                        string inventoryUrl = "api/InventoryStock/get-inventories";
                        string jsonResponseInventory = await GetDatas(inventoryUrl);
                        if (jsonResponseInventory != null)
                        {
                            try
                            {
                                var inventories = System.Text.Json.JsonSerializer.Deserialize<List<Inventory>>(jsonResponseInventory, new JsonSerializerOptions
                                {
                                    PropertyNameCaseInsensitive = true
                                });
                                var inventoryId = inventories.OrderBy(w => w.InventoryStockId).Last().InventoryStockId;

                                //Envantere ait sarf malzeme kaydı
                                Consumable cnsDto = new Consumable();
                                cnsDto.InventoryStockId_FK = inventoryId;
                                cnsDto.ProductId_FK = inventoryConsumable.ConsumableProductId_FK;
                                cnsDto.ConsumableDescription = inventoryConsumable.ConsumableDescription;
                                cnsDto.Count = inventoryConsumable.Count;
                                var consumableData = JsonConvert.SerializeObject(cnsDto);
                                var consumableContent = new StringContent(consumableData, Encoding.UTF8, "application/json");
                                await GenericClient.Client.PostAsync("api/Consumable/create-consumable", consumableContent);
                            }
                            catch (Exception ex)
                            {

                                throw;
                            }
                          
                        }
                        
                    }


                }
            }
            catch (Exception ex)
            {

                throw;
            }
            





        }
        [HttpPost]
        public async Task Update([FromBody] Inventory inventory)
        {
            if (ModelState.IsValid)
            {
                var jsonData = JsonConvert.SerializeObject(inventory);
                var content = new StringContent(jsonData, Encoding.UTF8, "application/json");
                await GenericClient.Client.PostAsync("api/InventoryStock/update-inventory", content);

            }


        }
        [HttpPost]
        public async Task Delete([FromBody] int id)
        {
            if (ModelState.IsValid)
            {
                await GenericClient.Client.DeleteAsync("api/InventoryStock/delete-inventory/" + id);
            }
        }
    }
}
