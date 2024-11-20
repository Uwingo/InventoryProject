using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Entities.Model
{
    public class MeasurementUnit:AuditableEntity
    {
        public int MeasurementUnitId { get; set; }
        public string MeasurementUnitName { get; set; }
     
    }
}
