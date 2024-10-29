using Ecommerce.Application.Contracts;
using Ecommerce.Context;
using Ecommerce.Models;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Ecommerce.Infrastructure
{
    public class GenricReposatiry<TEntity, TId> : IGenericReposatiry<TEntity, TId> where TEntity : BaseEntity<TId>
    {
        private readonly EcommerceContext Context;

        private readonly DbSet<TEntity> dbset;

        public GenricReposatiry(EcommerceContext _Context)
        {

            Context = _Context;
            dbset = Context.Set<TEntity>();


        }

        public async Task<TEntity> CreateAsync(TEntity Entity)
        {
            var x = (await dbset.AddAsync(Entity)).Entity;
            Debug.WriteLine(x.Id);
            return x ;
        }

        public Task<TEntity> UpdateAsync(TEntity Entity)
        {
            return Task.FromResult(dbset.Update(Entity).Entity);
        }

        public async Task<TEntity> DeleteAsync(TEntity Entity)
        {
            return Context.Remove(Entity).Entity;

        }

        public Task<IQueryable<TEntity>> GetAllAsync()
        {
            return Task.FromResult(dbset.Select(p => p));
        }

        public Task<IQueryable<TEntity>> GetAllWithDeleteAsync()
        {
            return Task.FromResult(dbset.Select(p => p));
        }

        public ValueTask<TEntity> GetOneAsync(TId Id)
        {
            return dbset.FindAsync(Id);
        }



        public async Task<int> SaveChanges()
        {
            return await Context.SaveChangesAsync();
        }
    }
}
