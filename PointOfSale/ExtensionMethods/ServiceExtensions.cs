using Microsoft.AspNetCore.Authentication.JwtBearer;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.IdentityModel.Tokens;
using Model.Models;
using Model.Persistence;
using PointOfSale.Handlers;
using Service.Interface;
using Service.Service;
using Swashbuckle.AspNetCore.Swagger;
using System;
using System.Text;

namespace ExtensionMethods
{
    public static class ServiceExtensions
    { 
        //configure cors to acepts request
        public static void ConfigureCords(this IServiceCollection services)
           => services.AddCors(options =>
            {
                options.AddPolicy("AllowSpecificOrigin", 
                     builder => builder
                    .AllowAnyHeader()
                    .AllowAnyMethod()
                    .AllowCredentials()
                    .AllowAnyOrigin());
            });

        public static void ConfigureDbContext(this IServiceCollection services , IConfiguration configuration)
        => services.AddDbContext<AppDbContext>(options =>
        {
            options.UseSqlServer(configuration.GetConnectionString("SqlServer"));
        });

        //add configuration Identity to Septup
        public static void IdentityConfig(this IServiceCollection services)
           => services.AddIdentity<User, IdentityRole>()
                      .AddEntityFrameworkStores<AppDbContext>()
                      .AddDefaultTokenProviders();
                          
        //configuration Identity to password free
        public static void IdentityConfigModeFree(this IServiceCollection services)
        {
            services.Configure<IdentityOptions>(options =>
            {
                options.Password.RequireDigit = false;
                options.Password.RequireLowercase = false;
                options.Password.RequireNonAlphanumeric = false;
                options.Password.RequireUppercase = false;
                options.Password.RequiredLength = 6;
                options.Password.RequiredUniqueChars = 1;
                options.User.RequireUniqueEmail = true;

            });
        }

        public static void SwaggerDoc(this IServiceCollection services)
        {
            services.AddSwaggerGen(c =>
            {
                c.SwaggerDoc("v1", new Info
                {
                    Title = "Point Of Sale Api",
                    Version = "v1"
                });
            });
        }
        //Configuration service to auth with JWT 
        public static void AuthServiceConfig(this IServiceCollection services, IConfiguration configuration)
        {
            services.AddAuthentication(JwtBearerDefaults.AuthenticationScheme)
                .AddJwtBearer(options =>
               options.TokenValidationParameters = new TokenValidationParameters
               {
                   ValidateIssuer = true,
                   ValidateAudience = true,
                   ValidateLifetime = false,
                   ValidateIssuerSigningKey = true,
                   ValidIssuer = "devteams.com",
                   ValidAudience = "devteams.com",
                   IssuerSigningKey = new SymmetricSecurityKey(
                   Encoding.UTF8.GetBytes(configuration["Secret_Key"])),
                   ClockSkew = TimeSpan.Zero
               });
        }

        //configuration to services
        public static void WebServices(this IServiceCollection services)
        {
            services.AddTransient<IClientService, ClientService>();
            services.AddTransient<IEmployeService, EmployeeService>();
            services.AddTransient<ISupplierService, SupplierService>();
            services.AddTransient<IUserService, UserService>();
            services.AddTransient<IProductService, ProductService>();
            services.AddTransient<ISaleService, SaleService>();
            services.AddTransient<IImageWriter, ImageWriter>();
            services.AddTransient<IImageHandlerService<IActionResult>, ImageHandler>();
            services.AddTransient<IReportService, ReportService>();
        }

    }
}
