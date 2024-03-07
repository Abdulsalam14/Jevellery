using Jewellery.Entities.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Expressions;
using System.Text;
using System.Threading.Tasks;

namespace Jewellery.Business.Abstract
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
