using Microsoft.EntityFrameworkCore;
using Model.Models;
using Model.Persistence;
using Service.Interface;
using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;

namespace Service.Service
{
    public class UserService : IUserService
    {
        private readonly AppDbContext _dbContext;
        public UserService(AppDbContext dbContext)
        {
            _dbContext = dbContext;
        }

        public async Task<User> GetUserByEmail(string email)
        {
            return await _dbContext.Users.SingleAsync(x => x.UserName == email);
        } 
    }
}
