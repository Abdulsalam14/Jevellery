using Jevellery.Models;
using Microsoft.EntityFrameworkCore.Query;
using System.Linq.Expressions;

namespace Jevellery.Repositories.Abstract
{
    public interface ICartProductRepository : IRepository<CartProduct>
    {
        //public Task<List<CartProduct>> GetList(Expression<Func<CartProduct, bool>> filter = null,
        //    Func<IQueryable<CartProduct>, IIncludableQueryable<CartProduct, object>> include = null);

        public Task<List<CartProduct>> GetList(Expression<Func<CartProduct, bool>> filter = null, Func<IQueryable<CartProduct>,
            IQueryable<CartProduct>> select = null);
    }
}
