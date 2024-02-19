using System.Linq.Expressions;

namespace Jevellery.Repositories.Abstract
{
    public interface IRepository<T> where T : class
    {
        Task<List<T>> GetAll();
        Task<T> Get(Expression<Func<T, bool>> expression);
        Task Add(T entity);
        Task Update(T entity);
        Task Delete(T entity);
    }
}
