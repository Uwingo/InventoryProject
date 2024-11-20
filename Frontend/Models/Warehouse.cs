namespace Frontend.Models
{
    public class Warehouse
    {
        public int WarehouseId { get; set; }
        public string WarehouseName { get; set; }
        public string UnitName { get; set; }
        public int? MainWarehouse { get; set; }
        public bool IsMainWarehouse { get; set; }
    }
}
