using Common.Entities;
using System.Linq.Expressions;

namespace DataAccess.Repositories.Concrete
{
    public interface IRepository<T> where T : BaseEntity
    {
        Task<List<T>> GetAllAsync();
        Task<T> GetAsync(int id);
        Task CreateAsync(T entity);
        void Update(T entity);
        void Delete(T entity);

        public Task RemoveBlogTagsAsync(int blogId);



        // gedib data basede tekrarin olub olmadigini yoxlayir
        Task<bool> IsExistAsync(Expression<Func<T, bool>> expression, bool isTracking = false, params string[] includes);

        //Get by Id relation olanda relation etdiyimiz obyektide gostermek ucun
        Task<T> GetSingleAsync(Expression<Func<T, bool>> expression, params string[] includes);


        IQueryable<T> GetAll(bool isTracking = false, params string[] includes);
        IQueryable<T> GetFiltered(Expression<Func<T, bool>> expression, bool isTracking = false, params string[] includes);
    }
}