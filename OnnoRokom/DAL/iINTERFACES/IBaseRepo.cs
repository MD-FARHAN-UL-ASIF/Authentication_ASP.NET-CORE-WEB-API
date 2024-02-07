using System;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Expressions;
using System.Text;
using System.Threading.Tasks;

namespace DAL.iINTERFACES
{
    public interface IBaseRepo<T> where T : class
    {
        Task<T> GetById(object id);

        Task<List<T>> GetAll();

        Task<List<T>> Find(Expression<Func<T, bool>> expression);

        IQueryable<T> query();

        void Add(T entity);

        void Remove(T entity);

        void Update(T entity);

        Task<bool> SaveChangesAsync(T entity);
    }
}
