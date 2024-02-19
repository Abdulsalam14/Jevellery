using Jevellery.Models;
using System.Linq.Expressions;

namespace Jevellery.Services.Abstract
{
    public interface ICartService
    {
        Task<List<Cart>> GetAllAsync();
        Task<Cart> Get(Expression<Func<Cart, bool>> expression);
        Task AddAsync(Cart entity);
        Task UpdateAsync(Cart entity);
        Task DeleteAsync(Cart entity);
    }
}
