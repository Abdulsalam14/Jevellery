using Jevellery.Models;
using Jevellery.Repositories.Abstract;
using Jevellery.Services.Abstract;
using Microsoft.EntityFrameworkCore;
using System.Linq.Expressions;

namespace Jevellery.Services.Concrete
{
    public class ProductService : IProductService
    {
        private readonly IProductRepository _repository;

        public ProductService(IProductRepository repository)
        {
            _repository = repository;

        }

        public async Task AddAsync(Product entity)
        {
            await _repository.Add(entity);
        }

        public async Task DeleteAsync(Product entity)
        {
            await _repository.Delete(entity);
        }

        public async Task<Product> Get(Expression<Func<Product, bool>> expression)
        {
            return await _repository.Get(expression);
        }

        public async Task<List<Product>> GetAllAsync()
        {
            return await _repository.GetAll();
        }

        public async Task<List<Product>> GetProductsByCategory(int id)
        {
            return await _repository.GetList(p => p.CategoryId == id,p=>p.Include(p=>p.Category));
        }

        public async Task UpdateAsync(Product entity)
        {
            await _repository.Update(entity);
        }
    }
}
