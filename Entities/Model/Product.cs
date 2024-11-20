using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Entities.Model
{
    public class Product:AuditableEntity
    {
        public int ProductId { get; set; }
        public string Barcode { get; set; }
        public string ProductName { get; set; }
        public string PurchaseNumber { get; set; }
        public bool Status { get; set; }
        public bool IsConsumables { get; set; }
        public int StockNumber { get; set; }
        //public string? SerialNumber { get; set; }
        public int Count { get; set; }

        public Model Model { get; set; }
        [ForeignKey("Model")]
        public int ModelId_FK { get; set; }
        public Supplier Supplier { get; set; }
        [ForeignKey("Supplier")]
        public int SupplierId_FK { get; set; }
        public ProductType ProductType { get; set; }
        [ForeignKey("ProductType")]
        public int ProductTypeId_FK { get; set; }
        
    }
}
