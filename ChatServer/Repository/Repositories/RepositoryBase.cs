using ChatServer.EF;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace ChatServer.Repositories
{
    public abstract class RepositoryBase<T> : IRepositoryBase<T> where T : class
    {
        private ApplicationContext _db { get; set; }

        public RepositoryBase(ApplicationContext db)
        {
            _db = db;
        }

        public async virtual Task<IEnumerable<T>> GetAllAsync()
        {
            return await _db.Set<T>().ToListAsync();
        }

        public async virtual Task<T> GetByIdAsync(Guid id)
        {
            return await _db.Set<T>().FindAsync(id);
        }

        public void Create(T entity)
        {
            _db.Set<T>().Add(entity);
        }

        public void Update(T entity)
        {
            _db.Set<T>().Update(entity);
        }

        public void Delete(int id)
        {
            T entity = _db.Set<T>().Find(id);
            if (entity != null)
            {
                _db.Set<T>().Remove(entity);
            }
        }
    }
}
