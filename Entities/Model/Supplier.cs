using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Entities.Model
{
    public class Supplier:AuditableEntity
    {
        public int SupplierId { get; set; }
        public string SupplierName { get; set; }
        public string ContactInfo { get; set; }
    }
}
