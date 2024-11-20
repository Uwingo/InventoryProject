using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Repositories.Contracts
{
    public interface IRepositoryBase<T>
    {
        Task Create(T entity);
        Task<List<T>> Read(bool trackChanges);
        Task Update(T entity);
        Task Delete(T entity);
        Task<T> GetById(int id);
    }
}
