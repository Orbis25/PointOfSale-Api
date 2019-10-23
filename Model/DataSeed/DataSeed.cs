using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Identity;
using Microsoft.Extensions.DependencyInjection;
using Model.Models;
using Model.Persistence;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Model.DataSeed
{
    public static class DataSeed
    {
        public static void SeedRoles(IApplicationBuilder app)
        {
            using(var appScooped = app.ApplicationServices.GetRequiredService<IServiceScopeFactory>().CreateScope())
            {
                string passwordAdmin = "admin123";
                string passwordEmployee = "amployeed123";

                AppDbContext _context = appScooped.ServiceProvider.GetService<AppDbContext>();
                UserManager<User> _userManager = appScooped.ServiceProvider.GetService<UserManager<User>>();

                if (!_context.Users.Any())
                {
                    var admin = new User
                    {
                        UserName = "admin@example.com",
                        Email = "admin@example.com",
                        Name = "admin",
                        LastName = "none",
                        Rol = Enums.User.Rol.admin,
                        CreatedAt = DateTime.Now
                    };

                    var employeed = new User
                    {
                        UserName = "employee@example.com",
                        Email = "employee@example.com",
                        Name = "employee",
                        LastName = "none",
                        Rol = Enums.User.Rol.user,
                        CreatedAt = DateTime.Now
                    };
                      
                    _userManager.CreateAsync(admin, passwordAdmin);
                    _userManager.CreateAsync(employeed, passwordEmployee);
                    _context.Add(admin);
                    _context.Add(employeed);
                    _context.SaveChanges();
                }
            }
        }
    }
}
