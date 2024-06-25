using Microsoft.EntityFrameworkCore;
using MSAuth.Domain.Entities;
using MSAuth.Domain.Interfaces.Repositories;

namespace MSAuth.Infrastructure.Repositories
{
    public class BaseRepository<T> : IBaseRepository<T> where T : BaseEntity
    {
        private readonly DbContext _context;
        private readonly DbSet<T> _dbSet;

        public BaseRepository(DbContext context)
        {
            _context = context;
            _dbSet = _context.Set<T>();
        }

        public IQueryable<T> GetEntity() { 
            return _dbSet.AsQueryable();
        }

        public async Task<T> AddAsync(T entity)
        {
            entity.DateOfCreation = DateTime.Now;
            await _dbSet.AddAsync(entity);
            return entity;
        }

        public void Update(T entity)
        {
            entity.DateOfModification = DateTime.Now;
            _context.Entry(entity).State = EntityState.Modified;
        }

        public void Delete(T entity)
        {
            _dbSet.Remove(entity);
        }
    }
}
