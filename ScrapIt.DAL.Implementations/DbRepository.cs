using ScrapIt.DAL.Contracts;
using ScrapIt.DAL.Contracts.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Expressions;
using System.Threading.Tasks;

namespace ScrapIt.DAL.Implementations
{
    public class DbRepository: IDbRepository
    {
        private readonly DataContext _context;

        public DbRepository(DataContext context)
        {
            _context = context;
        }

        public async Task<int> SaveChangesAsync()
        {
            return await _context.SaveChangesAsync();
        }

        public async Task<long> Add<T>(T newEntity) where T: class, IEntity
        {
            var entity = await _context.Set<T>().AddAsync(newEntity);
            return entity.Entity.Id;
        }

        public async Task AddRange<T>(IEnumerable<T> newEntities) where T : class, IEntity
        {
            await _context.Set<T>().AddRangeAsync(newEntities);
        }

        public Task<IQueryable<T>> Get<T>(Expression<Func<T, bool>> selector) where T : class, IEntity
        {
            return Task.Run(()=>_context.Set<T>().Where(selector).AsQueryable());
        }

        public Task<IQueryable<T>> GetAll<T>() where T : class, IEntity
        {
            return Task.Run(()=>_context.Set<T>().AsQueryable());
        }

        public Task<T> GetById<T>(long id) where T : class, IEntity
        {
            return Task.Run(() => _context.Set<T>().FirstOrDefault(e => e.Id == id));
        }

        public async Task Remove<T>(T entity) where T : class, IEntity
        {
            await Task.Run(() => _context.Set<T>().Remove(entity));
        }

        public async Task RemoveRange<T>(IEnumerable<T> entities) where T : class, IEntity
        {
            await Task.Run(() => _context.Set<T>().RemoveRange(entities));
        }

        public async Task Update<T>(T entity) where T : class, IEntity
        {
            await Task.Run(() => _context.Set<T>().Update(entity));
        }

        public async Task UpdateRange<T>(IEnumerable<T> entities) where T : class, IEntity
        {
            await Task.Run(() => _context.Set<T>().UpdateRange(entities));
        }
    }
}
