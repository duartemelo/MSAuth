﻿using MSGym.Domain.Entities;

namespace MSGym.Domain.Interfaces.Persistence.Repositories
{
    public interface IBaseRepository<T> where T : BaseEntity
    {
        Task<T> AddAsync(T entity);
        void Deactivate(T entity);
        void Delete(T entity);
        IQueryable<T> GetEntity();
        void Update(T entity);
    }
}
