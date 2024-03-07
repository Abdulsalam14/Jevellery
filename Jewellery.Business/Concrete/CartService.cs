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
    public class CartService : ICartService
    {
        private readonly ICartDal _cartDal;

        public CartService(ICartDal cartDal)
        {
            _cartDal = cartDal;
        }

        public async Task AddAsync(Cart entity)
        {
            await _cartDal.AddAsync(entity);
        }

        public async Task DeleteAsync(Cart entity)
        {
            await _cartDal.DeleteAsync(entity);
        }

        public async Task<Cart> GetAsync(Expression<Func<Cart, bool>> expression)
        {
            return await _cartDal.GetAsync(expression);
        }

        public async Task<List<Cart>> GetAllAsync()
        {
            return await _cartDal.GetListAsync();
        }

        public async Task UpdateAsync(Cart entity)
        {
            await _cartDal.UpdateAsync(entity);
        }
    }
}
