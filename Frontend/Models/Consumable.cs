using System.ComponentModel.DataAnnotations.Schema;

namespace Frontend.Models
{
    public class Consumable
    {
        public int ConsumableId { get; set; }
        public int Count { get; set; }
        public string ConsumableDescription { get; set; }
        public int ProductId_FK { get; set; }
        public int InventoryStockId_FK { get; set; }
    }
}
