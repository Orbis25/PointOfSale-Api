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
    public class EmployeeService : IEmployeService
    {
        private readonly AppDbContext _dbContext;

        public EmployeeService(AppDbContext dbContext)
        {
            _dbContext = dbContext;
        }


        public async Task<bool> Add(Employee model)
        {
            try
            {
                _dbContext.Employes.Add(model);
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
                var model = _dbContext.Employes.Find(id);
                model.State = Model.Enums.Shared.State.deleted;
                _dbContext.Update(model);
                await _dbContext.SaveChangesAsync();
                return true;
            }
            catch (Exception)
            {
                return false;
            }
        }

        public async Task<IEnumerable<Employee>> GetAll()
        {
            try
            {
                return await _dbContext.Employes
                            .Include(x => x.User)
                            .Where(x => x.State == Model.Enums.Shared.State.active)
                            .ToListAsync();
            }
            catch (Exception)
            {
                return new List<Employee>();
            }
        }

        public Employee GetbyEmail(string email)
        {
            try
            {
                return _dbContext.Employes.Include(x => x.User).Single(x => x.User.Email == email);
            }
            catch (Exception)
            {
                return null;
            }
        }

        public async Task<Employee> GetById(Guid id)
        {
            try
            {
                return await _dbContext.Employes.Include(x => x.User).SingleAsync(x => x.Id == id);
            }
            catch (Exception)
            {
                return null;
            }
        }

        public async Task<bool> Update(Employee model)
        {
            try
            {
                var employe = _dbContext.Employes.Find(model.Id);
                employe.Address = model.Address;
                employe.Avatar = model.Avatar;
                employe.Dni = model.Dni;
                employe.EmployeeCode = model.EmployeeCode;
                employe.PhoneNumber = model.PhoneNumber;
                employe.State = model.State;
                _dbContext.Update(employe);

                if(model.User.Email != null)
                {
                    var user = _dbContext.Users.Single(x => x.Email == model.User.Email);
                    user.Name = model.User.Name;
                    user.LastName = model.User.LastName;
                    _dbContext.Users.Update(user);
                }
                 await _dbContext.SaveChangesAsync();
                return true;
            }
            catch (Exception)
            {
                return false;
            }
        }

        public async Task<bool> UpdateImg(Employee model)
        {
            try
            {
                var employee = _dbContext.Employes.Single(x => x.Id == model.Id);
                employee.Avatar = model.Avatar;
                _dbContext.Update(employee);
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
