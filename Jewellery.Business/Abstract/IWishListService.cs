using Jewellery.Entities.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Expressions;
using System.Text;
using System.Threading.Tasks;

namespace Jewellery.Business.Abstract
{
    public interface IWishListService
    {
        Task<List<WishList>> GetAllAsync();
        Task<WishList> GetAsync(Expression<Func<WishList, bool>> expression);
        Task AddAsync(WishList entity);
        Task UpdateAsync(WishList entity);
        Task DeleteAsync(WishList entity);
    }
}
