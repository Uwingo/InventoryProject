namespace Frontend.Models
{
    public class InventoryToWarehouse
    {
        public int InventoryStockId { get; set; }
        public int WarehouseId { get; set; }
        public int? Stock { get; set; }
        public string? WarehouseName { get; set; }
    }
}
