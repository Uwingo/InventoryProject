using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Entities.Model
{
    public class ProductType:AuditableEntity
    {
        public int ProductTypeId { get; set; }
        public string ProductTypeName { get; set; }
        public string ProductTypeDescription { get; set; }
       
        public MeasurementUnit MeasurementUnit { get; set; }
        [ForeignKey("MeasurementUnit")] 
        public int MeasurementUnitId_FK { get; set; }
    }
}
