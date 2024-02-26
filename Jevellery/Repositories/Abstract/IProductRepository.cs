using Jevellery.Models;
using System.Linq.Expressions;

namespace Jevellery.Repositories.Abstract
{
    public interface IProductRepository:IRepository<Product>
    {
        public Task<List<Product>> GetList(Expression<Func<Product, bool>> filter = null, Func<IQueryable<Product>,
     IQueryable<Product>> select = null);
    }
}
