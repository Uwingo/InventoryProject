using System.ComponentModel.DataAnnotations.Schema;

namespace Frontend.Models
{
    public class Product
    {
        public int? ProductId { get; set; }
        public string? Barcode { get; set; }
        public string? ProductName { get; set; }
        public string? PurchaseNumber { get; set; }
        public bool Status { get; set; }
        public int? Count { get; set; }
        public string? SerialNumber { get; set; }
        public bool? IsConsumables { get; set; }
        public int? StockNumber { get; set; }

        public int? ModelId_FK { get; set; }        
        public int? SupplierId_FK { get; set; }       
        public int? ProductTypeId_FK { get; set; }
    }
}
