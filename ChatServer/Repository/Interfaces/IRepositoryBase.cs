using System;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace ChatServer.Repositories
{
    public interface IRepositoryBase<T>
    {
        Task<IEnumerable<T>> GetAllAsync();
        Task<T> GetByIdAsync(Guid id);
        void Create(T entity);
        void Update(T entity);
        void Delete(int id);
    }
}
