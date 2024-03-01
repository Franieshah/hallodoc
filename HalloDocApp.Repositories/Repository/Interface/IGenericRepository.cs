using System;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Expressions;
using System.Text;
using System.Threading.Tasks;

namespace HalloDocApp.Repositories.Repository.Interface
{
    public interface IGenericRepository<T> where T : class
    {
        IEnumerable<T> GetAll();

        IEnumerable<T> GetAll(Expression<Func<T,bool>> expression);

        void Delete(T entity);

        T GetFirstOrDefault(Expression<Func<T, bool>> expression);

        Task Add(T entity);

        void Update(T entity);

        void Save();
    }
}
