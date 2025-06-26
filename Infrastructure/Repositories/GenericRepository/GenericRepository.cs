using Application.Abstraction.IRepositories;
using Infrastructure.Persistance;
using Microsoft.EntityFrameworkCore;

namespace Infrastructure.Repositories.GenericRepository
{
    public class GenericRepository<TEntity, TKey> : IGenericRepository<TEntity, TKey> where TEntity : class
        where TKey : notnull
    {
        private readonly ApplicationDbContext _context;
        protected DbSet<TEntity> _dbSet;
        public GenericRepository(ApplicationDbContext context)
        {
            _context = context;
            _dbSet = _context.Set<TEntity>();
        }

        public async Task AddAsync(TEntity entity)
        {
            await _dbSet.AddAsync(entity).ConfigureAwait(false);
        }

        public virtual async Task<IEnumerable<TEntity>> GetAllAsync()
        {
            return await _dbSet.AsNoTracking().ToListAsync();
        }
        public virtual async Task<TEntity?> GetByIdAsync(TKey id)
        {
            return await _dbSet.FindAsync(id).ConfigureAwait(false);
        }

        public virtual void Update(TEntity entity)
        {
            _dbSet.Update(entity);
        }
        public virtual void Remove(TEntity entity)
        {
            _dbSet.Remove(entity);
        }
        public async Task SaveChangesAsync()
        {
            await _context.SaveChangesAsync().ConfigureAwait(false);
        }
        public virtual async Task<bool> ExistsAsync(TKey id)
        {
            return await _dbSet.FindAsync(id) != null;
        }


    }
}
