
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Entities.Model
{
    public class Brand :AuditableEntity
    {
        public int BrandId { get; set; }
        public string BrandName { get; set; }
       
    }
}
