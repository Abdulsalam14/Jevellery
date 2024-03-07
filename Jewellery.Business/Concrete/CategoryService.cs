using Jewellery.Business.Abstract;
using Jewellery.DataAccess.Abstract;
using Jewellery.Entities.Models;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Expressions;
using System.Text;
using System.Threading.Tasks;

namespace Jewellery.Business.Concrete
{
    public class CategoryService : ICategoryService
    {
        private readonly ICategoryDal _categoryDal;

        public CategoryService(ICategoryDal categoryDal)
        {
            _categoryDal = categoryDal;
        }

        public async Task AddAsync(Category entity)
        {
            await _categoryDal.AddAsync(entity);
        }

        public async Task DeleteAsync(Category entity)
        {
            await _categoryDal.DeleteAsync(entity);
        }

        public async Task<Category> GetAsync(Expression<Func<Category, bool>> expression)
        {
            return await _categoryDal.GetAsync(expression);
        }

        public async Task<List<Category>> GetAllAsync()
        {
            return await _categoryDal.GetListAsync(c=>c.Id>0,c=>c.Include(c=>c.Products));
        }

        public Task UpdateAsync(Category entity)
        {
            return _categoryDal.UpdateAsync(entity);
        }
    }
}
