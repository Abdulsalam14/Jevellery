using Jewellery.Entities.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Expressions;
using System.Text;
using System.Threading.Tasks;

namespace Jewellery.Business.Abstract
{
    public interface ICartService
    {
        Task<List<Cart>> GetAllAsync();
        Task<Cart> GetAsync(Expression<Func<Cart, bool>> expression);
        Task AddAsync(Cart entity);
        Task UpdateAsync(Cart entity);
        Task DeleteAsync(Cart entity);
    }
}
