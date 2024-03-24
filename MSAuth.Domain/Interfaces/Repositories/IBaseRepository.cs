using MSAuth.Domain.Entities;

namespace MSAuth.Domain.Interfaces.Repositories
{
    public interface IBaseRepository<T> where T : BaseEntity
    {
        T Add(T entity);
        void Delete(T entity);
        void Update(T entity);
    }
}