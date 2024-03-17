using Jewellery.Entities.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Expressions;
using System.Text;
using System.Threading.Tasks;

namespace Jewellery.Business.Abstract
{
    public interface ICollectionService
    {
        Task<List<Collection>> GetAllAsync();
        Task<Collection> GetAsync(Expression<Func<Collection, bool>> expression);
        Task AddAsync(Collection entity);
        Task UpdateAsync(Collection entity);
        Task DeleteAsync(Collection entity);
    }
}
