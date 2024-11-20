namespace Frontend.Models
{
    public class WarehouseDetailsViewModel
    {
        public List<Inventory> Inventories { get; set; }
        public List<Warehouse> Warehouses { get; set; }
        public int WarehouseId { get; set; }
        public string WarehouseName { get; set; }
    }
}
