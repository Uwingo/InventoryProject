namespace Frontend.Models
{
    public class InventoryConsumable
    {
        public int InventoryId { get; set; }
        public int ProductId_FK { get; set; }
        public int Stock { get; set; }
        public int WarehouseId_FK { get; set; }
       // public string? SerialNumber { get; set; }
        public DateTime BeginningWarrantyDate { get; set; }
        public DateTime EndingWarrantyDate { get; set; }
        public string? InventoryDescription { get; set; }
        public int Count { get; set; }
        public string? ConsumableDescription { get; set; }
        public int ConsumableProductId_FK { get; set; }
        
    }
}
