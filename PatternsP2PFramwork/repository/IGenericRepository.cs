using System.Linq;
using System.Threading.Tasks;

namespace PatternsP2PFramwork.repository
{
    public interface IGenericRepository<TEntity> where TEntity:class
    {
        IQueryable<TEntity> GetAll();
        Task<TEntity> GetById(int id);
        Task Create(TEntity entity);
        Task Update(int id, TEntity entity);
        Task Delete(int id);
    }
}
