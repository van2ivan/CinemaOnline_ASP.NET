using System.Linq.Expressions;

namespace CinemaOnline.Data.Base
{
    public interface IEntityBaseRepository<T> where T: class, IEntityBase, new()
    {
        public Task<IEnumerable<T>> GetAllAsync();
        public Task<IEnumerable<T>> GetAllAsync(params Expression<Func<T,object>>[] includeProperties);
        public Task<T> GetByIdAsync(int id);
        public Task<T> GetByIdAsync(int id, params Expression<Func<T,object>>[] includeProperties);
        public Task AddAsync(T data);
        public Task RemoveAsync(int id);
        public Task UpdateAsync(int id, T data);
    }
}
