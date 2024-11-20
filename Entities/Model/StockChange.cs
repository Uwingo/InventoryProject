using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Entities.Model
{
    public class StockChange : AuditableEntity
    {
        public int StockChangeId { get; set; }
        public int Count { get; set; }
        public bool IsTransfer { get; set; }
        public Warehouse Warehouse { get; set; }
        [ForeignKey("Warehouse")]
        public int? WarehouseId_FK { get; set; }

        public InventoryStock Inventory { get; set; }
        [ForeignKey("Inventory")]
        public int? InventoryId_FK { get; set; }
    }
}
