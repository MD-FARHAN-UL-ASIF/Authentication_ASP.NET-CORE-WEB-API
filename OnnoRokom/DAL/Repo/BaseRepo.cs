using DAL.EF;
using DAL.iINTERFACES;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Expressions;
using System.Text;
using System.Threading.Tasks;

namespace DAL.Repo
{
    public class BaseRepo<T> : IBaseRepo<T> where T : class
    {
        private readonly DataContext _context;

        public BaseRepo(DataContext context)
        { 
            _context = context;
        }

        public void Add(T entity)
        {
            _context.Set<T>().Add(entity);
        }

        public async Task<List<T>> Find(Expression<Func<T, bool>> expression)
        {
            var data = await _context.Set<T>().Where(expression).ToListAsync();
            return data;
        }

        public async Task<List<T>> GetAll()
        {
            return await _context.Set<T>().ToListAsync();
        }

        public async Task<T> GetById(object id)
        {
            return await _context.Set<T>().FindAsync(id);
        }

        public void Remove(T entity)
        {
            _context.Set<T>().Remove(entity);
        }

        public async Task<bool> SaveChangesAsync(T entity)
        {
            return await _context.SaveChangesAsync() > 0;
        }

        public void Update(T entity)
        {
            _context.Set<T>().Update(entity);
        }

        public IQueryable<T> query()
        {
            return _context.Set<T>().AsQueryable();
        }
    }
}
