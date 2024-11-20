using Entities.Model;
using Repositories.Contracts;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Repositories.EFCore
{
    public class RepositoryWarehouse:RepositoryBase<Warehouse>,IRepositoryWarehouse
    {
        private readonly RepositoryContext _context;
        public RepositoryWarehouse(RepositoryContext context) : base(context)
        {
            _context = context;
        }
    }
}
