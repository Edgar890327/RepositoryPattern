using System;
using System.Collections.Generic;
using System.Linq.Expressions;
using System.Threading.Tasks;
using Microsoft.EntityFrameworkCore;

namespace Repository.SqlRepository
{
    public interface ISqlRepository<TContext>
         where TContext : DbContext
    {
        TEntity Retrieve<TEntity>(Expression<Func<TEntity, bool>> criteria) where TEntity : class;

        Task<TEntity> RetrieveAsync<TEntity>(Expression<Func<TEntity, bool>> criteria) where TEntity : class;

        IEnumerable<TEntity> Filter<TEntity>(Expression<Func<TEntity, bool>> criteria) where TEntity : class;

        Task<IEnumerable<TEntity>> FilterAsync<TEntity>(Expression<Func<TEntity, bool>> criteria) where TEntity : class;

        TEntity Create<TEntity>(TEntity toCreate) where TEntity : class;

        Task<TEntity> CreateAsync<TEntity>(TEntity toCreate) where TEntity : class;

        bool Update<TEntity>(TEntity toUpdate) where TEntity : class;

        Task<bool> UpdateAsync<TEntity>(TEntity toUpdate) where TEntity : class;

        IEnumerable<TEntity> GetAll<TEntity>() where TEntity : class;

        Task<IEnumerable<TEntity>> GetAllAsync<TEntity>() where TEntity : class;

        Task<int> GetCountAsync<TEntity>(Expression<Func<TEntity, bool>> criteria) where TEntity : class;
    }
}
