using Jewellery.Entities.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Expressions;
using System.Text;
using System.Threading.Tasks;

namespace Jewellery.Business.Abstract
{
    public interface IProductService
    {
        Task<List<Product>> GetAllAsync();
        Task<Product> GetAsync(Expression<Func<Product, bool>> expression);
        Task AddAsync(Product entity);
        Task UpdateAsync(Product entity);
        Task DeleteAsync(Product entity);
        Task<List<Product>> GetProductsByCategory(int id);
        Task<List<Product>> GetNewArrivalProducts();
        Task<List<Product>> GetNotNewArrivalProducts();
        Task<List<Product>> GetNotFeaturedProducts();
        Task<List<Product>> GetFeaturedProducts();
        Task<List<Product>> GetWishListProducts(List<int> ids);

    }
}
