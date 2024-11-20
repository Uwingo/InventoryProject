using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Entities.Model
{
    public class Consumable : AuditableEntity
    {
        public int ConsumableId { get; set; }
        public int Count { get; set; }
        public string ConsumableDescription { get; set; }
        public Product? Product { get; set; }
        [ForeignKey("Product")]
        public int ProductId_FK { get; set; }
        public InventoryStock? Inventory { get; set; }
        [ForeignKey("InventoryStock")]
        public int InventoryStockId_FK { get; set; }

    }
}
