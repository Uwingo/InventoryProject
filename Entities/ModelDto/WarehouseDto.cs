using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Entities.ModelDto
{
    public class WarehouseDto
    {
        public int WarehouseId { get; set; }
        public string WarehouseName { get; set; }
        public string UnitName { get; set; }
        public int? MainWarehouse { get; set; }
        public bool IsMainWarehouse { get; set; }
    }
}
