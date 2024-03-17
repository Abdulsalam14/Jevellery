using Jewellery.Business.Abstract;
using Jewellery.DataAccess.Abstract;
using Jewellery.DataAccess.Concrete.EFEntityFramework;
using Jewellery.Entities.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Expressions;
using System.Text;
using System.Threading.Tasks;

namespace Jewellery.Business.Concrete
{
    public class CollectionService : ICollectionService
    {
        private readonly ICollectionDal _collectionDal;

        public CollectionService(ICollectionDal collectionDal)
        {
            _collectionDal = collectionDal;
        }

        public async Task AddAsync(Collection entity)
        {
            await _collectionDal.AddAsync(entity);
        }

        public async Task DeleteAsync(Collection entity)
        {
            await _collectionDal.DeleteAsync(entity);
        }

        public async Task<List<Collection>> GetAllAsync()
        {
            return await _collectionDal.GetListAsync();
        }

        public async Task<Collection> GetAsync(Expression<Func<Collection, bool>> expression)
        {
            return await _collectionDal.GetAsync(expression);
        }

        public async Task UpdateAsync(Collection entity)
        {
            await _collectionDal.UpdateAsync(entity);
        }
    }
}
