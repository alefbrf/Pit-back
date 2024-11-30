using System.Linq.Expressions;

namespace CoffeeBreak.Application.Common.Interfaces.Repositories
{
    public interface IBaseRepository<T> where T : class
    {
        IQueryable<T> FilterQuery(Expression<Func<T, Boolean>> filter);
        T Insert(T entity, bool autoSave = false);
        List<T> Insert(List<T> entities, bool autoSave = false);
        void Delete(T entity);
        void DeleteWhere(Expression<Func<T, Boolean>> filter);
        List<T> GetAll();
        List<T> GetPaged(int page, int limit);
        List<T> GetPagedByFilter(int page, int limit, Expression<Func<T, Boolean>> filter);
        T? GetById(int id);
        void Commit();
    }
}
