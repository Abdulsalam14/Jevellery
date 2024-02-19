using Jevellery.Models;
using Jevellery.Repositories.Abstract;
using Jevellery.Services.Abstract;
using System.Linq.Expressions;

namespace Jevellery.Services.Concrete
{
    public class CartProductService : ICartProductService
    {
        private readonly ICartProductRepository _repository;
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

        public async Task UpdateAsync(CartProduct entity)
        {
            await _repository.Update(entity);
        }
    }
}
