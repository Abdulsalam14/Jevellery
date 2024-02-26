using Jevellery.Models;
using Jevellery.Repositories.Abstract;
using Jevellery.Services.Abstract;
using System.Linq.Expressions;

namespace Jevellery.Services.Concrete
{
    public class CategoryService : ICategoryService
    {
        private readonly ICategoryRepository _repository;

        public CategoryService(ICategoryRepository repository)
        {
            _repository = repository;
        }

        public async Task AddAsync(Category entity)
        {
            await _repository.Add(entity);
        }

        public async Task DeleteAsync(Category entity)
        {
            await _repository.Delete(entity);
        }

        public async Task<Category> Get(Expression<Func<Category, bool>> expression)
        {
            return await _repository.Get(expression);
        }

        public async Task<List<Category>> GetAllAsync()
        {
           return  await _repository.GetAll();
        }

        public Task UpdateAsync(Category entity)
        {
            return _repository.Update(entity);
        }
    }
}
