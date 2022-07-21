using Bernhoeft.DataContext;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Expressions;
using System.Threading.Tasks;


namespace Bernhoeft.Repository.Base
{

        public class Repository<T> where T : class
        {
            public readonly Context _context;
            public Repository(Context context)
            {
                _context = context;
            }
            public async void Add(T entity)
            {
                await _context.Set<T>().AddAsync(entity);
                _context.SaveChanges();

            }

            public void Update(T entity)
            {
                _context.Set<T>().Update(entity);
                _context.SaveChanges();

            }

            public async void AddRange(IEnumerable<T> entities)
            {
                await _context.Set<T>().AddRangeAsync(entities);
                _context.SaveChanges();

            }
            public IEnumerable<T> Find(Expression<Func<T, bool>> expression)
            {
                return _context.Set<T>().Where(expression);
            }
            public async Task<IEnumerable<T>> GetAll()
            {
                return await _context.Set<T>().ToListAsync();
            }
            public async Task<T> GetById(int id)
            {
                return await _context.Set<T>().FindAsync(id);
            }
            public void Remove(T entity)
            {
                _context.Set<T>().Remove(entity);
                _context.SaveChanges();

            }
            public void RemoveRange(IEnumerable<T> entities)
            {
                _context.Set<T>().RemoveRange(entities);
                _context.SaveChanges();

            }
        }

}