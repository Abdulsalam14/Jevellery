using Jevellery.DAL;
using Jevellery.Models;
using Jevellery.Repositories.Abstract;
using Microsoft.EntityFrameworkCore;
using System.Linq.Expressions;

namespace Jevellery.Repositories.Concrete
{
    public class CartRepository : ICartRepository
    {
        private readonly AppDbContext _appDbContext;

        public CartRepository(AppDbContext appDbContext)
        {
            _appDbContext = appDbContext;
        }

        public async Task Add(Cart entity)
        {
            await _appDbContext.AddAsync(entity);
            await _appDbContext.SaveChangesAsync();
        }

        public async Task Delete(Cart entity)
        {
             _appDbContext.Remove(entity);
            await _appDbContext.SaveChangesAsync();
        }

        public async Task<Cart> Get(Expression<Func<Cart, bool>> expression)
        {
            return await _appDbContext.Carts.Include(c=>c.CartProducts).FirstOrDefaultAsync(expression);
        }

        public async Task<List<Cart>> GetAll()
        {
            return await _appDbContext.Carts.ToListAsync();
        }

        public async Task Update(Cart entity)
        {
            _appDbContext.Update(entity);
            await _appDbContext.SaveChangesAsync();
        }
    }
}
