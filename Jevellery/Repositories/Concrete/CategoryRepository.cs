using Jevellery.DAL;
using Jevellery.Models;
using Jevellery.Repositories.Abstract;
using Microsoft.EntityFrameworkCore;
using System.Linq.Expressions;

namespace Jevellery.Repositories.Concrete
{
    public class CategoryRepository : ICategoryRepository
    {
        private readonly AppDbContext _appDbContext;
        public CategoryRepository(AppDbContext appDbContext)
        {
            _appDbContext = appDbContext;
        }

        public async Task Add(Category entity)
        {
            await _appDbContext.AddAsync(entity);
            await _appDbContext.SaveChangesAsync();
        }

        public async Task Delete(Category entity)
        {
             _appDbContext.Remove(entity);
            await _appDbContext.SaveChangesAsync();
        }

        public async Task<Category> Get(Expression<Func<Category, bool>> expression)
        {
            return await _appDbContext.Categories.FirstOrDefaultAsync(expression);

        }

        public async Task<List<Category>> GetAll()
        {
            return await _appDbContext.Categories.Include(a=>a.Products).ToListAsync();
        }

        public async Task Update(Category entity)
        {
             _appDbContext.Update(entity);
            await _appDbContext.SaveChangesAsync();
        }
    }
}
