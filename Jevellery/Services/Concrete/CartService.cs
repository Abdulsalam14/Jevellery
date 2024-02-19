using Jevellery.Models;
using Jevellery.Repositories.Abstract;
using Jevellery.Services.Abstract;
using System.Linq.Expressions;

namespace Jevellery.Services.Concrete
{
    public class CartService : ICartService
    {
        private readonly ICartRepository _repository;

        public CartService(ICartRepository repository)
        {
            _repository = repository;
        }

        public async Task AddAsync(Cart entity)
        {
            await _repository.Add(entity);
        }

        public async Task DeleteAsync(Cart entity)
        {
            await _repository.Delete(entity);
        }

        public async Task<Cart> Get(Expression<Func<Cart, bool>> expression)
        {
            return await _repository.Get(expression);
        }

        public async Task<List<Cart>> GetAllAsync()
        {
            return await _repository.GetAll();
        }

        public async Task UpdateAsync(Cart entity)
        {
            await _repository.Update(entity);
        }
    }
}
