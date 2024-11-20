namespace Frontend.Models
{
    public class Inventory
    {
        public int InventoryStockId { get; set; }
        public int ProductId_FK { get; set; }
        public string? ProductName { get; set; }
        public int Stock { get; set; }
        public int WarehouseId_FK { get; set; }
        public string? SerialNumber { get; set; }
        public DateTime BeginningWarrantyDate { get; set; }
        public DateTime EndingWarrantyDate { get; set; }
        public string? InventoryDescription { get; set; }
     
    }
}
