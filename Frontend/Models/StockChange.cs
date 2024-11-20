using System.ComponentModel.DataAnnotations;

namespace Frontend.Models
{
    public class StockChange
    {
        public int StockChangeId { get; set; }
        public int Count { get; set; }
        public bool IsTransfer { get; set; }
        public int? WarehouseId_FK { get; set; }
        public int? InventoryId_FK { get; set; }
    }
}
