using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Entities.ModelDto
{
    public class ProductTypeDto
    {
        public int ProductTypeId { get; set; }
        public string ProductTypeName { get; set; }
        public string ProductTypeDescription { get; set; }
        public int MeasurementUnitId_FK { get; set; }
    }
}
