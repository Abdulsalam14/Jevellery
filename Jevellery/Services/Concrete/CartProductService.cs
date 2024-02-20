using Jevellery.Models;
using Jevellery.Repositories.Abstract;
using Jevellery.Services.Abstract;
using Microsoft.EntityFrameworkCore;
using System.Linq.Expressions;

namespace Jevellery.Services.Concrete
{
    public class CartProductService : ICartProductService
    {
        private readonly ICartProductRepository _repository;

        public CartProductService(ICartProductRepository repository)
        {
            _repository = repository;
        }

        public async Task AddAsync(CartProduct entity)
        {
            await _repository.Add(entity);
        }

        public async Task DeleteAsync(CartProduct entity)
        {
            await _repository.Delete(entity);
        }

        public async Task<CartProduct> Get(Expression<Func<CartProduct, bool>> expression)
        {
            return await _repository.Get(expression);
        }

        public async Task<List<CartProduct>> GetAllAsync()
        {
            return await _repository.GetAll();
        }

        public async Task<List<CartProduct>> GetCartProductsByCartId(int cartId)
        {
            return await _repository.GetList(
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
            await _repository.Update(entity);
        }
    }
}
