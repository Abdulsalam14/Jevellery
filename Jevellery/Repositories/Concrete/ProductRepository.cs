using Jevellery.DAL;
using Jevellery.Models;
using Jevellery.Repositories.Abstract;
using Microsoft.EntityFrameworkCore;
using System.Linq.Expressions;

namespace Jevellery.Repositories.Concrete
{
    public class ProductRepository : IProductRepository
    {
        private readonly AppDbContext _appDbContext;

        public ProductRepository(AppDbContext appDbContext)
        {
            _appDbContext = appDbContext;
        }

        public async Task Add(Product entity)
        {
            await _appDbContext.AddAsync(entity);
            await _appDbContext.SaveChangesAsync();
        }

        public async Task Delete(Product entity)
        {
             _appDbContext.Remove(entity);
            await _appDbContext.SaveChangesAsync();
        }

        public async Task<Product> Get(Expression<Func<Product, bool>> expression)
        {
            var pr= await  _appDbContext.Products.FirstOrDefaultAsync(expression);
            return pr;
        }


        public async Task Update(Product entity)
        {
            _appDbContext.Update(entity);
            await _appDbContext.SaveChangesAsync();
        }

        async Task<List<Product>> IRepository<Product>.GetAll()
        {
            return await _appDbContext.Products.Include(p=>p.Category).ToListAsync();
        }
    }
}
