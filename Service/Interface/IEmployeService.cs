using Model.Models;
using Repository;
using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;

namespace Service.Interface
{
    public interface IEmployeService : IRepositoryService<Employee>
    {
        Employee GetbyEmail(string email);
        Task<bool> UpdateImg(Employee model);
    }
}
