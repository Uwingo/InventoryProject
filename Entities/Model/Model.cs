using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Entities.Model
{
    public class Model:AuditableEntity
    {
        public int ModelId { get; set; }
        public  string ModelName { get; set; }     

        public Brand Brand { get; set; }
        [ForeignKey("Brand")]
        public  int BrandId_FK { get; set; }
    }
}
