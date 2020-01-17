using Auth.Infrastructure.Data;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Expressions;

namespace Auth.Infrastructure.Repository
{
    public class RepositoryBase<T> : IRepository<T> where T : class
    {

        protected WebAppContext dbContext;
        private readonly DbSet<T> dbSet;


        protected RepositoryBase(WebAppContext dbContext)
        {
            this.dbContext = dbContext;
            dbSet = dbContext.Set<T>();
        }

        public virtual IEnumerable<T> Get()
        {
            return dbSet.ToList();
        }
        public void Create(T entity)
        {
            dbSet.Add(entity);
        }

        public void Delete(T entity)
        {
            dbSet.Remove(entity);
        }
        public void Update(T entity)
        {
            dbSet.Update(entity);
        }

        public IEnumerable<T> GetWhere(Expression<Func<T, bool>> filter)
        {
            return dbSet.Where(filter).AsEnumerable();
        }
        public void AddRange(List<T> ls)
        {
            dbSet.AddRange(ls);
        }
    }
    public interface IRepository<T> where T : class
    {
        // Get an entity using delegate
        IEnumerable<T> Get();
        void Create(T entity);
        void Delete(T entity);
        IEnumerable<T> GetWhere(Expression<Func<T, bool>> filter);
        void Update(T entity);
        void AddRange(List<T> entity);
    }
}