using Bernhoeft.Repository.Base;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Expressions;
using System.Threading.Tasks;


namespace Bernhoeft.Services.Base
{

        public class Service<T> where T : class
        {
            public Repository<T> repository;

            public Service(Repository<T> _repository)
            {
                repository = _repository;
            }

            public T GetById(int id)
            {
                return repository.GetById(id).Result;
            }
            public IEnumerable<T> GetAll()
            {
                return repository.GetAll().Result;
            }
            public IEnumerable<T> Find(Expression<Func<T, bool>> expression)
            {
                return this.repository.Find(expression);
            }
            public void Add(T entity)
            {
                repository.Add(entity);
            }
            public void Update(T entity)
            {
                repository.Update(entity);
            }

            public void AddRange(IEnumerable<T> entities)
            {
                repository.AddRange(entities);
            }
            public void Remove(T entity)
            {
                repository.Remove(entity);
            }
            public void RemoveRange(IEnumerable<T> entities)
            {
                repository.RemoveRange(entities);
            }

        }
    }
