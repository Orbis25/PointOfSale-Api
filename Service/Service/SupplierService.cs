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
    public class SupplierService : ISupplierService
    {
        private readonly AppDbContext _dbContext;

        public SupplierService(AppDbContext dbContext)
        {
            _dbContext = dbContext;
        }

        public async Task<bool> Add(Supplier model)
        {
            try
            {
                _dbContext.Suppliers.Add(model);
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
                var model = _dbContext.Suppliers.Find(id);
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

        public async Task<IEnumerable<Supplier>> GetAll()
        {
            try
            {
                return await _dbContext.Suppliers.Where(x => x.State == State.active)
                             .OrderByDescending(x => x.CreatedAt)
                    .ToListAsync();
            }
            catch (Exception)
            {
                return new List<Supplier>();
            }
        }

        public async Task<Supplier> GetById(Guid id)
        {
            try
            {
                return await _dbContext.Suppliers.Where(x => x.State == State.active).SingleAsync(x => x.Id == id);
            }
            catch (Exception)
            {
                return new Supplier();
            }
        }

        public async Task<bool> Update(Supplier model)
        {
            try
            {
                var suppler = _dbContext.Suppliers.Find(model.Id);
                suppler.Name = model.Name;
                suppler.PhoneNumber = model.PhoneNumber;
                suppler.CompanyName = model.CompanyName;
                suppler.State = model.State;
                _dbContext.Suppliers.Update(suppler);
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
