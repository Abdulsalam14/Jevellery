using Jevellery.Models;
using System.Linq.Expressions;

namespace Jevellery.Services.Abstract
{
    public interface IProductService
    {
        Task<List<Product>> GetAllAsync();
        Task<Product> Get(Expression<Func<Product, bool>> expression);
        Task AddAsync(Product entity);
        Task UpdateAsync(Product entity);
        Task DeleteAsync(Product entity);
        Task<List<Product>> GetProductsByCategory(int id);

    }
}
