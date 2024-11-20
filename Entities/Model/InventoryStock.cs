using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Entities.Model
{
    public class InventoryStock : AuditableEntity
    {
        public int InventoryStockId { get; set; }
        public Product? Product { get; set; }
        [ForeignKey("Product")]
        public int? ProductId_FK { get; set; }
        public int Stock { get; set; }
        public Warehouse? Warehouse { get; set; }
        [ForeignKey("Warehouse")]
        public int? WarehouseId_FK { get; set; }
        public string? SerialNumber { get; set; }
        public DateTime BeginningWarrantyDate { get; set; }
        public DateTime EndingWarrantyDate { get; set; }
        public string? InventoryDescription { get; set; }
    
    }
}
