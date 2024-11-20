using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Entities.ModelDto
{
    public class StockChangeDto : AuditableEntityDto
    {
        public int StockChangeId { get; set; }
        public int Count { get; set; }
        public bool IsTransfer { get; set; }
        public int WarehouseId_FK { get; set; }
        public int InventoryId_FK { get; set; }
    }
}
