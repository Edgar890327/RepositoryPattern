using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Expressions;
using System.Threading.Tasks;

namespace Repository.SqlRepository
{
    public class SqlRepository<TContext>:ISqlRepository<TContext> where TContext : DbContext
    {
        private readonly TContext _context;
        public SqlRepository(TContext context)
        {
            _context = context;
        }

        private DbSet<TEntity> GetEntitySet<TEntity>() where TEntity : class => _context.Set<TEntity>();

        public TEntity Retrieve<TEntity>(Expression<Func<TEntity, bool>> criteria) where TEntity : class
        {
            return GetEntitySet<TEntity>().FirstOrDefault(criteria);
        }

        public async Task<TEntity> RetrieveAsync<TEntity>(Expression<Func<TEntity, bool>> criteria) where TEntity : class
        {
            return await GetEntitySet<TEntity>().FirstOrDefaultAsync(criteria);
        }

        public IEnumerable<TEntity> Filter<TEntity>(Expression<Func<TEntity, bool>> criteria) where TEntity : class
        {
            return GetEntitySet<TEntity>().Where(criteria);
        }

        public async Task<IEnumerable<TEntity>> FilterAsync<TEntity>(Expression<Func<TEntity, bool>> criteria) where TEntity : class
        {
            return await GetEntitySet<TEntity>().Where(criteria).ToListAsync();
        }

        public TEntity Create<TEntity>(TEntity toCreate) where TEntity : class
        {
            GetEntitySet<TEntity>().Add(toCreate);
            _context.SaveChanges();
            return toCreate;
        }

        public async Task<TEntity> CreateAsync<TEntity>(TEntity toCreate) where TEntity : class
        {
            GetEntitySet<TEntity>().Add(toCreate);
            await _context.SaveChangesAsync();
            return toCreate;
        }

        public bool Update<TEntity>(TEntity toUpdate) where TEntity : class
        {
            GetEntitySet<TEntity>().Attach(toUpdate);
            _context.Entry(toUpdate).State = EntityState.Modified;
            return _context.SaveChanges() > 0;
        }

        public async Task<bool> UpdateAsync<TEntity>(TEntity toUpdate) where TEntity : class
        {
            GetEntitySet<TEntity>().Attach(toUpdate);
            _context.Entry(toUpdate).State = EntityState.Modified;
            return await _context.SaveChangesAsync() > 0;
        }

        public IEnumerable<TEntity> GetAll<TEntity>() where TEntity : class
        {
            return GetEntitySet<TEntity>();
        }

        public async Task<IEnumerable<TEntity>> GetAllAsync<TEntity>() where TEntity : class
        {
            return await GetEntitySet<TEntity>().ToListAsync();
        }

        public async Task<int> GetCountAsync<TEntity>(Expression<Func<TEntity, bool>> criteria) where TEntity : class
        {
            return await GetEntitySet<TEntity>().CountAsync(criteria);
        }
    }
}
