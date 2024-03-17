using Jewellery.Business.Abstract;
using Jewellery.DataAccess.Abstract;
using Jewellery.DataAccess.Concrete.EFEntityFramework;
using Jewellery.Entities.Models;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Expressions;
using System.Text;
using System.Threading.Tasks;

namespace Jewellery.Business.Concrete
{
    public class WishListProductService:IWishListProductService
    {
        private readonly IWishListProductDal _wishListDal;

        public WishListProductService(IWishListProductDal wishListDal)
        {
            _wishListDal = wishListDal;
        }

        public async Task AddAsync(WishlistProduct entity)
        {
            await _wishListDal.AddAsync(entity);
        }

        public async Task DeleteAsync(WishlistProduct entity)
        {
            await _wishListDal.DeleteAsync(entity);
        }

        public async Task<List<WishlistProduct>> GetAllAsync()
        {
            return await _wishListDal.GetListAsync();
        }

        public async Task<WishlistProduct> GetAsync(Expression<Func<WishlistProduct, bool>> expression)
        {
            return await _wishListDal.GetAsync(expression);
        }

        public async Task<List<WishlistProduct>> GetWishListProductsbyIds(List<int> ids)
        {

            return await _wishListDal.GetListAsync(p => ids.Contains(p.ProductId)==true,p=>p.Include(p=>p.Product));
        }

        public async Task<List<WishlistProduct>> GetWishListProductsBylistId(int wishListId)
        {
            return await _wishListDal.GetListAsync(
                cp => cp.WishlistId == wishListId,query => query
                    .Include(cp => cp.Product)
            );
        }

        public async Task UpdateAsync(WishlistProduct entity)
        {
            await _wishListDal.UpdateAsync(entity);
        }
    }
}
