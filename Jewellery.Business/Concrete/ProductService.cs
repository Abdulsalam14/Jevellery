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
using Microsoft.AspNetCore.Http;

namespace Jewellery.Business.Concrete
{
    public class ProductService : IProductService
    {
        private readonly IProductDal _productDal;


        public ProductService( IProductDal productDal)
        {
            _productDal = productDal;
        }

        public async Task AddAsync(Product entity)
        {
            await _productDal.AddAsync(entity);
        }

        public async Task DeleteAsync(Product entity)
        {
            await _productDal.DeleteAsync(entity);
        }

        public async Task<Product> GetAsync(Expression<Func<Product, bool>> expression)
        {
            return await _productDal.GetAsync(expression,p=>p.Include(p=>p.Category));
        }

        public async Task<List<Product>> GetAllAsync()
        {
            return await _productDal.GetListAsync(p=>p.Id>0 ,p=>p.Include(p=>p.Category));
        }

        public async Task<List<Product>> GetNewArrivalProducts()
        {
            return await _productDal.GetListAsync(p => p.IsNewArrival == true, p => p.Include(p => p.Category));
        }

        public async Task<List<Product>> GetProductsByCategory(int id)
        {
            return await _productDal.GetListAsync(p => p.CategoryId == id, p => p.Include(p => p.Category));
        }

        public async Task UpdateAsync(Product entity)
        {
            await _productDal.UpdateAsync(entity);
        }

        public async Task<List<Product>> GetNotNewArrivalProducts()
        {
            return await _productDal.GetListAsync(p => p.IsNewArrival == false, p => p.Include(p => p.Category));
        }

        public async Task<List<Product>> GetNotFeaturedProducts()
        {
            return await _productDal.GetListAsync(p => p.IsFeatured == false, p => p.Include(p => p.Category));


        }

        public async Task<List<Product>> GetFeaturedProducts()
        {
            return await _productDal.GetListAsync(p => p.IsFeatured == true, p => p.Include(p => p.Category));

        }

        public async Task<List<Product>> GetWishListProducts(List<int> ids)
        {
            return await _productDal.GetListAsync(p=>ids.Contains(p.Id));
        }
    }
}
