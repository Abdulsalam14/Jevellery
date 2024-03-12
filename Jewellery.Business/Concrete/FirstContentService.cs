using Jewellery.Business.Abstract;
using Jewellery.DataAccess.Abstract.Home;
using Jewellery.Entities.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Expressions;
using System.Text;
using System.Threading.Tasks;

namespace Jewellery.Business.Concrete
{
    public class FirstContentService : IFirstContentService
    {
        private readonly IFIrstContentDal _firstContentDal;

        public FirstContentService(IFIrstContentDal firstContentDal)
        {
            _firstContentDal = firstContentDal;
        }

        public async Task AddAsync(HomeFirstContent entity)
        {
            await _firstContentDal.AddAsync(entity);
        }

        public async Task DeleteAsync(HomeFirstContent entity)
        {
            await _firstContentDal.DeleteAsync(entity);
        }

        public async Task<List<HomeFirstContent>> GetAllAsync()
        {
            return await _firstContentDal.GetListAsync();
        }

        public async Task<HomeFirstContent> GetAsync(Expression<Func<HomeFirstContent, bool>> expression)
        {
            return await _firstContentDal.GetAsync(expression);
        }

        public async Task UpdateAsync(HomeFirstContent entity)
        {
            await _firstContentDal.UpdateAsync(entity);
        }
    }
}
