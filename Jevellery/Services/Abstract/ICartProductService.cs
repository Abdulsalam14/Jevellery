using Jevellery.Models;
using System.Linq.Expressions;

namespace Jevellery.Services.Abstract
{
    public interface ICartProductService
    {
        Task<List<CartProduct>> GetAllAsync();
        Task<List<CartProduct>> GetCartProductsByCartId(int cartId);
        Task<CartProduct> Get(Expression<Func<CartProduct, bool>> expression);
        Task AddAsync(CartProduct entity);
        Task UpdateAsync(CartProduct entity);
        Task DeleteAsync(CartProduct entity);
    }
}
