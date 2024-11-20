namespace Frontend.Models
{
    public class ProductType
    {
        public int ProductTypeId { get; set; }
        public string ProductTypeName { get; set; }
        public string ProductTypeDescription { get; set; }
        public int MeasurementUnitId_FK { get; set; }
    }
}
