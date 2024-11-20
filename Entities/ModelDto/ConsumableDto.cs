using Entities.Model;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Entities.ModelDto
{
    public class ConsumableDto
    {
        public int ConsumableId { get; set; }
        public int Count { get; set; }
        public string ConsumableDescription { get; set; }
        public int ProductId_FK { get; set; }
        public int InventoryStockId_FK { get; set; }

    }
}
