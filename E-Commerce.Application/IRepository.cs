using E_Commerce.Domain.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Expressions;
using System.Text;
using System.Threading.Tasks;

namespace E_Commerce.Application
{
    public interface IRepository<T> where T : BaseModel
    {
        T Add(T entity);

        IQueryable<T> GetAll();
        IQueryable<T> Get(Expression<Func<T, bool>> predicate);
        Task<T> GetByIDAsync(int id);
        T GetWithTrackinByID(int id);
        void Update(T entity);
        void UpdateIncluded(T entity, params string[] updatedProperties);
        void Delete(T entity);
        void Delete(int id);
        T First(Expression<Func<T, bool>> predicate);

        void SaveChanges();
        // IDisposable BeginTransaction();
    }

}
