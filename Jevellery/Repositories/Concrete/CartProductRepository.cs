﻿using Jevellery.DAL;
using Jevellery.Models;
using Jevellery.Repositories.Abstract;
using Microsoft.EntityFrameworkCore;
using System.Linq.Expressions;

namespace Jevellery.Repositories.Concrete
{
    public class CartProductRepository : ICartProductRepository
    {
        private readonly AppDbContext _dbContext;

        public CartProductRepository(AppDbContext dbContext)
        {
            _dbContext = dbContext;
        }

        public async Task Add(CartProduct entity)
        {
            await _dbContext.AddAsync(entity);
            await _dbContext.SaveChangesAsync();
        }

        public async Task Delete(CartProduct entity)
        {
             _dbContext.Remove(entity);
            await _dbContext.SaveChangesAsync();
        }

        public async Task<CartProduct> Get(Expression<Func<CartProduct, bool>> expression)
        {
           return await _dbContext.CartProducts.FirstOrDefaultAsync(expression);
        }

        public async Task<List<CartProduct>> GetAll()
        {
            return await _dbContext.CartProducts.ToListAsync();
        }

        public async Task Update(CartProduct entity)
        {
             _dbContext.Update(entity);
            await _dbContext.SaveChangesAsync();
        }
    }
}