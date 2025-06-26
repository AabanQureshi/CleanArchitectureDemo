
namespace Application.Abstraction.IRepositories
{
    public interface IGenericRepository<TEntity, TKey>
        where TEntity : class
        where TKey : notnull
    {
        Task AddAsync(TEntity entity);
        Task<bool> ExistsAsync(TKey id);
        Task<IEnumerable<TEntity>> GetAllAsync();
        Task<TEntity?> GetByIdAsync(TKey id);
        void Remove(TEntity entity);
        Task SaveChangesAsync();
        void Update(TEntity entity);
    }
}