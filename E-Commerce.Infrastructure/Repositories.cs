using E_Commerce.Application;
using E_Commerce.Domain.Models;
using E_Commerce.Infrastructure.Data;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.ChangeTracking;
using System.Linq.Expressions;

namespace E_Commerce.Infrastructure
{
    public class Repository<T> : IRepository<T> where T : BaseModel
    {
        StoreContext _context;

        public Repository(StoreContext context)
        {
            _context = context;
        }

        public T Add(T entity)
        {
            _context.Set<T>().Add(entity);
            return entity;
        }


        public IQueryable<T> Get(Expression<Func<T, bool>> predicate)
        {
            return GetAll().Where(predicate);
        }

        


        public IQueryable<T> GetAll()
        {
            return _context.Set<T>()
                .Where(x => !x.IsDeleted)
                .AsNoTracking();
            //return _context.Set<T>().Where(x => !x.Deleted).AsNoTrackingWithIdentityResolution();
        }

        public async Task<T> GetByIDAsync(int id)
        {
            return await _context.Set<T>().FirstOrDefaultAsync(x => x.ID == id);
        }


        public T GetWithTrackinByID(int id)
        {
            return _context.Set<T>()
                .Where(x => !x.IsDeleted && x.ID == id)
                .AsTracking()
                .FirstOrDefault();
        }

        public T First(Expression<Func<T, bool>> predicate)
        {
            return Get(predicate).FirstOrDefault();
        }



        public void UpdateIncluded(T entity, params string[] updatedProperties)
        {
            T local = _context.Set<T>().Local.FirstOrDefault(x => x.ID == entity.ID);

            EntityEntry entityEntry;

            if (local is null)
            {
                entityEntry = _context.Entry(entity);
            }
            else
            {
                entityEntry = _context.ChangeTracker.Entries<T>().FirstOrDefault(x => x.Entity.ID == entity.ID);
            }

            foreach (var property in entityEntry.Properties)
            {
                if (updatedProperties.Contains(property.Metadata.Name))
                {
                    property.CurrentValue = entity.GetType().GetProperty(property.Metadata.Name).GetValue(entity);
                    property.IsModified = true;
                }
            }
        }



        public void Update(T entity)
        {
            _context.Set<T>().Update(entity);
        }



        public void Delete(T entity)
        {
            //_context.Set<T>().Remove(entity);
            entity.IsDeleted = true;
            Update(entity);
        }

        public void Delete(int id)
        {
            T entity = _context.Find<T>(id);
            Delete(entity);
        }

        public void HardDelete(int id)
        {
            //T entity = _context.Find<T>(id);
            //_context.Set<T>().Remove(entity);

            _context.Set<T>().Where(x => x.ID == id).ExecuteDelete();
        }
        public void SaveChanges()
        {
            _context.SaveChanges();
        }

        public IQueryable<T> GetAll(params Expression<Func<T, object>>[] includes)
        {
            IQueryable<T> query = _context.Set<T>().Where(x => !x.IsDeleted).AsNoTracking();

            if (includes != null)
            {
                foreach (var include in includes)
                {
                    query = query.Include(include);
                }
            }

            return query;
        }


    }

}
