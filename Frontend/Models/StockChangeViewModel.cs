using System.ComponentModel.DataAnnotations;
using System.Diagnostics.Contracts;

namespace Frontend.Models
{
    public class StockChangeViewModel
    {
        public StockChange StockChange { get; set; }
        public string warehouse { get; set; }
        public string inventoriesDes { get; set; }
        public int inventories { get; set; }
    }
}
