using Jewellery.Entities.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Expressions;
using System.Text;
using System.Threading.Tasks;

namespace Jewellery.Business.Abstract
{
    public interface IFirstContentService
    {
        Task<List<HomeFirstContent>> GetAllAsync();
        Task<HomeFirstContent> GetAsync(Expression<Func<HomeFirstContent, bool>> expression);
        Task AddAsync(HomeFirstContent entity);
        Task UpdateAsync(HomeFirstContent entity);
        Task DeleteAsync(HomeFirstContent entity);
    }
}
