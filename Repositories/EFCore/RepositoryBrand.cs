using Entities.Model;
using Repositories.Contracts;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Repositories.EFCore
{
    public class RepositoryBrand:RepositoryBase<Brand>,IRepositoryBrand
    {
        private readonly RepositoryContext _context;
        public RepositoryBrand(RepositoryContext context) : base(context)
        {
            _context = context;
        }
    }
}
