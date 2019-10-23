using Microsoft.EntityFrameworkCore;
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
    public class ClientService : IClientService
    {
        private readonly AppDbContext _dbContext;
        public ClientService(AppDbContext dbContext)
        {
            _dbContext = dbContext;
        }

        public async Task<bool> Add(Client model)
        {
            try
            {
                _dbContext.Clients.Add(model);
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
                var model = _dbContext.Clients.Find(id);
                model.State = Model.Enums.Shared.State.deleted;
                _dbContext.Clients.Update(model);
                await _dbContext.SaveChangesAsync();
                return true;
            }
            catch (Exception)
            {
                return false;
            }
        }

        public async Task<IEnumerable<Client>> GetAll()
        {
            try
            {
                return await _dbContext.Clients.Where(x => x.State == Model.Enums.Shared.State.active)
                             .OrderByDescending(x => x.CreatedAt)
                    .ToListAsync();
            }
            catch (Exception)
            {
                return new List<Client>();
            }
        }

        public async Task<Client> GetById(Guid id)
        {
            try
            {
                return await _dbContext.Clients.FindAsync(id);
            }
            catch (Exception)
            {
                return new Client();
            }
        }

        public async Task<bool> Update(Client model)
        {
            try
            {
                var client = await _dbContext.Clients.SingleAsync(x => x.Id == model.Id);
                client.LastName = model.LastName;
                client.Name = model.Name;
                client.PhoneNumber = model.PhoneNumber;
                client.Address = model.Address;
                _dbContext.Clients.Update(client);
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
