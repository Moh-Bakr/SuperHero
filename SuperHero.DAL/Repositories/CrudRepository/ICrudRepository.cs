using System.Linq.Expressions;

namespace SuperHero.DAL;

public interface ICrudRepository<T> where T : class
{
   Task<T> AddAsync(T entity);
   Task<T> UpdateAsync(T entity);
   Task<bool> DeleteAsync(int id);

   Task<IEnumerable<T>> GetAllAsync(Expression<Func<T, object>> orderBy, int page = 1,
      int size = 10);

   Task<T> GetByIdAsync(int id);
   Task<T> FindAsync(Expression<Func<T, bool>> match);
}
