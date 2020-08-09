using ScrapIt.DAL.Contracts.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Expressions;
using System.Threading.Tasks;

namespace ScrapIt.DAL.Contracts
{
    public interface IDbRepository
    {
        Task<IQueryable<T>> Get<T>(Expression<Func<T, bool>> selector) where T : class, IEntity;
        Task<IQueryable<T>> GetAll<T>() where T : class, IEntity;
        Task<T> GetById<T>(long id) where T : class, IEntity;

        Task<long> Add<T>(T newEntity) where T : class, IEntity;
        Task AddRange<T>(IEnumerable<T> newEntities) where T : class, IEntity;

        Task Remove<T>(T entity) where T : class, IEntity;
        Task RemoveRange<T>(IEnumerable<T> entities) where T : class, IEntity;

        Task Update<T>(T entity) where T : class, IEntity;
        Task UpdateRange<T>(IEnumerable<T> entities) where T : class, IEntity;

        Task<int> SaveChangesAsync();
    }
}
