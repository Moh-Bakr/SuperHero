using System.Linq.Expressions;
using Microsoft.EntityFrameworkCore;

namespace SuperHero.DAL;

public class CrudRepository<T> : ICrudRepository<T> where T : class
{
   private readonly ApplicationDbContext _context;

   public CrudRepository(ApplicationDbContext context)
   {
      _context = context;
   }

   # region Create,Delete methods

   public async Task<T> AddAsync(T entity)
   {
      await _context.Set<T>().AddAsync(entity);
      await _context.SaveChangesAsync();
      return entity;
   }

   public async Task<bool> DeleteAsync(int id)
   {
      var entity = await _context.Set<T>().FindAsync(id);
      if (entity == null) return false;

      _context.Set<T>().Remove(entity);
      await _context.SaveChangesAsync();
      return true;
   }

   # endregion

   # region Get methods

   public async Task<IEnumerable<T>> GetByIdAsync(
      String id,
      string columnName,
      int pageNumber,
      int pageSize,
      Expression<Func<T, object>> orderBy = null)
   {
      pageNumber = Math.Max(1, pageNumber);
      pageSize = Math.Max(1, Math.Min(pageSize, 10));

      IQueryable<T> query = _context.Set<T>();

      query = query.Where(entity => EF.Property<T>(entity, columnName).Equals(id));

      if (orderBy != null)
      {
         query = query.OrderBy(orderBy);
      }
      else
      {
         query = query.OrderBy(entity => EF.Property<T>(entity, columnName));
      }

      int skip = (pageNumber - 1) * pageSize;
      query = query.Skip(skip).Take(pageSize);

      return await query.ToListAsync();
   }

   public bool FindValuesByExpressionAsync(Expression<Func<T, bool>> predicate)
   {
      return _context.Set<T>().Any(predicate);
   }

   public async Task<T> FindByIdAsync(int id)
   {
      return await _context.Set<T>().FirstOrDefaultAsync(e => EF.Property<T>(e, "Id").Equals(id));
   }

   # endregion
}
