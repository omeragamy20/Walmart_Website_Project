using Ecommerce.Application.Contracts;
using Ecommerce.Context;
using Ecommerce.Models;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Data.Entity;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using DbContext = Microsoft.EntityFrameworkCore.DbContext;

namespace Ecommerce.Infrastructure
{
    public class GenericRepository<TEntity, TId> : IGenericReposatiry<TEntity, TId> where TEntity : BaseEntity<TId>
    {
        private readonly System.Data.Entity.DbContext _context;
        private readonly Microsoft.EntityFrameworkCore.DbSet<TEntity> _dbSet;


        
        /// //////////
        ///      حل الخطا وتكملة الحل
        /// 
        /// 
        
        public GenericRepository(DbContext context1)
        {
            DbContext context = context1;
           // _context = context;
            _dbSet = context.Set<TEntity>();
        }

        public async Task<TEntity> CreateAsync(TEntity entity)
        {
            await _dbSet.AddAsync(entity);
            return entity;
        }

        public async Task<TEntity> UpdateAsync(TEntity entity)
        {
            _dbSet.Update(entity);
            return entity;
        }

        public async Task<TEntity> DeleteAsync(TEntity entity)
        {
            _dbSet.Remove(entity);
            return entity;
        }

        public async Task<IQueryable<TEntity>> GetAllAsync()
        {
            return await Task.FromResult(_dbSet.AsQueryable());
        }

        public async ValueTask<TEntity> GetOneAsync(TId id)
        {
            return await _dbSet.FindAsync(id);
        }

        public async Task<int> SaveChanges()
        {
            return await _context.SaveChangesAsync();
        }



    }

}