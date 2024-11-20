using Microsoft.EntityFrameworkCore;
using Repositories.Contracts;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Repositories.EFCore
{
    public class RepositoryBase<T> : IRepositoryBase<T> where T : class
    {
        protected readonly RepositoryContext _context;
        public RepositoryBase(RepositoryContext context)
        {
            _context = context;
        }
        public async Task Create(T entity) => _context.Set<T>().Add(entity);


        public async Task Delete(T entity)
        {
            _context.Set<T>().Update(entity);
        }

        public async Task<T> GetById(int id)
        {
            var entity = _context.Set<T>().Find(id);
            _context.Entry(entity).State = EntityState.Detached;
            return entity;
        }


        public async Task<List<T>> Read(bool trackChanges) => _context.Set<T>().AsNoTracking().ToList();

        public async Task Update(T entity) => _context.Set<T>().Update(entity);

    }
}
