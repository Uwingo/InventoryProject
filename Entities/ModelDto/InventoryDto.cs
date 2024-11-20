using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Entities.ModelDto
{
    public class InventoryDto
    {
        public int InventoryStockId { get; set; }
        public int ProductId_FK { get; set; }
        public int Stock { get; set; }
        public int WarehouseId_FK { get; set; }
        public string? SerialNumber { get; set; }
        public DateTime BeginningWarrantyDate { get; set; }
        public DateTime EndingWarrantyDate { get; set; }
        public string InventoryDescription { get; set; }
     
    }
}
