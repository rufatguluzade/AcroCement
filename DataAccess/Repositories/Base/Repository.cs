using Common.Entities;
using DataAccess.Context;
using Microsoft.EntityFrameworkCore;
using System.Linq.Expressions;

namespace DataAccess.Repositories.Concrete
{
    public class Repository<T> : IRepository<T> where T : BaseEntity
    {
        private readonly AppDbContext _context;
        private DbSet<T> _table;

        public Repository(AppDbContext context)
        {
            _context = context;
            _table = _context.Set<T>();
        }

        public async Task CreateAsync(T entity)
        {
            await _table.AddAsync(entity);
        }

        public void Delete(T entity)
        {
            _table.Remove(entity);
        }

        public async Task<List<T>> GetAllAsync()
        {
            return await _table.ToListAsync();
        }

        public async Task<T> GetAsync(int id)
        {
            return await _table.FindAsync(id);
        }

        public void Update(T entity)
        {
            _table.Update(entity);
        }


        public async Task<bool> IsExistAsync(Expression<Func<T, bool>> expression, bool isTracking = false, params string[] includes)
        {
            var query = _table.AsQueryable();
            if (includes is not null && includes.Length > 0)
            {
                foreach (var include in includes)
                {
                    query = query.Include(include);
                }
            }
            return isTracking ? await query.AnyAsync(expression) : await query.AsNoTracking().AnyAsync(expression);
        }



        public IQueryable<T> GetAll(bool isTracking = false, params string[] includes)
        {
            var query = _table.AsQueryable();

            if (includes is not null && includes.Length > 0)
            {
                foreach (var include in includes)
                {
                    query = query.Include(include);
                }
            }
            return isTracking ? query : query.AsNoTracking();

        }

        public IQueryable<T> GetFiltered(Expression<Func<T, bool>> expression, bool isTracking = false, params string[] includes)
        {
            var query = _table.AsQueryable();
            if (includes is not null && includes.Length > 0)
            {
                foreach (var include in includes)
                {
                    query = query.Include(include);
                }
            }

            return isTracking ? query.Where(expression) : query.AsNoTracking().Where(expression);

        }



        public async Task<T> GetSingleAsync(Expression<Func<T, bool>> expression, params string[] includes)
        {
            var query = _table.AsQueryable();
            if (includes is not null && includes.Length > 0)
            {
                foreach (var include in includes)
                {
                    query = query.Include(include);
                }
            }

            return await query.FirstOrDefaultAsync(expression);


        }

        public async Task RemoveBlogTagsAsync(int blogId)
        {
            var blog = await _context.Blogs.Include(b => b.BlogTags)
                                    .FirstOrDefaultAsync(b => b.Id == blogId);

            if (blog != null && blog.BlogTags.Any())
            {
                blog.BlogTags.Clear();
                _context.BlogTags.RemoveRange(blog.BlogTags); // This ensures deletion
                await _context.SaveChangesAsync(); // Save the changes to persist the deletion
            }
        }
    }
}
