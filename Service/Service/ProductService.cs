using Microsoft.EntityFrameworkCore;
using Model.Enums.Shared;
using Model.Models;
using Model.Persistence;
using Service.Interface;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Service.Service
{
    public class ProductService : IProductService
    {
        private readonly AppDbContext _dbContext;
        public ProductService(AppDbContext appDbContext) => _dbContext = appDbContext;
        public async Task<bool> Add(Product model)
        {
            try
            {
                await _dbContext.Products.AddAsync(model);
                await _dbContext.SaveChangesAsync();
                return true;
            }
            catch (Exception)
            {
                return false;
            }
        }

        public async Task<bool> Delete(Guid id)
        {
            try
            {
                var model = _dbContext.Products.Find(id);
                model.State = State.deleted;
                await _dbContext.SaveChangesAsync();
                return true;
            }
            catch (Exception)
            {
                return false;
            }
        }

        public async Task<IEnumerable<Product>> GetAll()
        {
            try
            {
                return await _dbContext.Products.Include(x => x.Supplier).Where(x => x.State == State.active)
                    .OrderByDescending(x => x.CreateAt).ToListAsync();
            }
            catch (Exception)
            {
                return new List<Product>();
            }
        }

        public async Task<Product> GetById(Guid id)
        {
            try
            {
                return await _dbContext.Products.Include(x => x.Supplier)
                    .SingleAsync(x => x.Id == id && x.State != State.deleted);
            }
            catch (Exception)
            {
                return new Product();
            }
        }

        public async Task<IEnumerable<Product>> ProductsSpent()
        {
            try
            {
                return await _dbContext.Products.Where(x => x.Quantity <= 2).ToListAsync();
            }
            catch (Exception)
            {
                return null;
            }
        }

        public async Task<bool> Update(Product model)
        {
            try
            {
                var product = _dbContext.Products.Single(x => x.Id == model.Id);
                product.Name = model.Name;
                product.Model = model.Model;
                product.Price = model.Price;
                product.Quantity = model.Quantity;
                product.Type = model.Type;
                product.Avatar = model.Avatar;
                _dbContext.Update(product);
                await _dbContext.SaveChangesAsync();
                return true;
            }
            catch (Exception)
            {

                return false;
            }
        }

        public async Task<bool> UpdateAvatar(Product model)
        {
            try
            {
                var product = _dbContext.Products.Single(x => x.Id == model.Id);
                product.Avatar = model.Avatar;
                _dbContext.Update(product);
                await _dbContext.SaveChangesAsync();
                return true;
            }
            catch (Exception)
            {

                return false;
            }
        }
    }
}
