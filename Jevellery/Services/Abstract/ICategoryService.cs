using Jevellery.Models;
using System.Linq.Expressions;

namespace Jevellery.Services.Abstract
{
    public interface ICategoryService
    {
        Task<List<Category>> GetAllAsync();
        Task<Category> Get(Expression<Func<Category, bool>> expression);
        Task AddAsync(Category entity);
        Task UpdateAsync(Category entity);
        Task DeleteAsync(Category entity);
    }
}
