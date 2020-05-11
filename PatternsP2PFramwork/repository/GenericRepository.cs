using System.Data.Entity;
using System.Linq;
using System.Threading.Tasks;
using PatternsP2PFramwork.models;

namespace PatternsP2PFramwork.repository
{
    public class GenericRepository<TEntity> : IGenericRepository<TEntity> where TEntity : class
    {
        private readonly MyAppContext _dbContext;
        
        public GenericRepository(MyAppContext dbContext)
        {
            this._dbContext = dbContext;
        }
        public async Task Create(TEntity entity)
        {
            _dbContext.Set<TEntity>().Add(entity);
            await _dbContext.SaveChangesAsync();
        }

        public async Task Delete(int id)
        {
            var entity = await GetById(id);
            _dbContext.Set<TEntity>().Remove(entity);
        }

        public IQueryable<TEntity> GetAll()
        {
            //we use AsNoTracking because we only read entities
            return _dbContext.Set<TEntity>().AsNoTracking();
        }

        public async Task<TEntity> GetById(int id)
        {
            return await _dbContext.Set<TEntity>()
                .FindAsync(id);
        }

        public async Task Update(int id, TEntity entity)
        {
            _dbContext.Entry(entity).State=EntityState.Modified;//not calling dbset
            await _dbContext.SaveChangesAsync();

        }
    }
}
