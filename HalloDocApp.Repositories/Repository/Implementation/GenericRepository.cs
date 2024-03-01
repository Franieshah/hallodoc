using HalloDocApp.Entities.DataContext;
using HalloDocApp.Repositories.Repository.Interface;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Expressions;
using System.Text;
using System.Threading.Tasks;

namespace HalloDocApp.Repositories.Repository.Implementation
{
    public class GenericRepository<T> : IGenericRepository<T> where T : class
    {
        private readonly ApplicationDBContext _dbContext;
        internal DbSet<T> dbSet;

        public GenericRepository(ApplicationDBContext dbContext)
        {
            _dbContext = dbContext;
            this.dbSet = _dbContext.Set<T>();
        }

        public async Task Add(T entity)
        {
            await dbSet.AddAsync(entity);
        }

        public void Delete(T entity)
        {
            dbSet.Remove(entity);
        }

        public IEnumerable<T> GetAll()
        {
            return dbSet.ToList();
        }

        public IEnumerable<T> GetAll(Expression<Func<T, bool>> expression)
        {
            return dbSet.Where(expression).ToList();
        }

        public T GetFirstOrDefault(Expression<Func<T, bool>> expression)
        {
            return dbSet.FirstOrDefault(expression);
        }

        public void Save()
        {
            _dbContext.SaveChanges();
        }

        public void Update(T entity)
        {
            dbSet.Update(entity);
        }
    }
}
