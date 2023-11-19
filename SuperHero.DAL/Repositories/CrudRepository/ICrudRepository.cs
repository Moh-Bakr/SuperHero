using System.Linq.Expressions;

namespace SuperHero.DAL;

public interface ICrudRepository<T> where T : class
{
   Task<T> AddAsync(T entity);
   Task<bool> DeleteAsync(int id);

   Task<IEnumerable<T>> GetByIdAsync(String id, string columnName, int pageNumber, int pageSize,
      Expression<Func<T, object>> orderBy = null);

   bool FindValuesByExpressionAsync(Expression<Func<T, bool>> predicate);
   Task<T> FindByIdAsync(int id);
}
