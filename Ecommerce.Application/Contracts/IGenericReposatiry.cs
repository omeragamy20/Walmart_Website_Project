using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Ecommerce.Application.Contracts
{
    public interface IGenericReposatiry<TEntity, TId>
    {
        public Task<TEntity>CreateAsync(TEntity Entity);

        public Task<TEntity>UpdateAsync(TEntity Entity);

        public Task<TEntity>DeleteAsync(TEntity Entity);
        public Task<IQueryable<TEntity>> GetAllAsync();
        public ValueTask<TEntity> GetOneAsync(TId Id);


        public Task<int> SaveChanges();

    }
}
