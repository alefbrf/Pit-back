using CoffeeBreak.Application.Common.Interfaces.Repositories;
using CoffeeBreak.Infrastructure.Persistence;
using Microsoft.EntityFrameworkCore;
using System.Linq.Expressions;

namespace CoffeeBreak.Infrastructure.Repositories
{
    public class BaseRepository<T> : IBaseRepository<T> where T : class
    {
        private readonly AppDbContext _context;

        public BaseRepository(AppDbContext context)
        {
            _context = context;
        }
        public T Insert(T entity, bool autoSave = false)
        {
            _context.Set<T>().Add(entity);

            if (autoSave) 
            {
                _context.SaveChanges();
            }

            return entity;
        }
        public List<T> Insert(List<T> entities, bool autoSave = false)
        {
            _context.Set<T>().AddRange(entities);
            if (autoSave) 
            {
                _context.SaveChanges();
            }

            return entities;
        }

        public IQueryable<T> FilterQuery(Expression<Func<T, bool>> filter)
        {
            return _context.Set<T>().AsNoTracking().Where(filter);
        }

        public void Delete(T entity)
        {
            _context.Remove(entity);
        }

        public void Delete(List<T> entities, bool autoSave = false)
        {
            _context.Set<T>().AttachRange(entities);

            _context.Set<T>().RemoveRange(entities);
            if (autoSave)
            {
                _context.SaveChanges();
            }
        }

        public void DeleteWhere(Expression<Func<T, bool>> filter)
        {
            _context.Set<T>().Where(filter).ExecuteDelete();
        }

        public List<T> GetAll()
        {
            return _context.Set<T>().ToList();
        }

        public List<T> GetPaged(int page, int limit)
        {
            if (page == 0)
            {
                page = 1;
            }

            if (limit == 0)
            {
                limit = 10;
            }

            var skip = (page - 1) * limit;

            return _context.Set<T>().Skip(skip).Take(limit).ToList();
        }
        public List<T> GetPagedByFilter(int page, int limit, Expression<Func<T, bool>> filter)
        {
            if (page == 0)
            {
                page = 1;
            }

            if (limit == 0)
            {
                limit = 10;
            }

            var skip = (page - 1) * limit;

            return _context.Set<T>().Skip(skip).Take(limit).Where(filter).ToList();
        }

        public T? GetById(int id)
        {
            return _context.Set<T>().Find(id);
        }

        public void Commit()
        {
            _context.SaveChanges();
        }
    }
}
