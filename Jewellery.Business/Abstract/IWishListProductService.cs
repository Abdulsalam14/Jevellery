using Jewellery.Entities.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Expressions;
using System.Text;
using System.Threading.Tasks;

namespace Jewellery.Business.Abstract
{
    public interface IWishListProductService
    {
        Task<List<WishlistProduct>> GetAllAsync();
        Task<List<WishlistProduct>> GetWishListProductsBylistId(int wishListId);
        Task<List<WishlistProduct>> GetWishListProductsbyIds(List<int> ids);
        Task<WishlistProduct> GetAsync(Expression<Func<WishlistProduct, bool>> expression);
        Task AddAsync(WishlistProduct entity);
        Task UpdateAsync(WishlistProduct entity);
        Task DeleteAsync(WishlistProduct entity);
    }
}
