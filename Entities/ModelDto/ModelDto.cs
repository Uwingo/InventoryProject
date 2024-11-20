using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Entities.ModelDto
{
    public class ModelDto
    {
        public int ModelId { get; set; }
        public  string ModelName { get; set; }
        public  int BrandId_FK { get; set; }
    }
}
