using System.Linq.Expressions;

namespace FoodBox.Core.Services
{
    public interface IService<TEntity> where TEntity : class
    {
        List<TEntity> List();
        List<TEntity> List(Expression<Func<TEntity, bool>> expression);
        Task<IEnumerable<TEntity>> GetAll();
        Task<IEnumerable<TEntity>> GetAll(Expression<Func<TEntity, bool>> predicate);
        Task<TEntity> GetById(Guid id);
        Task<TEntity> Create(TEntity entity);
        Task Update(TEntity entity);
        Task Delete(TEntity entity);
    }
}
