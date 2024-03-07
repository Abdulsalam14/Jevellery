using Jewellery.Business.Abstract;
using Jewellery.DataAccess.Abstract;
using Jewellery.Entities.Models;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Query;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Expressions;
using System.Text;
using System.Threading.Tasks;

namespace Jewellery.Business.Concrete
{
    public class CartProductService:ICartProductService
    {
        private readonly ICartProductDal _cartProductDal;

        public CartProductService(ICartProductDal cartProductDal)
        {
            _cartProductDal = cartProductDal;
        }

        public async Task AddAsync(CartProduct entity)
        {
            await _cartProductDal.AddAsync(entity);
        }

        public async Task DeleteAsync(CartProduct entity)
        {
            await _cartProductDal.DeleteAsync(entity);
        }

        public async Task<CartProduct> Get(Expression<Func<CartProduct, bool>> expression)
        {
            return await _cartProductDal.GetAsync(expression);
        }

        public async Task<List<CartProduct>> GetAllAsync()
        {
            return await _cartProductDal.GetListAsync();
        }

        public async Task<List<CartProduct>> GetCartProductsByCartId(int cartId)
        {
            return await _cartProductDal.GetListAsync(
                cp => cp.CartId == cartId,
                query => query
                    .Include(cp => cp.Product)
                    .Select(cp => new CartProduct
                    {
                        Product = cp.Product,
                        Quantity = cp.Quantity,
                        CartId = cp.CartId,
                        Id = cp.Id
                    })
            );
        }

        public async Task UpdateAsync(CartProduct entity)
        {
            await _cartProductDal.UpdateAsync(entity);
        }
    }
}
