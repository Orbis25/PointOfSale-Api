using Microsoft.EntityFrameworkCore;
using Model.Enums.Shared;
using Model.Models;
using Model.Persistence;
using Model.ViewModels.Sale;
using Service.Interface;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Service.Service
{
    public class SaleService : ISaleService
    {
        private readonly AppDbContext _dbContext;

        public SaleService(AppDbContext dbContext)
        {
            _dbContext = dbContext;
        }

        public async Task<bool> Add(Sale model)
        {
            try
            {
                await _dbContext.AddAsync(model);
                await _dbContext.SaveChangesAsync();
                return true;
            }
            catch (Exception)
            {
                return false;
            }
        }

        public async Task<bool> AddSV(SaleVM model)
        {
            try
            {
                var sale = new Sale()
                {
                    ClientId = model.ClientId,
                    EmployeeId = model.EmployeeId,
                    CreatedAt = DateTime.Now,
                    SaleCode = model.SaleCode,
                    State = State.active,
                    Total = model.Total
                };

                if (await Add(sale))
                {
                    //retorna el ultimo elemento agregado para sacar el id
                    var saleId = await _dbContext.Sales.OrderByDescending(x => x.CreatedAt).FirstAsync();
                    foreach (var item in model.Products)
                    {
                        var p = await _dbContext.Products.FindAsync(item.Id);
                        p.Quantity -= item.Quantity;
                        await _dbContext.SaleProducts
                            .AddAsync(new SaleProduct
                            {
                                CreateAt = DateTime.Now,
                                ProductId = item.Id,
                                Quantity = item.Quantity,
                                SaleId = saleId.Id
                            });
                        _dbContext.Products.Update(p);
                    }
                }
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
                var model = _dbContext.Sales.Find(id);
                model.State = State.deleted;
                _dbContext.Update(model);
                await _dbContext.SaveChangesAsync();
                return true;
            }
            catch (Exception)
            {
                return false;
            }
        }

        public async Task<IEnumerable<Sale>> GetAll()
        {
            try
            {
                return await _dbContext.Sales.Where(x => x.State == State.active)
                             .OrderByDescending(x => x.CreatedAt)
                            .Include(x => x.Client).ToListAsync();
            }
            catch (Exception)
            {
                return new List<Sale>();
            }
        }

        public async Task<Sale> GetById(Guid id)
        {
            try
            {
                return await _dbContext.Sales
                            .Include(x => x.Client)
                            .Include(x => x.Employee)
                            .Include(X => X.Employee.User)
                            .SingleAsync(x => x.Id == id && x.State == State.active);
            }
            catch (Exception)
            {
                return null;
            }
        }

        public async Task<IEnumerable<Product>> GetsProductBySaleId(Guid id)
        {
            try
            {
                var saleProducts = await _dbContext.SaleProducts.Where(x => x.SaleId == id)
                        .Include(x => x.Product).ToListAsync();
                var products = saleProducts.Select(x => x.Product);

                foreach (var product in products)
                {
                    foreach (var sp in saleProducts)
                    {
                        product.Quantity = sp.Quantity;
                    }
                }

                return products;
            }
            catch (Exception)
            {
                return null;
            }
        }

        public Task<bool> Update(Sale model)
        {
            throw new NotImplementedException();
        }
    }
}
