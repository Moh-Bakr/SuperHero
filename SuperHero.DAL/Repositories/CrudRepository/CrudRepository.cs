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

   # region Create, Update, Delete methods

   public async Task<T> AddAsync(T entity)
   {
      await _context.Set<T>().AddAsync(entity);
      await _context.SaveChangesAsync();
      return entity;
   }

   public async Task<T> UpdateAsync(T entity)
   {
      throw new NotImplementedException();
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

   public async Task<IEnumerable<T>> GetAllAsync(
      Expression<Func<T, object>> orderBy = null,
      int page = 1,
      int size = 10)
   {
      page = Math.Max(page, 1);
      size = Math.Max(1, Math.Min(size, 10));
      IQueryable<T> query = _context.Set<T>();

      if (orderBy != null)
      {
         query = query.OrderBy(orderBy);
      }

      int skip = (page - 1) * size;

      query = query.Skip(skip).Take(size);

      return await query.ToListAsync();
   }


   public async Task<T> GetByIdAsync(int id) => await _context.Set<T>().FindAsync(id).AsTask();

   public async Task<T> FindAsync(Expression<Func<T, bool>> match) =>
      await _context.Set<T>().FirstOrDefaultAsync(match);

   # endregion
}
