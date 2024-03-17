using Jewellery.Business.Abstract;
using Jewellery.DataAccess.Abstract;
using Jewellery.Entities.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Expressions;
using System.Text;
using System.Threading.Tasks;

namespace Jewellery.Business.Concrete
{
    public class WishListService : IWishListService
    {
        private readonly IWishListDal _wishListDal;

        public WishListService(IWishListDal wishListDal)
        {
            _wishListDal = wishListDal;
        }

        public async Task AddAsync(WishList entity)
        {
            await _wishListDal.AddAsync(entity);
        }

        public async Task DeleteAsync(WishList entity)
        {
            await _wishListDal.DeleteAsync(entity);
        }

        public async Task<List<WishList>> GetAllAsync()
        {
            return await _wishListDal.GetListAsync();
        }

        public async Task<WishList> GetAsync(Expression<Func<WishList, bool>> expression)
        {
            return await _wishListDal.GetAsync(expression);
        }

        public async Task UpdateAsync(WishList entity)
        {
            await _wishListDal.UpdateAsync(entity);
        }
    }
}
